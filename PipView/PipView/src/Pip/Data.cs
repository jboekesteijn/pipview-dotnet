using System;

namespace PipView.Pip
{
	public class TrafficData
	{
		public TrafficData()
		{
			periodStart = DateTime.MinValue;
			periodToday = DateTime.MinValue;
			periodEnd = DateTime.MinValue;
		}

		public void Calculate()
		{
			trafficTodayTotal = trafficTodayDown + trafficTodayUp;
			periodLeft = (periodEnd - periodToday).TotalDays;
			periodLength = (periodEnd - periodStart).TotalDays;
			periodDone = (periodToday - periodStart).TotalDays;

			periodPercentage = (PeriodLength == 0) ? 0 : (PeriodDone / PeriodLength * 100);
		}

		private DateTime periodStart;

		public DateTime PeriodStart
		{
			get { return periodStart; }
			set { periodStart = value; }
		}

		private DateTime periodToday;

		public DateTime PeriodToday
		{
			get { return periodToday; }
			set { periodToday = value; }
		}

		private DateTime periodEnd;

		public DateTime PeriodEnd
		{
			get { return periodEnd; }
			set { periodEnd = value; }
		}

		private double trafficTodayDown;

		public double TrafficTodayDown
		{
			get { return trafficTodayDown; }
			set { trafficTodayDown = value; }
		}

		private double trafficTodayUp;

		public double TrafficTodayUp
		{
			get { return trafficTodayUp; }
			set { trafficTodayUp = value; }
		}

		private double trafficPeriodDown;

		public double TrafficPeriodDown
		{
			get { return trafficPeriodDown; }
			set { trafficPeriodDown = value; }
		}

		private double trafficPeriodUp;

		public double TrafficPeriodUp
		{
			get { return trafficPeriodUp; }
			set { trafficPeriodUp = value; }
		}

		private double trafficPeriodTotal;

		public double TrafficPeriodTotal
		{
			get { return trafficPeriodTotal; }
			set { trafficPeriodTotal = value; }
		}

		private double trafficSupernews;

		public double TrafficSupernews
		{
			get { return trafficSupernews; }
			set { trafficSupernews = value; }
		}

		private double trafficGiganews;

		public double TrafficGiganews
		{
			get { return trafficGiganews; }
			set { trafficGiganews = value; }
		}

		private double trafficTodayTotal;
		
		public double TrafficTodayTotal
		{
		    get { return trafficTodayTotal; }
		}

		private double periodLeft;

		public double PeriodLeft
		{
		    get { return periodLeft; }
		}

		private double periodLength;
		
		public double PeriodLength
		{
		    get { return periodLength; }
		}

		private double periodDone;
		
		public double PeriodDone
		{
		    get { return periodDone; }
		}

		private double periodPercentage;
		
		public double PeriodPercentage
		{
			get { return periodPercentage; }
		}
	}
}
