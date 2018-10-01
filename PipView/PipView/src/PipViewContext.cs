using System.Windows.Forms;
using System.Threading;
using PipView.Pip;
using System.Timers;
using System;
using PipView.Exceptions;
using System.Net;
using PipView.Updater;
using System.Diagnostics;
using System.Text;
using PipView.Configuration;

namespace PipView
{
	public class PipViewContext : ApplicationContext
	{
		private NotifyIcon notifyIcon;
		private MainForm mainForm;
		private OptionsForm optionsForm;
		private TrafficData pd;
		private System.Timers.Timer refreshTimer;
		private Thread RefreshDataThread;
		private Thread CheckUpdatesThread;
		private Thread SendTalkbackThread;
		private int activeThreadCount;
		delegate void InvokeDelegate();

		public PipViewContext()
		{
			pd = new TrafficData();

			mainForm = new MainForm();
			//mainForm.FormClosed += new FormClosedEventHandler(mainForm_FormClosed);
			mainForm.RefreshData += new EventHandler(mnuRefreshData_Click);
			mainForm.ShowBalloon += new EventHandler(mnuShowBalloon_Click);
			mainForm.Options += new EventHandler(mnuOptions_Click);
			mainForm.PipInBrowser += new EventHandler(mnuPipInBrowser_Click);
			mainForm.CheckUpdates += new EventHandler(mnuCheckUpdates_Click);
			mainForm.Help += new EventHandler(mainForm_Help);
			mainForm.Exit += new EventHandler(mnuExit_Click);
			mainForm.About += new EventHandler(mainForm_About);
			mainForm.Website += new EventHandler(mainForm_Website);

			// refresh timer
			refreshTimer = new System.Timers.Timer();
			refreshTimer.Elapsed += new ElapsedEventHandler(refreshTimer_Elapsed);
			refreshTimer.AutoReset = true;
			UpdateTimerInterval();
			refreshTimer.Enabled = Program.Settings.AutoRefresh;

			// tray icon
			notifyIcon = new NotifyIcon();
			notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
			notifyIcon.Icon = Resources.SmallIcon;
			notifyIcon.Text = "PipView";
			notifyIcon.Visible = true;

			// tray menu
			MenuItem mnuPipView = new MenuItem("PipView...");
			mnuPipView.DefaultItem = true;
			mnuPipView.Click += new EventHandler(notifyIcon_DoubleClick);

			ContextMenu mnuTray = new ContextMenu();
			//mnuTray.Popup += new EventHandler(mnuTray_Popup);
			mnuTray.MenuItems.Add(mnuPipView);
			mnuTray.MenuItems.Add(new MenuItem("Pip in browser openen...", new EventHandler(mnuPipInBrowser_Click)));
			mnuTray.MenuItems.Add(new MenuItem("-"));
			mnuTray.MenuItems.Add(new MenuItem("Ballon tonen", new EventHandler(mnuShowBalloon_Click)));
			mnuTray.MenuItems.Add(new MenuItem("Gegevens vernieuwen", new EventHandler(mnuRefreshData_Click), Shortcut.F5));
			mnuTray.MenuItems.Add(new MenuItem("-"));
			mnuTray.MenuItems.Add(new MenuItem("Afsluiten", new EventHandler(mnuExit_Click)));

			notifyIcon.ContextMenu = mnuTray;

			if (Program.Settings.RefreshOnStartup)
			{
				StartRefreshData();
			}
		}

		private void refreshTimer_Elapsed(object sender, ElapsedEventArgs e)
		{
			StartRefreshData();
		}

		private void UpdateTimerInterval()
		{
			refreshTimer.Interval = Program.Settings.RefreshInterval * 60 * 1000;
		}

		private void Run(InvokeDelegate id)
		{
			if (!mainForm.IsDisposed && mainForm.InvokeRequired)
			{
				mainForm.Invoke(id);
			}
			else
			{
				id.Invoke();
			}
		}

