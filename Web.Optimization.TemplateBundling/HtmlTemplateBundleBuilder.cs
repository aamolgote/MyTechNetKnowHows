using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Optimization;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Html Template Bundle Builder
    /// </summary>
    public class HtmlTemplateBundleBuilder : IBundleBuilder
    {
        #region Private Members
        internal static IBundleBuilder Instance = (IBundleBuilder)new HtmlTemplateBundleBuilder();
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the <see cref="HtmlTemplateBundleBuilder"/> class.
        /// </summary>
        static HtmlTemplateBundleBuilder()
        {
        }
        #endregion

        #region Private Functions
        /// <summary>
        /// Gets the instrumented bundle preamble.
        /// </summary>
        /// <param name="boundaryValue">The boundary value.</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetInstrumentedBundlePreamble(string boundaryValue)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            dictionary["Bundle"] = "System.Web.Optimization.Bundle";
            dictionary["Boundary"] = boundaryValue;
            return dictionary;
        }

        //private static string GetBoundaryIdentifier(Bundle bundle)
        //{
        //    return Convert.ToBase64String(Encoding.Unicode.GetBytes((bundle.Transforms == null || bundle.Transforms.Count <= 0 ? typeof(DefaultTransform) : bundle.Transforms[0].GetType()).FullName.GetHashCode().ToString((IFormatProvider)CultureInfo.InvariantCulture)));
        //}

        /// <summary>
        /// Gets the instrumented file header format.
        /// </summary>
        /// <param name="boundaryValue">The boundary value.</param>
        /// <returns></returns>
        private static string GetInstrumentedFileHeaderFormat(string boundaryValue)
        {
            return "/* " + boundaryValue + " \"{0}\" */";
        }

        /// <summary>
        /// Converts to application relative path.
        /// </summary>
        /// <param name="appPath">The application path.</param>
        /// <param name="fullName">The full name.</param>
        /// <returns></returns>
        internal static string ConvertToAppRelativePath(string appPath, string fullName)
        {
            if (string.Equals("/", appPath, StringComparison.OrdinalIgnoreCase))
                return fullName;
            else
                return (string.IsNullOrEmpty(appPath) || !fullName.StartsWith(appPath, StringComparison.OrdinalIgnoreCase) ? fullName : fullName.Replace(appPath, "~/")).Replace('\\', '/');
        }

        /// <summary>
        /// Gets the application path.
        /// </summary>
        /// <param name="vpp">The VPP.</param>
        /// <returns></returns>
        private static string GetApplicationPath(VirtualPathProvider vpp)
        {
            if (vpp != null && vpp.DirectoryExists("~"))
            {
                VirtualDirectory directory = vpp.GetDirectory("~");
                if (directory != null)
                    return directory.VirtualPath;
            }
            return (string)null;
        }

        /// <summary>
        /// Gets the file header.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="file">The file.</param>
        /// <param name="fileHeaderFormat">The file header format.</param>
        /// <returns></returns>
        private static string GetFileHeader(BundleContext context, VirtualFile file, string fileHeaderFormat)
        {
            if (string.IsNullOrEmpty(fileHeaderFormat))
                return string.Empty;
            string applicationPath = HtmlTemplateBundleBuilder.GetApplicationPath(BundleTable.VirtualPathProvider);
            return string.Format((IFormatProvider)CultureInfo.InvariantCulture, fileHeaderFormat, new object[1]
              {
                (object) HtmlTemplateBundleBuilder.ConvertToAppRelativePath(applicationPath, file.VirtualPath)
              }) + "\r\n";
        }

        /// <summary>
        /// Generates the bundle preamble.
        /// </summary>
        /// <param name="bundleHash">The bundle hash.</param>
        /// <returns></returns>
        private static string GenerateBundlePreamble(string bundleHash)
        {
            Dictionary<string, string> instrumentedBundlePreamble = HtmlTemplateBundleBuilder.GetInstrumentedBundlePreamble(bundleHash);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/* ");
            foreach (string index in instrumentedBundlePreamble.Keys)
                stringBuilder.Append(index + "=" + instrumentedBundlePreamble[index] + ";");
            stringBuilder.Append(" */");
            return ((object)stringBuilder).ToString();
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Builds the content of the bundle.
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        /// <param name="context">The context.</param>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// context
        /// or
        /// bundle
        /// </exception>
        public string BuildBundleContent(Bundle bundle, BundleContext context, IEnumerable<BundleFile> files)
        {
            if (files == null)
                return string.Empty;
            if (context == null)
                throw new ArgumentNullException("context");
            if (bundle == null)
                throw new ArgumentNullException("bundle");
            StringBuilder stringBuilder = new StringBuilder();
            string str1 = "";
            if (context.EnableInstrumentation)
            {
                //str1 = HtmlTemplateBundleBuilder.GetBoundaryIdentifier(bundle);
                stringBuilder.AppendLine(HtmlTemplateBundleBuilder.GenerateBundlePreamble(str1));
            }
            string str2 = (string)null;
            if (!string.IsNullOrEmpty(bundle.ConcatenationToken))
            {
                str2 = bundle.ConcatenationToken;
            }
            else
            {
                foreach (object obj in (IEnumerable<IBundleTransform>)bundle.Transforms)
                {
                    if (typeof(HtmlTemplateMinify).IsAssignableFrom(obj.GetType()))
                    {
                        str2 = ";" + Environment.NewLine;
                        break;
                    }
                }
            }
            if (str2 == null || context.EnableInstrumentation)
                str2 = Environment.NewLine;
            string tagFormat = "<script type=\"text/template\" id=\"{1}\">{0}</script>";
            foreach (BundleFile bundleFile in files)
            {
                if (context.EnableInstrumentation)
                    stringBuilder.Append(HtmlTemplateBundleBuilder.GetFileHeader(context, bundleFile.VirtualFile, HtmlTemplateBundleBuilder.GetInstrumentedFileHeaderFormat(str1)));
                string fileContent = bundleFile.ApplyTransforms();
                fileContent = string.Format((IFormatProvider)CultureInfo.InvariantCulture, tagFormat, fileContent, bundleFile.VirtualFile.VirtualPath);
                stringBuilder.Append(fileContent);
                stringBuilder.Append(str2);
            }
            return ((object)stringBuilder).ToString();
        }
        #endregion
    }
}
