using System.Net;
using System;
using PipView.Exceptions;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml.XPath;

namespace PipView.Updater
{
	internal static class Update
	{
		internal static UpdateInfo GetUpdateInfo()
		{
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri("http://pipview.xxp.nu/update.xml"));

			wr.Method = "GET";
			wr.KeepAlive = false;
			wr.AllowAutoRedirect = false;
			wr.UserAgent = String.Format("PipView/{0}", Program.VersionInfo);

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode == HttpStatusCode.OK)
				{
					XPathDocument xpd = new XPathDocument(res.GetResponseStream());
					XPathNavigator nav = xpd.CreateNavigator();
					XPathNodeIterator xpit;

					UpdateInfo ui = new UpdateInfo();

					xpit = nav.Select("/PipViewUpdate");
					if (xpit.MoveNext())
					{
						XPathNavigator doc = xpit.Current;

						xpit = doc.Select("Uri/text()");
						if (xpit.MoveNext())
						{
							ui.Uri = new Uri(xpit.Current.Value);
						}

						xpit = doc.Select("Version/text()");
						if (xpit.MoveNext())
						{
							ui.Version = xpit.Current.Value;
						}

						xpit = doc.Select("Hash/text()");
						if (xpit.MoveNext())
						{
							ui.Hash = xpit.Current.Value;
						}
					}

					return ui;
				}
				else
				{
					throw new PipException("Het controleren op updates van PipView is mislukt (http-aanvraag mislukt). Probeer het later opnieuw of neem contact op met de maker van PipView.");
				}
			}
		}

		internal static void DownloadUpdate(UpdateInfo ui)
		{
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(ui.Uri);

			wr.Method = "GET";
			wr.KeepAlive = false;
			wr.AllowAutoRedirect = false;
			wr.UserAgent = String.Format("PipView/{0}", Program.VersionInfo);

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode == HttpStatusCode.OK)
				{
					using (MemoryStream memoryStream = new MemoryStream())
					{
						using (Stream responseStream = res.GetResponseStream())
						{
							byte[] buffer = new byte[8192];

							int count = responseStream.Read(buffer, 0, buffer.Length);

							while (count > 0)
							{
								memoryStream.Write(buffer, 0, count);
								count = responseStream.Read(buffer, 0, buffer.Length);
							}
						}

						memoryStream.Position = 0;

						SHA1Managed sha1 = new SHA1Managed();

						byte[] givenHash = Convert.FromBase64String(ui.Hash);
						byte[] hash = sha1.ComputeHash(memoryStream);

						if (Program.ArrayMatch(hash, givenHash))
						{
							try
							{
								using (FileStream fs = new FileStream("pipview.update", FileMode.Create))
								{
									memoryStream.WriteTo(fs);
								}
							}
							catch (Exception)
							{
								throw new PipException("Het opslaan van de PipView-update is mislukt. U heeft geen rechten om bestanden op te slaan in de PipView-map.");
							}
						}
						else
						{
							throw new PipException("Het downloaden van de PipView-update is mislukt (beschadigde download). Probeer het later opnieuw of neem contact op met de maker van PipView.");
						}
					}
				}
				else
				{
					throw new PipException("Het downloaden van de PipView-update is mislukt (http-aanvraag mislukt). Probeer het later opnieuw of neem contact op met de maker van PipView.");
				}
			}
		}

		internal static void PerformUpdate()
		{
			// check to see if an update is available
			if (File.Exists("pipview.update"))
			{
				// start the update-file as a process with our own pid as first (and only) argument
				ProcessStartInfo pi = new ProcessStartInfo();
				pi.FileName = "pipview.update";
				pi.UseShellExecute = false;
				pi.Arguments = String.Format("{0}", Process.GetCurrentProcess().Id);

				Process.Start(pi);

				Application.Exit();
			}
		}
	}
}
