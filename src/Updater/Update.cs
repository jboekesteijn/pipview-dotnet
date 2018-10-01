using System.Net;
using System;
using PipView.Exceptions;
using PipView.Extensions;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

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
            wr.UserAgent = String.Format("PipView/{0}", PipView.VersionInfo);

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode == HttpStatusCode.OK)
				{
                    XPathDocument xpd = new XPathDocument(res.GetResponseStream());
                    XPathNavigator doc = xpd.CreateNavigator().SelectSingleNode("/PipViewUpdate");

                    return new UpdateInfo { 
                        Uri = new Uri(doc.SelectSingleNode("Uri/text()").Value),
                        Version = doc.SelectSingleNode("Version/text()").Value,
                        Hash = doc.SelectSingleNode("Hash/text()").Value
                    };
				}
				else
				{
					throw new PipException("Het controleren op updates van PipView is mislukt (http-aanvraag mislukt). Probeer het later opnieuw of neem contact op met de maker van PipView.");
				}
			}
		}

		internal static string DownloadUpdate(UpdateInfo ui)
		{
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(ui.Uri);

			wr.Method = "GET";
			wr.KeepAlive = false;
			wr.AllowAutoRedirect = false;
            wr.UserAgent = String.Format("PipView/{0}", PipView.VersionInfo);

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
                        
                        if (hash.EqualsArray(givenHash))
						{
							try
							{
                                string tempName = Path.GetTempFileName();

                                using (FileStream fs = new FileStream(tempName, FileMode.Create))
								{
									memoryStream.WriteTo(fs);
								}

                                return tempName;
							}
							catch (Exception)
							{
								throw new PipException("Het opslaan van de PipView-update is mislukt.");
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

		internal static void PerformUpdate(string updateFileName)
		{
            Process updater = new Process { 
                StartInfo = { 
                    FileName = "receive.exe", 
                    UseShellExecute = false, 
                    RedirectStandardInput = true 
                } 
            };       
    
            updater.Start();

            XmlWriter xmlw = XmlWriter.Create(updater.StandardInput.BaseStream);

            new XDocument(
                new XElement("PipViewUpdateInfo", new XAttribute("version", "1"),
                    new XElement("UpdateFileName", updateFileName),
                    new XElement("ProcessHandle", Process.GetCurrentProcess().Id)
                )
            ).Save(xmlw);

            xmlw.Close();
		}
	}
}
