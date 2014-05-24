using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Optimization Resources
    /// </summary>
    internal class OptimizationResources
    {
        #region Private Static Members
        private static ResourceManager resourceMan;
        private static CultureInfo resourceCulture;
        #endregion

        #region Internal Static Properties
        /// <summary>
        /// Gets the resource manager.
        /// </summary>
        /// <value>
        /// The resource manager.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals((object)OptimizationResources.resourceMan, (object)null))
                    OptimizationResources.resourceMan = new ResourceManager("System.Web.Optimization.Resources.OptimizationResources", typeof(OptimizationResources).Assembly);
                return OptimizationResources.resourceMan;
            }
        }

        /// <summary>
        /// Gets or sets the culture.
        /// </summary>
        /// <value>
        /// The culture.
        /// </value>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return OptimizationResources.resourceCulture;
            }
            set
            {
                OptimizationResources.resourceCulture = value;
            }
        }

        /// <summary>
        /// Gets the bundle directory_does_not_exist.
        /// </summary>
        /// <value>
        /// The bundle directory_does_not_exist.
        /// </value>
        internal static string BundleDirectory_does_not_exist
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("BundleDirectory_does_not_exist", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the CDN fall back script string.
        /// </summary>
        /// <value>
        /// The CDN fall back script string.
        /// </value>
        internal static string CdnFallBackScriptString
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("CdnFallBackScriptString", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the dynamic folder bundle_ invalid path.
        /// </summary>
        /// <value>
        /// The dynamic folder bundle_ invalid path.
        /// </value>
        internal static string DynamicFolderBundle_InvalidPath
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("DynamicFolderBundle_InvalidPath", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the file_does_not_exist.
        /// </summary>
        /// <value>
        /// The file_does_not_exist.
        /// </value>
        internal static string File_does_not_exist
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("File_does_not_exist", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the invalid optimization mode.
        /// </summary>
        /// <value>
        /// The invalid optimization mode.
        /// </value>
        internal static string InvalidOptimizationMode
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("InvalidOptimizationMode", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the invalid pattern.
        /// </summary>
        /// <value>
        /// The invalid pattern.
        /// </value>
        internal static string InvalidPattern
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("InvalidPattern", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the invalid wildcard search pattern.
        /// </summary>
        /// <value>
        /// The invalid wildcard search pattern.
        /// </value>
        internal static string InvalidWildcardSearchPattern
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("InvalidWildcardSearchPattern", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the minify error.
        /// </summary>
        /// <value>
        /// The minify error.
        /// </value>
        internal static string MinifyError
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("MinifyError", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the parameter_ null or empty.
        /// </summary>
        /// <value>
        /// The parameter_ null or empty.
        /// </value>
        internal static string Parameter_NullOrEmpty
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("Parameter_NullOrEmpty", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the property_ null or empty.
        /// </summary>
        /// <value>
        /// The property_ null or empty.
        /// </value>
        internal static string Property_NullOrEmpty
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("Property_NullOrEmpty", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the type_doesnt_inherit_from_type.
        /// </summary>
        /// <value>
        /// The type_doesnt_inherit_from_type.
        /// </value>
        internal static string Type_doesnt_inherit_from_type
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("Type_doesnt_inherit_from_type", OptimizationResources.resourceCulture);
            }
        }

        /// <summary>
        /// Gets the URL mappings_only_app_relative_url_allowed.
        /// </summary>
        /// <value>
        /// The URL mappings_only_app_relative_url_allowed.
        /// </value>
        internal static string UrlMappings_only_app_relative_url_allowed
        {
            get
            {
                return OptimizationResources.ResourceManager.GetString("UrlMappings_only_app_relative_url_allowed", OptimizationResources.resourceCulture);
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OptimizationResources"/> class.
        /// </summary>
        internal OptimizationResources()
        {
        }
        #endregion
    }
}
