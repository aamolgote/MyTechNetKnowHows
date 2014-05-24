using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// HTML Template Minify
    /// </summary>
    public class HtmlTemplateMinify : IBundleTransform
    {
        #region Internal Static Members
        internal static string HtmlTemplateContentType = "script/template";
        internal static readonly HtmlTemplateMinify Instance = new HtmlTemplateMinify();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the <see cref="HtmlTemplateMinify"/> class.
        /// </summary>
        static HtmlTemplateMinify()
        {
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Generates the error response.
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        /// <param name="errors">The errors.</param>
        internal static void GenerateErrorResponse(BundleResponse bundle, IEnumerable<object> errors)
        {
            //StringBuilder stringBuilder = new StringBuilder();
            //stringBuilder.Append("/* ");
            //stringBuilder.Append(OptimizationResources.MinifyError).Append("\r\n");
            //foreach (object obj in errors)
            //    stringBuilder.Append(obj.ToString()).Append("\r\n");
            //stringBuilder.Append(" */\r\n");
            //stringBuilder.Append(bundle.Content);
            //bundle.Content = ((object)stringBuilder).ToString();
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Transforms the bundle contents by applying javascript minification.
        /// </summary>
        /// <param name="context">The context associated with the bundle.</param><param name="response">The <see cref="T:System.Web.Optimization.BundleResponse"/>.</param>
        public virtual void Process(BundleContext context, BundleResponse response)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (response == null)
                throw new ArgumentNullException("response");
            if (!context.EnableInstrumentation)
            {
                //Minifier minifier1 = new Minifier();
                //Minifier minifier2 = minifier1;
                //string content = response.Content;
                //CodeSettings codeSettings1 = new CodeSettings();
                //codeSettings1.set_EvalTreatment((EvalTreatment)1);
                //codeSettings1.set_PreserveImportantComments(false);
                //CodeSettings codeSettings2 = codeSettings1;
                //string str = minifier2.MinifyJavaScript(content, codeSettings2);
                //if (minifier1.get_ErrorList().Count > 0)
                //    JsMinify.GenerateErrorResponse(response, (IEnumerable<object>)minifier1.get_ErrorList());
                //else
                //    response.Content = str;
            }
            response.ContentType = HtmlTemplateMinify.HtmlTemplateContentType;
        }
        #endregion
    }
}
