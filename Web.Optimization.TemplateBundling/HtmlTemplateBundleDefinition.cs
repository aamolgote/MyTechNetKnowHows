using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Html Template Bundle Definition
    /// </summary>
    public sealed class HtmlTemplateBundleDefinition
    {
        #region Properties
        /// <summary>
        /// Gets or sets the virtual path for the bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The virtual path for the bundle.
        /// </returns>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the CDN path for the bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The CDN path for the bundle.
        /// </returns>
        public string CdnPath { get; set; }

        /// <summary>
        /// Gets or sets the CDN fallback expression for the bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The CDN fallback expression for the bundle.
        /// </returns>
        public string CdnFallbackExpression { get; set; }

        /// <summary>
        /// Gets the files included in the bundle.
        /// </summary>
        /// 
        /// <returns>
        /// The files included in the bundle.
        /// </returns>
        public IList<string> Includes { get; set; }
        #endregion
    }
}
