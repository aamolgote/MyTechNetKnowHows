using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Optimization;
using System.Xml;
using System.Xml.Schema;

namespace Web.Optimization.TemplateBundling
{
    /// <summary>
    /// Html Template Bundle Manifest
    /// </summary>
    public class HtmlTemplateBundleManifest
    {
        #region Constants
        private const string XsdResourceName = "ML.Web.Optimization.HtmlTemplateBundleManifestSchema.xsd";
        private const string DefaultBundlePath = "~/htmlTemplateBundle.config";
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the registered style bundles.
        /// </summary>
        /// 
        /// <returns>
        /// The registered style bundles.
        /// </returns>
        public IList<HtmlTemplateBundleDefinition> HtmlTemplateBundles { get; private set; }
        #endregion

        #region Static Properties
        /// <summary>
        /// Gets or sets the path to the bundle manifest file that sets up the <see cref="T:System.Web.Optimization.BundleCollection"/>.
        /// </summary>
        /// 
        /// <returns>
        /// The path to the bundle manifest file that sets up the <see cref="T:System.Web.Optimization.BundleCollection"/>.
        /// </returns>
        public static string BundleManifestPath
        {
            get
            {
                return "~/htmlTemplateBundle.config";
            }
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Prevents a default instance of the <see cref="HtmlTemplateBundleManifest"/> class from being created.
        /// </summary>
        private HtmlTemplateBundleManifest()
        {
        }
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Reads the bundle manifest from a given stream.
        /// </summary>
        /// 
        /// <returns>
        /// The bundle manifest.
        /// </returns>
        /// <param name="bundleStream">The bundle stream to read from.</param>
        public static HtmlTemplateBundleManifest ReadBundleManifest(Stream bundleStream)
        {
            XmlDocument xmlDocument = HtmlTemplateBundleManifest.GetXmlDocument(bundleStream);
            return new HtmlTemplateBundleManifest()
            {
                HtmlTemplateBundles = (IList<HtmlTemplateBundleDefinition>)Enumerable.ToList<HtmlTemplateBundleDefinition>(Enumerable.Select<XmlElement, HtmlTemplateBundleDefinition>(Enumerable.Cast<XmlElement>((IEnumerable)xmlDocument.SelectNodes("bundles/htmlTemplateBundle")), new Func<XmlElement, HtmlTemplateBundleDefinition>(HtmlTemplateBundleManifest.ReadBundle))),
            };

        }
        

        /// <summary>
        /// Reads the bundle manifest using the default bundle configuration.
        /// </summary>
        /// 
        /// <returns>
        /// The bundle manifest.
        /// </returns>
        public static HtmlTemplateBundleManifest ReadBundleManifest()
        {
            return HtmlTemplateBundleManifest.ReadBundleManifest(BundleTable.VirtualPathProvider);
        }
        #endregion

        #region Internal Static Methods
        /// <summary>
        /// Reads the bundle manifest.
        /// </summary>
        /// <param name="vpp">The VPP.</param>
        /// <returns></returns>
        internal static HtmlTemplateBundleManifest ReadBundleManifest(VirtualPathProvider vpp)
        {
            if (vpp == null)
                return (HtmlTemplateBundleManifest)null;
            if (!vpp.FileExists(HtmlTemplateBundleManifest.BundleManifestPath))
                return (HtmlTemplateBundleManifest)null;
            using (Stream bundleStream = vpp.GetFile(HtmlTemplateBundleManifest.BundleManifestPath).Open())
                return HtmlTemplateBundleManifest.ReadBundleManifest(bundleStream);
        }
        #endregion

        #region Private Static Methods
        /// <summary>
        /// Gets the XML document.
        /// </summary>
        /// <param name="bundleStream">The bundle stream.</param>
        /// <returns></returns>
        private static XmlDocument GetXmlDocument(Stream bundleStream)
        {
            XmlDocument xmlDocument = new XmlDocument();
            using (Stream manifestResourceStream = typeof(HtmlTemplateBundleManifest).Assembly.GetManifestResourceStream(string.Format("{0}.HtmlTemplateBundleManifestSchema.xsd", typeof(HtmlTemplateBundleManifest).Assembly.GetName().Name)))
            {
                using (XmlReader schemaDocument = XmlReader.Create(manifestResourceStream))
                    xmlDocument.Schemas.Add((string)null, schemaDocument);
            }
            xmlDocument.Load(bundleStream);
            xmlDocument.Validate((ValidationEventHandler)((sender, e) =>
            {
                if (e.Severity == XmlSeverityType.Error)
                    throw new InvalidOperationException(e.Message);
            }));
            return xmlDocument;
        }

        /// <summary>
        /// Reads the bundle.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        private static HtmlTemplateBundleDefinition ReadBundle(XmlElement element)
        {
            return new HtmlTemplateBundleDefinition()
            {
                Path = element.GetAttribute("path"),
                CdnPath = element.GetAttribute("cdnPath"),
                CdnFallbackExpression = element.GetAttribute("cdnFallbackExpression"),
                Includes = (IList<string>)Enumerable.ToList<string>(Enumerable.Select<XmlElement, string>(Enumerable.Cast<XmlElement>((IEnumerable)element.GetElementsByTagName("include")), (Func<XmlElement, string>)(s => s.GetAttribute("path"))))
            };
        }
        #endregion

        #region Public Members
        /// <summary>
        /// Registers the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public void Register(BundleCollection collection)
        {
            foreach (HtmlTemplateBundleDefinition bundleDefinition in (IEnumerable<HtmlTemplateBundleDefinition>)this.HtmlTemplateBundles)
            {
                HtmlTemplateBundle htmlTemplateBundle = new HtmlTemplateBundle(bundleDefinition.Path);
                htmlTemplateBundle.Include(Enumerable.ToArray<string>((IEnumerable<string>)bundleDefinition.Includes));
                collection.Add((Bundle)htmlTemplateBundle);
            }
        }
        #endregion
    }
}
