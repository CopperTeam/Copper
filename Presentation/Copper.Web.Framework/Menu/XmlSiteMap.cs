//code from Telerik MVC Extensions

using System;
using System.IO;
using System.Linq;
using System.Xml;
using Copper.Core;
using Copper.Core.Infrastructure;
using Copper.Services.Localization;

namespace Copper.Web.Framework.Menu
{
    public class XmlSiteMap
    {
        public XmlSiteMap()
        {
            RootNode = new SiteMapNode();
        }

        public SiteMapNode RootNode { get; set; }

        public virtual void LoadFrom(string physicalPath)
        {
            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            string filePath = webHelper.MapPath(physicalPath);
            string content = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(content))
            {
                using (var sr = new StringReader(content))
                {
                    using (var xr = XmlReader.Create(sr,
                            new XmlReaderSettings
                            {
                                CloseInput = true,
                                IgnoreWhitespace = true,
                                IgnoreComments = true,
                                IgnoreProcessingInstructions = true
                            }))
                    {
                        var doc = new XmlDocument();
                        doc.Load(xr);

                        if ((doc.DocumentElement != null) && doc.HasChildNodes)
                        {
                            XmlNode xmlRootNode = doc.DocumentElement.FirstChild;
                            Iterate(RootNode, xmlRootNode);
                        }
                    }
                }
            }
        }

        private static void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            PopulateNode(siteMapNode, xmlNode);
            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);

                    Iterate(siteMapChildNode, xmlChildNode);
                }
            }
        }

        private static void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode)
        {
            //title
            var CopperResource = GetStringValueFromAttribute(xmlNode, "CopperResource");
            siteMapNode.Title = GetStringValueFromAttribute(xmlNode, "title");
            if (!string.IsNullOrEmpty(CopperResource))
            {
                var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
                var title = localizationService.GetResource(CopperResource);
                if (!string.IsNullOrEmpty(title))
                {
                    siteMapNode.Title = title;
                }
            }

            //routes, url
            var area = siteMapNode.ImageUrl = GetStringValueFromAttribute(xmlNode, "Area");
            var controllerName = GetStringValueFromAttribute(xmlNode, "Controller");
            var actionName = GetStringValueFromAttribute(xmlNode, "Action");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.AreaName = area;
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;
            }

            //image URL
            siteMapNode.ImageUrl = GetStringValueFromAttribute(xmlNode, "ImageUrl");

            //permission name
            var permissionNames = GetStringValueFromAttribute(xmlNode, "PermissionNames");
            if (!string.IsNullOrEmpty(permissionNames))
            {

            }
            else
            {
                siteMapNode.Visible = true;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                XmlAttribute attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
    }
}
