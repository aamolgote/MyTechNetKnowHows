using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Exception Utility Class
    /// </summary>
    internal static class ExceptionUtil
    {
        #region Internal Static Methods
        /// <summary>
        /// Parameters the null or empty.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        internal static ArgumentException ParameterNullOrEmpty(string parameter)
        {
            return new ArgumentException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, OptimizationResources.Parameter_NullOrEmpty, new object[1]
              {
                (object) parameter
              }), parameter);
        }

        /// <summary>
        /// Properties the null or empty.
        /// </summary>
        /// <param name="property">The property.</param>
        /// <returns></returns>
        internal static ArgumentException PropertyNullOrEmpty(string property)
        {
            return new ArgumentException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, OptimizationResources.Property_NullOrEmpty, new object[1]
              {
                (object) property
              }), property);
        }

        /// <summary>
        /// Validates the virtual path.
        /// </summary>
        /// <param name="virtualPath">The virtual path.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <returns></returns>
        internal static Exception ValidateVirtualPath(string virtualPath, string argumentName)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return (Exception)ExceptionUtil.ParameterNullOrEmpty(argumentName);
            if (virtualPath.StartsWith("~/", StringComparison.OrdinalIgnoreCase))
                return (Exception)null;
            return (Exception)new ArgumentException(string.Format((IFormatProvider)CultureInfo.CurrentCulture, OptimizationResources.UrlMappings_only_app_relative_url_allowed, new object[1]
              {
                (object) virtualPath
              }), argumentName);
        }

        /// <summary>
        /// Determines whether [is pure wildcard search pattern] [the specified search pattern].
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        internal static bool IsPureWildcardSearchPattern(string searchPattern)
        {
            if (!string.IsNullOrEmpty(searchPattern))
            {
                string a = searchPattern.Trim();
                if (string.Equals(a, "*", StringComparison.OrdinalIgnoreCase) || string.Equals(a, "*.*", StringComparison.OrdinalIgnoreCase))
                    return true;
            }
            return false;
        }
        #endregion
    }
}
