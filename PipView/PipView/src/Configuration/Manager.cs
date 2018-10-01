using System.Xml;
using System.IO;
using System;
using System.Xml.XPath;
using System.Windows.Forms;
using PipView.Licensing;

namespace PipView.Configuration
{
	public static class Manager
	{
		private static string configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PipView");
		private static string configFilename = "settings.xml";
		private static string configFilePath = Path.Combine(configDirectory, configFilename);

		public static Settings Load()
		{
			Settings settings = new Settings();

			if (File.Exists(configFilePath))
			{
				try
				{
					XPathDocument xpd = new XPathDocument(configFilePath);
					XPathNavigator nav = xpd.CreateNavigator();
					XPathNodeIterator xpit;

					xpit = nav.Select("/PipViewSettings[@version='1']");
					if (xpit.MoveNext())
					{
						XPathNavigator doc = xpit.Current;

						xpit = doc.Select("Username/text()");
						if (xpit.MoveNext())
						{
							settings.UserName = xpit.Current.Value;
						}

						xpit = doc.Select("Password/text()");
						if (xpit.MoveNext())
						{
							settings.SetPasswordBytes(Convert.FromBase64String(xpit.Current.Value));
						}

						xpit = doc.Select("PermanentLogin/text()");
						if (xpit.MoveNext())
						{
							settings.PermanentSession = Convert.ToBoolean(xpit.Current.Value);
						}

						xpit = doc.Select("RefreshOnStartup/text()");
						if (xpit.MoveNext())
						{
							settings.RefreshOnStartup = Convert.ToBoolean(xpit.Current.Value);
						}

						xpit = doc.Select("ShowBalloonAfterUpdate/text()");
						if (xpit.MoveNext())
						{
							settings.ShowBalloonAfterUpdate = Convert.ToBoolean(xpit.Current.Value);
						}

						xpit = doc.Select("AutoRefresh/text()");
						if (xpit.MoveNext())
						{
							settings.AutoRefresh = Convert.ToBoolean(xpit.Current.Value);
						}

						xpit = doc.Select("RefreshInterval/text()");
						if (xpit.MoveNext())
						{
							settings.RefreshInterval = Convert.ToInt32(xpit.Current.Value);
						}

						xpit = doc.Select("Window/@Left");
						if (xpit.MoveNext())
						{
							settings.Window.Left = Convert.ToInt32(xpit.Current.Value);
						}

						xpit = doc.Select("Window/@Top");
						if (xpit.MoveNext())
						{
							settings.Window.Top = Convert.ToInt32(xpit.Current.Value);
						}

						XPathNodeIterator lics = doc.Select("Licenses/License");

						foreach (XPathNavigator licdoc in lics)
						{
							License lic = new License();

							xpit = licdoc.Select("Username/text()");
							if (xpit.MoveNext())
							{
								lic.UserName = xpit.Current.Value;
							}

							xpit = licdoc.Select("Version/text()");
							if (xpit.MoveNext())
							{
								lic.Version = xpit.Current.Value;
							}

							xpit = licdoc.Select("Signature/text()");
							if (xpit.MoveNext())
							{
								lic.SetSignatureBytes(Convert.FromBase64String(xpit.Current.Value));
							}

							settings.Licenses.Add(lic);
						}
					}
				}
				catch (Exception)
				{
					MessageBox.Show("Er is een fout opgetreden tijdens het laden van de instellingen van PipView. De instellingen zijn hersteld naar de standaardwaarden.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);

					return new Settings();
				}
			}

			return settings;
		}

		private static void AddTextChild(XmlDocument doc, XmlElement root, string Name, string Value)
		{
			XmlElement node = doc.CreateElement(Name);
			node.AppendChild(doc.CreateTextNode(Value));

			root.AppendChild(node);
		}

		public static void Save(Settings settings)
		{
			XmlElement node, root, licenses, license;

			if (!Directory.Exists(configDirectory))
			{
				Directory.CreateDirectory(configDirectory);
			}

			XmlDocument doc = new XmlDocument();

			root = doc.CreateElement("PipViewSettings");
			root.SetAttribute("version", "1");
			doc.AppendChild(root);

			AddTextChild(doc, root, "Username", settings.UserName);
			AddTextChild(doc, root, "Password", Convert.ToBase64String(settings.GetPasswordBytes()));
			AddTextChild(doc, root, "PermanentLogin", Convert.ToString(settings.PermanentSession));
			AddTextChild(doc, root, "RefreshInterval", Convert.ToString(settings.RefreshInterval));
			AddTextChild(doc, root, "RefreshOnStartup", Convert.ToString(settings.RefreshOnStartup));
			AddTextChild(doc, root, "ShowBalloonAfterUpdate", Convert.ToString(settings.ShowBalloonAfterUpdate));
			AddTextChild(doc, root, "AutoRefresh", Convert.ToString(settings.AutoRefresh));

			node = doc.CreateElement("Window");
			node.SetAttribute("Left", Convert.ToString(settings.Window.Left));
			node.SetAttribute("Top", Convert.ToString(settings.Window.Top));
			root.AppendChild(node);

			licenses = doc.CreateElement("Licenses");
			root.AppendChild(licenses);

			foreach (License lic in settings.Licenses)
			{
				license = doc.CreateElement("License");

				AddTextChild(doc, license, "Username", lic.UserName);
				AddTextChild(doc, license, "Version", lic.Version);
				AddTextChild(doc, license, "Signature", Convert.ToBase64String(lic.GetSignatureBytes()));

				licenses.AppendChild(license);
			}

			XmlWriterSettings xws = new XmlWriterSettings();
			xws.Indent = true;

			using (XmlWriter writer = XmlWriter.Create(configFilePath, xws))
			{
				doc.Save(writer);
			}
		}
	}
}
