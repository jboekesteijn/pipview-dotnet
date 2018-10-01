using System.Net;
using System;
using System.Web;
using PipView.Exceptions;
using System.Xml.XPath;

namespace PipView.Licensing
{
	public class License
	{
		public string UserName { get; set; }
		public string Version { get; set; }
		public byte[] Signature { get; set; }

		public bool IsValid()
		{
			return LicenseValidator.VerifySignature(UserName + Version, Signature);
		}

		public static License Download(string userName, string version)
		{
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri(String.Format("http://pipview.xxp.nu/license.xml?username={0}&version={1}", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(version))));

			wr.Method = "GET";
			wr.AllowAutoRedirect = false;
			wr.KeepAlive = false;
            wr.UserAgent = String.Format("PipView/{0}", PipView.VersionInfo);

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode == HttpStatusCode.OK)
				{
					XPathDocument xpd = new XPathDocument(res.GetResponseStream());
                    XPathNavigator doc = xpd.CreateNavigator().SelectSingleNode("/PipViewLicense");

                    License l = new License();

                    l.UserName = doc.SelectSingleNode("Username/text()").Value;
                    l.Version = doc.SelectSingleNode("Version/text()").Value;
                    l.Signature = Convert.FromBase64String(doc.SelectSingleNode("Signature/text()").Value);	
					
					if ((l.UserName == userName) && (l.Version == version) && l.IsValid())
					{
						return l;
					}
					else
					{
						throw new PipException("Het activeren van PipView is mislukt (ongeldige licentie). Probeer het later opnieuw of neem contact op met de maker van PipView.");
					}
				}
				else
				{
					throw new PipException("Het activeren van PipView is mislukt (http-aanvraag mislukt). Probeer het later opnieuw of neem contact op met de maker van PipView.");
				}
			}
		}
	}
}
