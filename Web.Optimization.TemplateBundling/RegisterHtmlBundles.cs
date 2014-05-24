using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Optimization;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(Web.Optimization.TemplateBundling.RegisterHtmlTemplateBundles), "Register")]
namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Register Html Template Bundles
    /// </summary>
    public class RegisterHtmlTemplateBundles
    {
        #region Static Methods
        /// <summary>
        /// Registers HTML Template Bundles with Bundle Collection
        /// </summary>
        /// <exception cref="System.Exception">Unable to locate htmlTemplateBundle.config file</exception>
        public static void Register()
        {
            if (File.Exists(HttpContext.Current.Server.MapPath(@"/htmlTemplateBundle.config")))
            {
                using (FileStream fileStream = File.OpenRead(HttpContext.Current.Server.MapPath(@"/htmlTemplateBundle.config")))
                {
                    HtmlTemplateBundleManifest.ReadBundleManifest((Stream)fileStream).Register(BundleTable.Bundles);
                }
            }
        }
        #endregion
    }
}
