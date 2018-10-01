using System.Net;
using System;
using System.Web;
using PipView.Exceptions;
using System.Xml.XPath;

namespace PipView.Licensing
{
	public class License
	{
		private string userName = "";

		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		private string version = "";

		public string Version
		{
			get { return version; }
			set { version = value; }
		}

		private byte[] signature = { };

		public byte[] GetSignatureBytes()
		{
			return (byte[])signature.Clone();
		}

		public void SetSignatureBytes(byte[] signature)
		{
			this.signature = signature;
		}

		public bool IsValid()
		{
			return LicenseValidator.VerifySignature(userName + version, signature);
		}

		public static License Download(string userName, string version)
		{
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri(String.Format("http://pipview.xxp.nu/license.xml?username={0}&version={1}", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(version))));

			wr.Method = "GET";
			wr.AllowAutoRedirect = false;
			wr.KeepAlive = false;
			wr.UserAgent = String.Format("PipView/{0}", Program.VersionInfo);

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode == HttpStatusCode.OK)
				{
					XPathDocument xpd = new XPathDocument(res.GetResponseStream());
					XPathNavigator nav = xpd.CreateNavigator();
					XPathNodeIterator xpit;

					License l = new License();

					xpit = nav.Select("/PipViewLicense");
					if (xpit.MoveNext())
					{
						XPathNavigator doc = xpit.Current;

						xpit = doc.Select("Username/text()");
						if (xpit.MoveNext())
						{
							l.UserName = xpit.Current.Value;
						}

						xpit = doc.Select("Version/text()");
						if (xpit.MoveNext())
						{
							l.Version = xpit.Current.Value;
						}

						xpit = doc.Select("Signature/text()");
						if (xpit.MoveNext())
						{
							l.SetSignatureBytes(Convert.FromBase64String(xpit.Current.Value));
						}
					}
					
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
