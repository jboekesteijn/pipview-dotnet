using System.Collections.Generic;
using System;
using System.Net;
using System.IO;
using PipView.Exceptions;

namespace PipView.Talkback
{
	internal static class Reporter
	{
		internal static void ReportParsingError(string trafficpage)
		{
			List<SimpleMimePart> mimeParts = new List<SimpleMimePart>();

			mimeParts.Add(new SimpleMimePart { Name = "trafficpage", FileName = "trafficpage.html", ContentType = "text/html", Data = trafficpage });

			string boundary = String.Format("pipviewboundary-{0}", DateTime.Now.Ticks);			

			HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri("http://pipview.xxp.nu/talkback/"));
			//HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(new Uri("http://celeron/talkback.php"));

			wr.Method = "POST";
			wr.AllowAutoRedirect = false;
			wr.KeepAlive = false;
            wr.UserAgent = String.Format("PipView/{0}", PipView.VersionInfo);
			wr.ContentType = String.Format("multipart/form-data; boundary={0}", boundary);
			wr.ServicePoint.Expect100Continue = false;

			using (MemoryStream ms = new MemoryStream())
			{
				using (StreamWriter sw = new StreamWriter(ms))
				{
					sw.Write("--" + boundary);

					foreach (SimpleMimePart smp in mimeParts)
					{
						sw.Write("\r\nContent-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n", smp.Name, smp.FileName);
						sw.Write("Content-Type: {0}\r\n\r\n", smp.ContentType);
						sw.Write(smp.Data);

						sw.Write("\r\n" + "--" + boundary);
					}

					sw.Write("--\r\n");
					sw.Flush();

					wr.ContentLength = ms.Length;
					ms.WriteTo(wr.GetRequestStream());
				}
			}

			using (HttpWebResponse res = (HttpWebResponse)wr.GetResponse())
			{
				if (res.StatusCode != HttpStatusCode.OK)
				{
					throw new PipException("Het versturen van een foutenrapport is mislukt (http-aanvraag mislukt). Probeer het later opnieuw of neem contact op met de maker van PipView.");
				}
			}
		}
	}
}
