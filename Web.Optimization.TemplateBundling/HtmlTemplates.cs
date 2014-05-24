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
    /// HTML Templates
    /// </summary>
    public static class HtmlTemplates
    {
        #region Private Static Members
        private static string _defaultTagFormat = "<script type=\"text/template\" id=\"{1}\">{0}</script>";
        private static HttpContextBase _context;
        #endregion

        #region Internal Properties
        /// <summary>
        /// Gets or sets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        internal static HttpContextBase Context
        {
            get
            {
                return HtmlTemplates._context ?? (HttpContextBase)new HttpContextWrapper(HttpContext.Current);
            }
            set
            {
                HtmlTemplates._context = value;
            }
        }
        #endregion

        #region Private Static Properties
        /// <summary>
        /// Gets the manager.
        /// </summary>
        /// <value>
        /// The manager.
        /// </value>
        private static AssetManager Manager
        {
            get
            {
                return AssetManager.GetInstance(HtmlTemplates.Context);
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the default format string for defining how script tags are rendered.
        /// </summary>
        /// 
        /// <returns>
        /// The default format string for defining how script tags are rendered.
        /// </returns>
        public static string DefaultTagFormat
        {
            get
            {
                return HtmlTemplates._defaultTagFormat;
            }
            set
            {
                HtmlTemplates._defaultTagFormat = value;
            }
        }
        #endregion

        #region Static Constructor
        /// <summary>
        /// Initializes the <see cref="HtmlTemplates"/> class.
        /// </summary>
        static HtmlTemplates()
        {
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Renders script tags for the following paths.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML string containing the script tag or tags for the bundle.
        /// </returns>
        /// <param name="paths">Set of virtual paths for which to generate script tags.</param>
        public static IHtmlString Render(params string[] paths)
        {
            return HtmlTemplates.RenderFormat(HtmlTemplates.DefaultTagFormat, paths);
        }

        /// <summary>
        /// Renders script tags for a set of paths based on a format string.
        /// </summary>
        /// 
        /// <returns>
        /// The HTML string containing the script tag or tags for the bundle.
        /// </returns>
        /// <param name="tagFormat">The format string for defining the rendered script tags.</param><param name="paths">Set of virtual paths for which to generate script tags.</param>
        public static IHtmlString RenderFormat(string tagFormat, params string[] paths)
        {
            if (string.IsNullOrEmpty(tagFormat))
                throw ExceptionUtil.ParameterNullOrEmpty("tagFormat");
            if (paths == null)
                throw new ArgumentNullException("paths");
            foreach (string str in paths)
            {
                if (string.IsNullOrEmpty(str))
                    throw ExceptionUtil.ParameterNullOrEmpty("paths");
            }
            return HtmlTemplates.Manager.RenderExplicit(tagFormat, paths);
        }

        /// <summary>
        /// Returns a fingerprinted URL if the <paramref name="virtualPath"/> is to a bundle, otherwise returns the resolve URL.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Web.IHtmlString"/> that represents the URL.
        /// </returns>
        /// <param name="virtualPath">The virtual path.</param>
        public static IHtmlString Url(string virtualPath)
        {
            return (IHtmlString)HtmlTemplates.Manager.ResolveUrl(virtualPath);
        }
        #endregion
    }

}

