using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// HTML Template Bundle
    /// </summary>
    public class HtmlTemplateBundle : Bundle
    {
        #region Private Members
        private IBundleBuilder _builder;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Optimization.ScriptBundle"/> class that takes a virtual path for the bundle.
        /// </summary>
        /// <param name="virtualPath">The virtual path for the bundle.</param>
        public HtmlTemplateBundle(string virtualPath)
            : this(virtualPath, (string)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Optimization.ScriptBundle"/> class that takes virtual path and cdnPath for the bundle.
        /// </summary>
        /// <param name="virtualPath">The virtual path for the bundle.</param><param name="cdnPath">The path of a Content Delivery Network (CDN).</param>
        public HtmlTemplateBundle(string virtualPath, string cdnPath)
            : base(virtualPath, cdnPath, new IBundleTransform[1]
              {
                (IBundleTransform) new HtmlTemplateMinify()
              })
        {
            this.ConcatenationToken = ";" + Environment.NewLine;
        }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Processes the bundle request to generate the response.
        /// </summary>
        /// <param name="context">The <see cref="T:System.Web.Optimization.BundleContext" /> object that contains state for both the framework configuration and the HTTP request.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Optimization.BundleResponse" /> object containing the processed bundle contents.
        /// </returns>
        /// <exception cref="System.ArgumentNullException">context</exception>
        public override BundleResponse GenerateBundleResponse(BundleContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            IEnumerable<BundleFile> files1 = this.EnumerateFiles(context);
            IEnumerable<BundleFile> files2 = context.BundleCollection.IgnoreList.FilterIgnoredFiles(context, files1);
            IEnumerable<BundleFile> enumerable = this.Orderer.OrderFiles(context, files2);
            if (this.EnableFileExtensionReplacements)
                enumerable = context.BundleCollection.FileExtensionReplacementList.ReplaceFileExtensions(context, enumerable);
            string bundleContent = this.Builder.BuildBundleContent(this, context, enumerable);
            return this.ApplyTransforms(context, bundleContent, enumerable);
        }

        public override IBundleBuilder Builder
        {
            get
            {
                if (this._builder == null)
                    return HtmlTemplateBundleBuilder.Instance;
                else
                    return this._builder;
            }
            set
            {
                this._builder = value;
                this.InvalidateCacheEntries();
            }
        }

        public override BundleResponse ApplyTransforms(BundleContext context, string bundleContent, IEnumerable<BundleFile> bundleFiles)
        {
            return base.ApplyTransforms(context, bundleContent, bundleFiles);
        }

        #endregion

        #region Internal Methods
        /// <summary>
        /// Invalidates the cache entries.
        /// </summary>
        internal void InvalidateCacheEntries()
        {
           //Needs to be Implemented
        }
        #endregion
    }
}
