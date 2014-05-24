using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// URL Utility 
    /// </summary>
    internal static class UrlUtil
    {
        #region Internal Static Methods
        /// <summary>
        /// URLs the specified base path.
        /// </summary>
        /// <param name="basePath">The base path.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        internal static string Url(string basePath, string path)
        {
            if (basePath != null)
                path = VirtualPathUtility.Combine(basePath, path);
            path = VirtualPathUtility.ToAbsolute(path);
            return HttpUtility.UrlPathEncode(path);
        }
        #endregion
    }
}
