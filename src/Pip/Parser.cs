using System;
using PipView.Exceptions;
using System.Text.RegularExpressions;

namespace PipView.Pip
{
	public class Parser
	{
		private string trafficPage;

		private ParserException GenerateParserException()
		{
			ParserException pe = new ParserException("Fout tijdens het verwerken van de gegevens.");

			pe.Data["TrafficPage"] = trafficPage;

			return pe;
		}

		public Parser(string trafficpage)
		{
			trafficPage = trafficpage;
		}

		public string TrafficPage
		{
			get { return trafficPage; }
		}

		/// <param name="d">Day.</param>
		/// <param name="m">Month.</param>
		/// <param name="y">Year.</param>
		/// <returns>Returns a valid DateTime-object constructed from the given arguments.</returns>
		private DateTime ParseDateString(string y, string m, string d)
		{
            int month = DutchMonthNames.GetMonthNumber(m);

			if (month != -1)
			{
				return new DateTime(Convert.ToInt32(y), month + 1, Convert.ToInt32(d));
			}
			else
			{
				throw GenerateParserException();
			}
		}

		/// <param name="ts">Traffic with traffic like '999.999 Mb' or '99,99 MB'.</param>
		/// <returns>Returns the traffic as a Double variable.</returns>
		private Double ParseTrafficString(string ts)
		{
			ts = ts.Replace(".", "");

			if (new Regex(@"^([\d.,]+) (?:bytes|kb)$", RegexOptions.IgnoreCase).IsMatch(ts))
			{
				return 0;
			}

			Match m = new Regex(@"^([\d]+) mb$", RegexOptions.IgnoreCase).Match(ts);

			if (m.Success)
			{
				return Convert.ToDouble(m.Groups[1].Value);
			}

			m = new Regex(@"^([\d]+),([\d]+) mb$", RegexOptions.IgnoreCase).Match(ts);

			if (m.Success)
			{
				return Convert.ToDouble(m.Groups[1].Value) + Convert.ToDouble(m.Groups[2].Value) / Math.Pow(10, m.Groups[2].Length);
			}
			else
			{
				throw GenerateParserException();
			}
		}

		public TrafficData Parse()
		{
			Match m;
			TrafficData pipdata = new TrafficData();

			m = new Regex(@"(\d{1,2}) (\w*) (\d{4}) t/m (\d{1,2}) (\w*) (\d{4})").Match(trafficPage);

			if (m.Success)
			{
				pipdata.PeriodStart = ParseDateString(m.Groups[3].Value, m.Groups[2].Value, m.Groups[1].Value);
				pipdata.PeriodToday = DateTime.Now;
				pipdata.PeriodEnd = ParseDateString(m.Groups[6].Value, m.Groups[5].Value, m.Groups[4].Value).AddSeconds(86399);

				if (pipdata.PeriodEnd < pipdata.PeriodToday || pipdata.PeriodToday < pipdata.PeriodStart)
				{
					throw new PipException("De datum van uw computer valt niet binnen de huidige periode. Controleer de instellingen.");
				}
			}
			else
			{
				throw GenerateParserException();
			}

			m = new Regex(@"<td bgcolor=""#FFFFFF"">&nbsp;&nbsp;([\d.,]+ \w+)</td>").Match(trafficPage);

			if (m.Success)
			{
				pipdata.TrafficTodayDown += ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = m.NextMatch();

			if (m.Success)
			{
				pipdata.TrafficTodayDown += ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = m.NextMatch();

			if (m.Success)
			{
				pipdata.TrafficTodayUp += ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = m.NextMatch();

			if (m.Success)
			{
				pipdata.TrafficTodayUp += ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = new Regex(@"<td align=""center"">([\d,]* [\w]*)</td>\r\n\t</tr>").Match(trafficPage);

			if (m.Success)
			{
				pipdata.TrafficPeriodDown = ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = m.NextMatch();

			if (m.Success)
			{
				pipdata.TrafficPeriodUp = ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = new Regex(@"<td align=""center""><B>([\d,]* [\w]*)</B></td>\r\n\t</tr>").Match(trafficPage);

			if (m.Success)
			{
				pipdata.TrafficPeriodTotal = ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				throw GenerateParserException();
			}

			m = new Regex(@"Giganews(?:.*?)<B>([\d.,]+ \w+)</B>", RegexOptions.Singleline).Match(trafficPage);

			if (m.Success)
			{
				pipdata.TrafficGiganews = ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				pipdata.TrafficGiganews = 0;
			}

			m = new Regex(@"Supernews(?:.*?)<B>([\d.,]+ \w+)</B>", RegexOptions.Singleline).Match(trafficPage);

			if (m.Success)
			{
				pipdata.TrafficSupernews = ParseTrafficString(m.Groups[1].Value);
			}
			else
			{
				pipdata.TrafficSupernews = 0;
			}

			pipdata.Calculate();

			return pipdata;
		}
	}
}
