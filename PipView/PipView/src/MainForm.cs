using System.Windows.Forms;
using System;
using System.Drawing;
using PipView.Pip;

namespace PipView
{
	public partial class MainForm : Form
	{
		public event EventHandler RefreshData;
		public event EventHandler ShowBalloon;
		public event EventHandler Options;
		public event EventHandler PipInBrowser;
		public event EventHandler Exit;
		public event EventHandler Help;
		public event EventHandler About;
		public event EventHandler CheckUpdates;
		public event EventHandler Website;

		private SolidBrush evenBrush;
		private SolidBrush oddBrush;

		public MainForm()
		{
			InitializeComponent();

			// menu creation
			MenuItem mnuMainPipView = new MenuItem("&PipView");
			mnuMainPipView.MenuItems.Add(new MenuItem("Gegevens vernieuwen", new EventHandler(mnuRefreshData_Click), Shortcut.F5));
			mnuMainPipView.MenuItems.Add(new MenuItem("Ballon tonen", new EventHandler(mnuShowBalloon_Click)));
			mnuMainPipView.MenuItems.Add(new MenuItem("-"));
			mnuMainPipView.MenuItems.Add(new MenuItem("Opties...", new EventHandler(mnuOptions_Click)));
			mnuMainPipView.MenuItems.Add(new MenuItem("Pip in browser openen...", new EventHandler(mnuPipInBrowser_Click)));
			mnuMainPipView.MenuItems.Add(new MenuItem("-"));
			mnuMainPipView.MenuItems.Add(new MenuItem("Afsluiten", new EventHandler(mnuExit_Click)));

			MenuItem mnuMainHelp = new MenuItem("&Help");
			mnuMainHelp.MenuItems.Add(new MenuItem("Help...", new EventHandler(mnuHelp_Click), Shortcut.F1));
			mnuMainHelp.MenuItems.Add(new MenuItem("PipView website...", new EventHandler(mnuWebsite_Click)));
			mnuMainHelp.MenuItems.Add(new MenuItem("-"));
			mnuMainHelp.MenuItems.Add(new MenuItem("Controleren op updates", new EventHandler(mnuCheckUpdates_Click)));
			mnuMainHelp.MenuItems.Add(new MenuItem("-"));
			mnuMainHelp.MenuItems.Add(new MenuItem("Over dit programma", new EventHandler(mnuAbout_Click)));

			Menu = new MainMenu();
			Menu.MenuItems.Add(mnuMainPipView);
			Menu.MenuItems.Add(mnuMainHelp);

			// brushes
			evenBrush = new SolidBrush(Color.FromArgb(238, 238, 238));
			oddBrush = new SolidBrush(Color.FromArgb(255, 255, 255));

			// form icon & text
			Left = Program.Settings.Window.Left;
			Top = Program.Settings.Window.Top;
			Icon = Resources.Icon;

			Text = String.Format("PipView {0}", Program.VersionInfo);
		}

		private void mnuPipInBrowser_Click(object sender, EventArgs e)
		{
			PipInBrowser(sender, e);
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			Exit(sender, e);
		}

		private void mnuOptions_Click(object sender, EventArgs e)
		{
			Options(sender, e);
		}

		private void mnuShowBalloon_Click(object sender, EventArgs e)
		{
			ShowBalloon(sender, e);
		}

		private void mnuRefreshData_Click(object sender, EventArgs e)
		{
			RefreshData(sender, e);
		}

		private void mnuCheckUpdates_Click(object sender, EventArgs e)
		{
			CheckUpdates(sender, e);
		}

		private void mnuHelp_Click(object sender, EventArgs e)
		{
			Help(sender, e);
		}

		private void mnuAbout_Click(object sender, EventArgs e)
		{
			About(sender, e);
		}

		private void mnuWebsite_Click(object sender, EventArgs e)
		{
			Website(sender, e);
		}

		private void tableLayoutPanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
		{
			if (e.Row <= 5)
			{
				e.Graphics.FillRectangle(((e.Row % 2 == 0) ? evenBrush : oddBrush), e.CellBounds);
			}
		}

		internal void UpdateData(TrafficData pd)
		{
			string monthStart = Program.MonthNames[pd.PeriodStart.Month - 1];
			string monthEnd = Program.MonthNames[pd.PeriodEnd.Month - 1];

			trafficDownValueLabel.Text = String.Format("{0:#,0.0} MB", pd.TrafficPeriodDown);
			trafficUpValueLabel.Text = String.Format("{0:#,0.0} MB", pd.TrafficPeriodUp);
			trafficTotalValueLabel.Text = String.Format("{0:#,0.0} MB", pd.TrafficPeriodTotal);
			trafficTodayValueLabel.Text = String.Format("{0:#,0.0} MB", pd.TrafficTodayTotal);
			giganewsValueLabel.Text = String.Format("{0:#,0.0} MB", pd.TrafficGiganews);
			supernewsValueLabel.Text = String.Format("{0:#,0.0} MB", pd.TrafficSupernews);

			if (pd.PeriodStart != pd.PeriodEnd)
			{
				periodLabel.Text = String.Format("Periode ({0:%d} {1} t/m {2:%d} {3})", pd.PeriodStart, monthStart, pd.PeriodEnd, monthEnd);
			}
			else
			{
				periodLabel.Text = String.Format("Periode");
			}

			periodValueLabel.Text = String.Format("{0:0.0} %", pd.PeriodPercentage);

			periodBar.Value = (int)Math.Floor(pd.PeriodPercentage);
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;

				SaveLocation();
				Hide();
			}
		}

		private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveLocation();
		}

		private void SaveLocation()
		{
			if (WindowState != FormWindowState.Minimized)
			{
				Program.Settings.Window.Left = Left;
				Program.Settings.Window.Top = Top;
			}
		}
	}
}
