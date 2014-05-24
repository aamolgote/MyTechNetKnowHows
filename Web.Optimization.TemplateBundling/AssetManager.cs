using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Asset Manager Class
    /// </summary>
    internal sealed class AssetManager
    {
        #region Private Members
        internal static readonly object AssetsManagerKey = (object)typeof(AssetManager);
        private readonly HttpContextBase _httpContext;
        private Func<string, string, string> _resolveUrlMethod;
        private IBundleResolver _resolver;
        private BundleCollection _bundles;
        private bool? _optimizationEnabled;
        #endregion

        #region Properties
        internal HttpContextBase Context
        {
            get
            {
                return this._httpContext;
            }
        }

        internal Func<string, string, string> ResolveUrlMethod
        {
            get
            {
                return this._resolveUrlMethod ?? (Func<string, string, string>)((basePath, relativePath) => UrlUtil.Url(basePath, relativePath));
            }
            set
            {
                this._resolveUrlMethod = value;
            }
        }

        internal IBundleResolver Resolver
        {
            get
            {
                return this._resolver ?? HtmlTemplateBundleResolver.Current;
            }
            set
            {
                this._resolver = value;
            }
        }

        internal BundleCollection Bundles
        {
            get
            {
                return this._bundles ?? BundleTable.Bundles;
            }
            set
            {
                this._bundles = value;
                //this._bundles.Context = this.Context;
            }
        }

        internal bool OptimizationEnabled
        {
            get
            {
                if (this._optimizationEnabled.HasValue)
                    return this._optimizationEnabled.Value;
                else
                    return BundleTable.EnableOptimizations;
            }
            set
            {
                this._optimizationEnabled = new bool?(value);
            }
        }
        #endregion

        #region Constructor
        static AssetManager()
        {
        }

        public AssetManager(HttpContextBase context)
        {
            this._httpContext = context;
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public static AssetManager GetInstance(HttpContextBase context)
        {
            if (context == null)
                return (AssetManager)null;
            AssetManager assetManager = (AssetManager)context.Items[AssetManager.AssetsManagerKey];
            if (assetManager == null)
            {
                assetManager = new AssetManager(context);
                context.Items[AssetManager.AssetsManagerKey] = (object)assetManager;
            }
            return assetManager;
        }
        /// <summary>
        /// Renders the explicit.
        /// </summary>
        /// <param name="tagFormat">The tag format.</param>
        /// <param name="paths">The paths.</param>
        /// <returns></returns>
        public IHtmlString RenderExplicit(string tagFormat, params string[] paths)
        {
            IEnumerable<AssetManager.AssetTag> enumerable = this.DeterminePathsToRender((IEnumerable<string>)paths, tagFormat);
            StringBuilder stringBuilder = new StringBuilder();
            foreach (AssetManager.AssetTag assetTag in enumerable)
            {
                stringBuilder.Append(assetTag.Render());
                stringBuilder.Append(Environment.NewLine);
            }
            return (IHtmlString)new HtmlString(((object)stringBuilder).ToString());
        }
        #endregion

        #region Private Members
        /// <summary>
        /// Eliminates the duplicates and resolve urls.
        /// </summary>
        /// <param name="refs">The refs.</param>
        /// <param name="tagFormat">The tag format.</param>
        /// <returns>List Of Asset Tags</returns>
        private IEnumerable<AssetManager.AssetTag> EliminateDuplicatesAndResolveUrls(IEnumerable<AssetManager.AssetTag> refs, string tagFormat)
        {
            List<AssetManager.AssetTag> list = new List<AssetManager.AssetTag>();
            HashSet<string> hashSet = new HashSet<string>();
            HashSet<string> bundledContents = new HashSet<string>();
            HtmlTemplateBundleResolver resolver = (HtmlTemplateBundleResolver)this.Resolver;
            foreach (AssetManager.AssetTag assetTag in refs)
            {
                if (assetTag.IsStaticAsset)
                {
                    list.Add(assetTag);
                }
                else
                {
                    string virtualPath1 = assetTag.Value;
                    if (!hashSet.Contains(virtualPath1))
                    {
                        if (resolver.IsBundleVirtualPath(virtualPath1))
                        {
                            StringBuilder stringBuilder = new StringBuilder();
                            foreach (BundleFile bundleFile in resolver.GetBundleFileContents(virtualPath1))
                            {
                                string fileContent = bundleFile.ApplyTransforms();
                                string templateId = bundleFile.IncludedVirtualPath;
                                stringBuilder.Append(assetTag.Render(tagFormat, fileContent, templateId));
                                stringBuilder.Append(Environment.NewLine);
                            }
                            assetTag.Value = stringBuilder.ToString();
                            list.Add(assetTag);
                        }
                        else
                        {
                            string str = this.ResolveVirtualPath(virtualPath1);
                            if (!hashSet.Contains(str))
                            {
                                hashSet.Add(str);
                                assetTag.Value = str;
                                list.Add(assetTag);
                            }
                        }
                        hashSet.Add(virtualPath1);
                    }
                }
            }
            return Enumerable.Where<AssetManager.AssetTag>((IEnumerable<AssetManager.AssetTag>)list, (Func<AssetManager.AssetTag, bool>)(asset =>
            {
                if (!asset.IsStaticAsset)
                    return !bundledContents.Contains(asset.Value);
                else
                    return true;
            }));
        }

        private IEnumerable<AssetManager.AssetTag> DeterminePathsToRender(IEnumerable<string> assets, string tagFormat)
        {
            List<AssetManager.AssetTag> list = new List<AssetManager.AssetTag>();
            HtmlTemplateBundleResolver resolver = (HtmlTemplateBundleResolver)this.Resolver;
            foreach (string str1 in assets)
            {
                if (resolver.IsBundleVirtualPath(str1))
                {
                    if (!this.OptimizationEnabled)
                    {
                        list.Add(new AssetManager.AssetTag(str1));
                    }
                    else
                    {
                        list.Add(new AssetManager.AssetTag(str1));
                        if (this.Bundles.UseCdn)
                        {
                            Bundle bundleFor = this.Bundles.GetBundleFor(str1);
                            if (bundleFor != null && !string.IsNullOrEmpty(bundleFor.CdnPath) && !string.IsNullOrEmpty(bundleFor.CdnFallbackExpression))
                                list.Add(new AssetManager.AssetTag(string.Format((IFormatProvider)CultureInfo.InvariantCulture, OptimizationResources.CdnFallBackScriptString, new object[2]
                                {
                                  (object) bundleFor.CdnFallbackExpression,
                                  (object) this.ResolveVirtualPath(str1)
                                }))
                                {
                                    IsStaticAsset = true
                                });
                        }
                    }
                }
                else
                { 
                    list.Add(new AssetManager.AssetTag(str1));
                }
            }
            return this.EliminateDuplicatesAndResolveUrls((IEnumerable<AssetManager.AssetTag>)list, tagFormat);
        }



        internal string ResolveVirtualPath(string virtualPath)
        {
            Uri result;
            if (Uri.TryCreate(virtualPath, UriKind.Absolute, out result))
                return virtualPath;
            string str = "";
            if (this._httpContext.Request != null)
                str = this._httpContext.Request.AppRelativeCurrentExecutionFilePath;
            return this.ResolveUrlMethod(str, virtualPath);
        }

        internal HtmlString ResolveUrl(string url)
        {
            if (this.Resolver.IsBundleVirtualPath(url))
                return new HtmlString(this.Bundles.ResolveBundleUrl(url));
            else
                return new HtmlString(this.ResolveVirtualPath(url));
        }
        #endregion

        #region Asset Tag Internal Class
        internal class AssetTag
        {
            public string Value { get; set; }

            public bool IsStaticAsset { get; set; }

            public AssetTag(string value)
            {
                this.Value = value;
            }

            public string Render(string tagFormat)
            {
                if (this.IsStaticAsset)
                    return this.Value;
                return string.Format((IFormatProvider)CultureInfo.InvariantCulture, tagFormat, new object[1]
                {
                  (object) HttpUtility.UrlPathEncode(this.Value)
                });
            }
            public string Render(string tagFormat, string templateString, string id)
            {
                id = id.Replace("~", string.Empty);
                if (this.IsStaticAsset)
                    return this.Value;
                return string.Format((IFormatProvider)CultureInfo.InvariantCulture, tagFormat, templateString, id);
            }

            public string Render()
            {
                if (this.IsStaticAsset)
                    return this.Value;
                return string.Format((IFormatProvider)CultureInfo.InvariantCulture, this.Value);
            }
        }
        #endregion

    }
}
