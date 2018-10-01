using System.Net;
using System;
using System.Web;
using System.IO;
using PipView.Exceptions;

namespace PipView.Pip
{
	public class Session
	{
		private string username;
		private string password;
		private bool permanentSession;
		private CookieContainer cookies;

		public Session(string username, string password, bool permanentSession)
		{
			this.username = username;
			this.password = password;
			this.permanentSession = permanentSession;
		}

		public string DownloadStatsPage()
		{
			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri("https://secure.zeelandnet.nl/pip/index.php?page=202"));

			wr.Method = "GET";
			wr.KeepAlive = false;
			wr.AllowAutoRedirect = false;
            wr.UserAgent = String.Format("PipView/{0}", PipView.VersionInfo);
			wr.CookieContainer = cookies;

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode == HttpStatusCode.OK)
				{
					using (StreamReader sr = new StreamReader(res.GetResponseStream()))
					{
						return sr.ReadToEnd();
					}
				}
				else
				{
					throw new PipException("Het ophalen van de pagina met verkeersgegevens is mislukt (http-aanvraag mislukt). Probeer het later opnieuw of neem contact op met de maker van PipView.");
				}
			}
		}

		public void SignIn()
		{
			cookies = new CookieContainer();

			string request = String.Format("login_name={0}&login_pass={1}&login_type=abonnee&action=login&from", HttpUtility.UrlEncode(username), HttpUtility.UrlEncode(password));

			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri("https://secure.zeelandnet.nl/login/index.php"));

			wr.Method = "POST";
			wr.KeepAlive = false;
			wr.AllowAutoRedirect = false;
			wr.ContentLength = request.Length;
			wr.ContentType = "application/x-www-form-urlencoded";
			wr.Referer = "https://secure.zeelandnet.nl/login/index.php";
            wr.UserAgent = String.Format("PipView/{0}", PipView.VersionInfo);
			wr.CookieContainer = cookies;
			wr.ServicePoint.Expect100Continue = false;

			using (StreamWriter sw = new StreamWriter(wr.GetRequestStream()))
			{
				sw.Write(request);
			}

			using (HttpWebResponse hwr = (HttpWebResponse)wr.GetResponse())
			{
				bool success = (hwr.StatusCode == HttpStatusCode.Redirect);

				if (!success)
				{
					throw new PipException("Het inloggen is mislukt. Controleer uw loginnaam en wachtwoord.");
				}
				else if (success && permanentSession)
				{
					CookieCollection cc = cookies.GetCookies(new Uri("https://secure.zeelandnet.nl/login/index.php"));

					foreach (Cookie c in cc)
					{
						NativeMethods.InternetSetCookie
						(
							"https://secure.zeelandnet.nl/login/index.php",
							null,
							String.Format("{0}={1}; path={2}; domain={3}; expires=Wed, 29-11-2084 12:00:00 GMT", c.Name, c.Value, c.Path, c.Domain)
						);
					}
				}
			}
		}
	}
}
