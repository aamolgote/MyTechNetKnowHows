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
    /// Html Template Bundle Resolver
    /// </summary>
    public class HtmlTemplateBundleResolver : IBundleResolver
    {
        #region Private Members
        private static HtmlTemplateBundleResolver _default = new HtmlTemplateBundleResolver();
        private static IBundleResolver _current;
        private HttpContextBase _context;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the ScriptManager that reflects against <see cref="P:System.Web.Optimization.BundleResolver.Current"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The ScriptManager that reflects against <see cref="P:System.Web.Optimization.BundleResolver.Current"/>.
        /// </returns>
        public static IBundleResolver Current
        {
            get
            {
                return HtmlTemplateBundleResolver._current ?? (IBundleResolver)HtmlTemplateBundleResolver._default;
            }
            set
            {
                HtmlTemplateBundleResolver._current = value;
            }
        }

        private BundleCollection Bundles { get; set; }

        internal HttpContextBase Context
        {
            get
            {
                return this._context ?? (HttpContextBase)new HttpContextWrapper(HttpContext.Current);
            }
            set
            {
                this._context = value;
            }
        }
        #endregion

        #region Constructors
        static HtmlTemplateBundleResolver()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Optimization.BundleResolver"/> class.
        /// </summary>
        public HtmlTemplateBundleResolver()
            : this(BundleTable.Bundles)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Optimization.BundleResolver"/> class with the specified bundle.
        /// </summary>
        /// <param name="bundles">The bundles of objects.</param>
        public HtmlTemplateBundleResolver(BundleCollection bundles)
            : this(bundles, (HttpContextBase)null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Web.Optimization.BundleResolver"/> class with the specified bundle and context.
        /// </summary>
        /// <param name="bundles">The bundles of object.</param><param name="context">The HttpContextBase.</param>
        public HtmlTemplateBundleResolver(BundleCollection bundles, HttpContextBase context)
        {
            this.Bundles = bundles;
            this.Context = context;
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Determines if the virtualPath is to a bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The virtualPath.
        /// </returns>
        /// <param name="virtualPath">The virtual file path.</param>
        public bool IsBundleVirtualPath(string virtualPath)
        {
            if (ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") != null)
                return false;
            else
                return this.Bundles.GetBundleFor(virtualPath) != null;
        }

        /// <summary>
        /// Returns an enumeration of actual file paths to the contents of the bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The actual file paths to the contents of the bundle.
        /// </returns>
        /// <param name="virtualPath">The virtual file path.</param>
        public IEnumerable<string> GetBundleContents(string virtualPath)
        {
            if (ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") != null)
                return (IEnumerable<string>)null;
            Bundle bundleFor = this.Bundles.GetBundleFor(virtualPath);
            if (bundleFor == null)
                return (IEnumerable<string>)null;
            List<string> list = new List<string>();
            BundleContext context = new BundleContext(this.Context, this.Bundles, virtualPath);
            BundleResponse bundleResponse = bundleFor.GenerateBundleResponse(context);
            foreach (BundleFile bundleFile in bundleResponse.Files)
            { 
                list.Add(bundleFile.IncludedVirtualPath);
            }
            return (IEnumerable<string>)list;
        }

        /// <summary>
        /// Gets the versioned url for the bundle or returns the virtualPath unchanged if it does not point to a bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The versioned url for the bundle.
        /// </returns>
        /// <param name="virtualPath">The virtual file path.</param>
        public string GetBundleUrl(string virtualPath)
        {
            if (ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") != null)
                return (string)null;
            else
                return this.Bundles.ResolveBundleUrl(virtualPath);
        }

        /// <summary>
        /// Gets the bundle file contents.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <returns></returns>
        public IEnumerable<BundleFile> GetBundleFileContents(string virtualPath)
        {
            if (ExceptionUtil.ValidateVirtualPath(virtualPath, "virtualPath") != null)
                return (IEnumerable<BundleFile>)null;
            Bundle bundleFor = this.Bundles.GetBundleFor(virtualPath);
            if (bundleFor == null)
                return (IEnumerable<BundleFile>)null;
            List<string> list = new List<string>();
            BundleContext context = new BundleContext(this.Context, this.Bundles, virtualPath);
            BundleResponse bundleResponse = bundleFor.GenerateBundleResponse(context);
            return bundleResponse.Files;
        }
        #endregion
    }
}
