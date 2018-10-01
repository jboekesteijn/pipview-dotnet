using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using PipView.Extensions;
using PipView.Licensing;

namespace PipView.Configuration
{
	public static class SettingsManager
	{
		private static string configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PipView");
		private static string configFilename = "settings.xml";
		private static string configFilePath = Path.Combine(configDirectory, configFilename);

		public static Settings Load()
		{
			Settings settings = new Settings();

			if (File.Exists(configFilePath))
			{
				XPathDocument xpd = new XPathDocument(configFilePath);
                XPathNavigator doc = xpd.CreateNavigator().SelectSingleNode("/PipViewSettings[@version='1']");

                settings.UserName = doc.SelectSingleNode("Username/text()").Value;
                settings.Password = Convert.FromBase64String(doc.SelectSingleNode("Password/text()").Value);
                settings.PermanentSession = Convert.ToBoolean(doc.SelectSingleNode("PermanentLogin/text()").Value);
                settings.RefreshOnStartup = Convert.ToBoolean(doc.SelectSingleNode("RefreshOnStartup/text()").Value);
                settings.ShowBalloonAfterUpdate = Convert.ToBoolean(doc.SelectSingleNode("ShowBalloonAfterUpdate/text()").Value);
                settings.AutoRefresh = Convert.ToBoolean(doc.SelectSingleNode("AutoRefresh/text()").Value);
                settings.RefreshInterval = Convert.ToInt32(doc.SelectSingleNode("RefreshInterval/text()").Value);
                
                settings.Window.Left = Convert.ToInt32(doc.SelectSingleNode("Window/@Left").Value);
                settings.Window.Top = Convert.ToInt32(doc.SelectSingleNode("Window/@Top").Value);

                foreach (XPathNavigator license in doc.Select("Licenses/License"))
                {
                    License lic = new License();

                    lic.UserName = license.SelectSingleNode("Username/text()").Value;
                    lic.Version = license.SelectSingleNode("Version/text()").Value;
                    lic.Signature = Convert.FromBase64String(license.SelectSingleNode("Signature/text()").Value);

                    settings.Licenses.Add(lic);
                }                    	
			}

			return settings;
		}

		public static void Save(Settings settings)
		{
            if (!Directory.Exists(configDirectory))
            {
                Directory.CreateDirectory(configDirectory);
            }

            new XDocument(
                new XElement("PipViewSettings", new XAttribute("version", "1"),
                    new XElement("Username", settings.UserName),
                    new XElement("Password", Convert.ToBase64String(settings.Password)),
                    new XElement("PermanentLogin", settings.PermanentSession),
                    new XElement("RefreshInterval", settings.RefreshInterval),
                    new XElement("RefreshOnStartup", settings.RefreshOnStartup),
                    new XElement("ShowBalloonAfterUpdate", settings.ShowBalloonAfterUpdate),
                    new XElement("AutoRefresh", settings.AutoRefresh),

                    new XElement("Window", 
                        new XAttribute("Left", settings.Window.Left), 
                        new XAttribute("Top", settings.Window.Top)
                    ),

                    new XElement("Licenses", 
                        from license in settings.Licenses select 
                        new XElement("License",
                            new XElement("Username", license.UserName),
                            new XElement("Version", license.Version),
                            new XElement("Signature", Convert.ToBase64String(license.Signature))
                        )
                    )
                )
            ).Save(configFilePath);
		}
	}
}