		private void UpdateThreadCount(int c)
		{
			activeThreadCount += c;

			if (activeThreadCount < 0)
			{
				mainForm.Invoke
				(
					(InvokeDelegate)delegate()
					{
						MessageBox.Show("Interne fout in PipView. Negatief aantal actieve threads gesignaleerd.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				);
			}

			Run(
				(InvokeDelegate)delegate()
				{
					if (activeThreadCount == 0)
					{
						mainForm.Cursor = Cursors.Default;
					}
					else
					{
						mainForm.Cursor = Cursors.WaitCursor;
					}
				}
			);
		}

		private void HandleException(Exception e)
		{
			Run
			(
				(InvokeDelegate)delegate()
				{
					if (e is PipException)
					{
						MessageBox.Show(e.Message, "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (e is WebException)
					{
						MessageBox.Show("PipView kan geen verbinding maken met het internet. Controleer uw verbinding of de instellingen van uw firewall.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else if (e is ParserException)
					{
						if (MessageBox.Show("PipView kan de opgehaalde gegevens van ZeelandNet niet verwerken.\n\nWilt u hier een rapport van opsturen naar de maker van PipView?\nDeze gegevens worden gebruikt om PipView te verbeteren.", "PipView", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
						{
							StartSendTalkback((ParserException)e);
						}
					}
					else
					{
						MessageBox.Show("Er is een onbekende fout opgetreden. Neem contact op met de maker van PipView.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			);
		}

		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					refreshTimer.Stop();
					refreshTimer.Dispose();
					notifyIcon.Dispose();

					if (optionsForm != null)
					{
						optionsForm.Dispose();
					}

					mainForm.Dispose();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		private void mnuPipInBrowser_Click(object sender, EventArgs e)
		{
			OpenPipInBrowser();
		}

		private static void OpenPipInBrowser()
		{
			Process.Start("iexplore.exe", "https://secure.zeelandnet.nl/login/");
		}

		private void notifyIcon_DoubleClick(object sender, EventArgs e)
		{
			if (mainForm.WindowState == FormWindowState.Minimized)
			{
				mainForm.WindowState = FormWindowState.Normal;
			}

			mainForm.Show();
			mainForm.Activate();
		}

		private static void mainForm_Website(object sender, EventArgs e)
		{
			Process.Start("http://pipview.xxp.nu/");
		}

		private static void mainForm_About(object sender, EventArgs e)
		{
			MessageBox.Show(String.Format("PipView {0}. © 2001-2007 Joost-Wim Boekesteijn.", Program.VersionInfo), String.Format("PipView {0}", Program.VersionInfo), MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private static void mainForm_Help(object sender, EventArgs e)
		{
			Process.Start("http://pipview.xxp.nu/");
		}

		private void mnuShowBalloon_Click(object sender, EventArgs e)
		{
			ShowBalloon();
		}

		private void mnuOptions_Click(object sender, EventArgs e)
		{
			if (optionsForm == null)
			{
				optionsForm = new OptionsForm();

				optionsForm.FormClosed += new FormClosedEventHandler(optionsForm_FormClosed);
				optionsForm.TimerPropertiesChanged += new EventHandler(optionsForm_TimerPropertiesChanged);

				optionsForm.Show(mainForm);
			}
			else
			{
				optionsForm.Show();
				optionsForm.Activate();
			}
		}

		void optionsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
			optionsForm.Dispose();
			optionsForm = null;
		}

		void optionsForm_TimerPropertiesChanged(object sender, EventArgs e)
		{
			refreshTimer.Enabled = Program.Settings.AutoRefresh;
			UpdateTimerInterval();
		}

		private void mnuCheckUpdates_Click(object sender, EventArgs e)
		{
			StartCheckUpdates();
		}

		private void mnuRefreshData_Click(object sender, EventArgs e)
		{
			StartRefreshData();
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void ShowBalloon()
		{
			StringBuilder balloonText = new StringBuilder();

			balloonText.AppendFormat("Totaal: {0:#,0.0} MB\n\n", pd.TrafficPeriodTotal);
			balloonText.AppendFormat("Vandaag verbruikt: {0:#,0.0} MB\n\n", pd.TrafficTodayTotal);
			balloonText.AppendFormat("Periode: {0:0.0} %", pd.PeriodPercentage);

			notifyIcon.ShowBalloonTip(5000, "Dataverkeer", balloonText.ToString(), ToolTipIcon.Info);
		}

		private static bool HasLicense(string userName, string version)
		{
			foreach (PipView.Licensing.License l in Program.Settings.Licenses)
			{
				if ((l.UserName == userName) && (l.Version == version) && l.IsValid())
				{
					return true;
				}
			}

			return false;
		}

		private void StartSendTalkback(ParserException pe)
		{
			if ((SendTalkbackThread == null) || (SendTalkbackThread.ThreadState != System.Threading.ThreadState.Running))
			{
				SendTalkbackThread = new Thread(SendTalkback);
				UpdateThreadCount(1);
				SendTalkbackThread.Start(pe);
			}
		}

		private void SendTalkback(object exception)
		{
			ParserException pe = exception as ParserException;

			if (pe != null)
			{
				try
				{
					string trafficPage = pe.Data["TrafficPage"] as string;

					PipView.Talkback.Reporter.ReportParsingError(trafficPage);

					Run
					(
						(InvokeDelegate)delegate()
						{
							MessageBox.Show("Hartelijk dank voor het opsturen van de gegevens.\nIn het help-menu van PipView kunt u controleren of er een update voor PipView beschikbaar is.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					);
				}
				catch (Exception e)
				{
					HandleException(e);
				}
			}

			UpdateThreadCount(-1);
		}

		private void StartCheckUpdates()
		{
			if ((CheckUpdatesThread == null) || (CheckUpdatesThread.ThreadState != System.Threading.ThreadState.Running))
			{
				CheckUpdatesThread = new Thread(CheckUpdates);
				UpdateThreadCount(1);
				CheckUpdatesThread.Start();
			}
		}

		private void CheckUpdates()
		{
			try
			{
				UpdateInfo ui = Update.GetUpdateInfo();

				string yourversion = Program.VersionInfo;
				string lastversion = ui.Version;

				if (yourversion != lastversion)
				{
					bool downloadUpdate = false;

					Run
					(
						(InvokeDelegate)delegate()
						{
							downloadUpdate = (MessageBox.Show(String.Format("Er is een update beschikbaar. De laatste officiële versie van PipView is versie {0}.\n\nU werkt op dit moment met versie {1}. Wilt u PipView bijwerken naar versie {0}?", lastversion, yourversion), "PipView", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
						}
					);

					if (downloadUpdate)
					{
						Update.DownloadUpdate(ui);
						Update.PerformUpdate();
					}
				}
				else
				{
					Run
					(
						(InvokeDelegate)delegate()
						{
							MessageBox.Show("Er is geen update beschikbaar. U werkt op dit moment met de laatste versie van PipView.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
					);
				}
			}
			catch (Exception e)
			{
				HandleException(e);
			}

			UpdateThreadCount(-1);
		}

		private void StartRefreshData()
		{
			if ((RefreshDataThread == null) || (RefreshDataThread.ThreadState != System.Threading.ThreadState.Running))
			{
				RefreshDataThread = new Thread(RefreshData);
				UpdateThreadCount(1);
				RefreshDataThread.Start();
			}
		}

		private void RefreshData()
		{
			if (Program.Settings.DefaultCredentials)
			{
				Run
				(
					(InvokeDelegate)delegate()
					{
						notifyIcon.ShowBalloonTip(5000, "Waarschuwing", "U heeft nog geen loginnaam en wachtwoord ingevoerd. Wanneer u deze gegevens invult in het Opties-scherm, kan PipView inloggen op de website van ZeelandNet.", ToolTipIcon.Warning);
					}
				);
			}
			else
			{
				string username = Program.Settings.UserName;
				string password = Crypto.Decrypt(Program.Settings.GetPasswordBytes());
				string version = Program.VersionInfo;
				bool permanentlogin = Program.Settings.PermanentSession;

				try
				{
					if (!HasLicense(username, version))
					{
						PipView.Licensing.License lic = PipView.Licensing.License.Download(username, version);

						Program.Settings.Licenses.Add(lic);
					}

					Session session = new Session(username, password, permanentlogin);
					session.SignIn();

					string statsPage = session.DownloadStatsPage();

					Parser parser = new Parser(statsPage);
					pd = parser.Parse();

					Run
					(
						(InvokeDelegate)delegate()
						{
							mainForm.UpdateData(pd);

							if (Program.Settings.ShowBalloonAfterUpdate)
							{
								ShowBalloon();
							}
						}
					);
				}
				catch (Exception e)
				{
					HandleException(e);
				}
			}

			UpdateThreadCount(-1);
		}
	}
}
