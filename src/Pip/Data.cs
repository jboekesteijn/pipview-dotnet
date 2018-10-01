using System;

namespace PipView.Pip
{
	public class TrafficData
	{
		public TrafficData()
		{
			PeriodStart = DateTime.MinValue;
			PeriodToday = DateTime.MinValue;
			PeriodEnd = DateTime.MinValue;
		}

		public void Calculate()
		{
			TrafficTodayTotal = TrafficTodayDown + TrafficTodayUp;
			PeriodLeft = (PeriodEnd - PeriodToday).TotalDays;
			PeriodLength = (PeriodEnd - PeriodStart).TotalDays;
			PeriodDone = (PeriodToday - PeriodStart).TotalDays;

			PeriodPercentage = (PeriodLength == 0) ? 0 : (PeriodDone / PeriodLength * 100);
		}

		public DateTime PeriodStart { get; set; }
		public DateTime PeriodToday { get; set; }
		public DateTime PeriodEnd { get; set; }
		public double TrafficTodayDown { get; set; }
		public double TrafficTodayUp{ get; set; }
		public double TrafficPeriodDown { get; set; }
		public double TrafficPeriodUp { get; set; }
		public double TrafficPeriodTotal { get; set; }
		public double TrafficSupernews { get; set; }
		public double TrafficGiganews { get; set; }
		public double TrafficTodayTotal { get; set; }
		public double PeriodLeft { get; set; }
		public double PeriodLength { get; set; }
		public double PeriodDone { get; set; }
		public double PeriodPercentage { get; set; }
	}
}
