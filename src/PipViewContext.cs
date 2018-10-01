using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using PipView.Configuration;
using PipView.Exceptions;
using PipView.Pip;
using PipView.Updater;

namespace PipView
{
	public class PipViewContext : ApplicationContext
	{
        private Settings settings;
		private NotifyIcon notifyIcon;
		private MainForm mainForm;
		private OptionsForm optionsForm;
		private TrafficData pd;
		private System.Timers.Timer refreshTimer;
		private Thread RefreshDataThread;
		private Thread CheckUpdatesThread;
		private Thread SendTalkbackThread;
		private int activeThreadCount;

		public PipViewContext()
		{
            // load application settings
            settings = SettingsManager.Load();

			pd = new TrafficData();

			// refresh timer
			refreshTimer = new System.Timers.Timer();
			refreshTimer.Elapsed += new ElapsedEventHandler(refreshTimer_Elapsed);
			refreshTimer.AutoReset = true;
			UpdateTimerInterval();
			refreshTimer.Enabled = settings.AutoRefresh;

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
			mnuTray.MenuItems.Add(mnuPipView);
			mnuTray.MenuItems.Add(new MenuItem("Pip in browser openen...", new EventHandler(mnuPipInBrowser_Click)));
			mnuTray.MenuItems.Add(new MenuItem("-"));
			mnuTray.MenuItems.Add(new MenuItem("Ballon tonen", new EventHandler(mnuShowBalloon_Click)));
			mnuTray.MenuItems.Add(new MenuItem("Gegevens vernieuwen", new EventHandler(mnuRefreshData_Click), Shortcut.F5));
			mnuTray.MenuItems.Add(new MenuItem("-"));
			mnuTray.MenuItems.Add(new MenuItem("Afsluiten", new EventHandler(mnuExit_Click)));

			notifyIcon.ContextMenu = mnuTray;

            if (settings.RefreshOnStartup)
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
            int interval = settings.RefreshInterval * 60 * 1000;

            if (refreshTimer.Interval != interval)
            {
                refreshTimer.Interval = interval;
            }
		}

		private void Run(Action id)
		{
            if (mainForm != null && !mainForm.IsDisposed && mainForm.InvokeRequired)
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
				Run
				(
					delegate() 
					{
						MessageBox.Show("Interne fout in PipView. Negatief aantal actieve threads gesignaleerd.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				);
			}

			Run(
				delegate()
				{
                    if (mainForm != null)
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
				}
			);
		}

		private void HandleException(Exception e)
		{
			Run
			(
				delegate()
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
            if (mainForm == null)
            {
                mainForm = new MainForm(settings);

                mainForm.FormClosed += new FormClosedEventHandler(mainForm_FormClosed);

                mainForm.RefreshData += new EventHandler(mnuRefreshData_Click);
                mainForm.ShowBalloon += new EventHandler(mnuShowBalloon_Click);
                mainForm.Options += new EventHandler(mnuOptions_Click);
                mainForm.PipInBrowser += new EventHandler(mnuPipInBrowser_Click);
                mainForm.CheckUpdates += new EventHandler(mnuCheckUpdates_Click);
                mainForm.Help += new EventHandler(mainForm_Help);
                mainForm.Exit += new EventHandler(mnuExit_Click);
                mainForm.About += new EventHandler(mainForm_About);
                mainForm.Website += new EventHandler(mainForm_Website);
            }
            else
            {
                if (mainForm.WindowState == FormWindowState.Minimized)
                {
                    mainForm.WindowState = FormWindowState.Normal;
                }
            }

            mainForm.Show();
            mainForm.Activate();
		}

        void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            mainForm = null;
        }

		private static void mainForm_Website(object sender, EventArgs e)
		{
			Process.Start("http://pipview.xxp.nu/");
		}

		private static void mainForm_About(object sender, EventArgs e)
		{
            MessageBox.Show(String.Format("PipView {0}. © 2001-2008 Joost-Wim Boekesteijn.", PipView.VersionInfo), String.Format("PipView {0}", PipView.VersionInfo), MessageBoxButtons.OK, MessageBoxIcon.Information);
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
				optionsForm = new OptionsForm(settings);

				optionsForm.FormClosed += new FormClosedEventHandler(optionsForm_FormClosed);				
			}

            optionsForm.Show(mainForm);
			optionsForm.Activate();
		}

		void optionsForm_FormClosed(object sender, FormClosedEventArgs e)
		{
            optionsForm = null;

            if (refreshTimer.Enabled != settings.AutoRefresh)
            {
                refreshTimer.Enabled = settings.AutoRefresh;
            }

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

		private bool HasLicense(string userName, string version)
		{
			return true;
		
            /*foreach (Licensing.License l in settings.Licenses)
			{
				if ((l.UserName == userName) && (l.Version == version) && l.IsValid())
				{
					return true;
				}
			}

			return false;*/
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

					Talkback.Reporter.ReportParsingError(trafficPage);

					Run
					(
						delegate()
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
            bool exitApplication = false;

			try
			{
				UpdateInfo ui = Update.GetUpdateInfo();

                string yourversion = PipView.VersionInfo;
				string lastversion = ui.Version;

				if (yourversion != lastversion)
				{
					bool downloadUpdate = false;

					Run
					(
						delegate()
						{
							downloadUpdate = (MessageBox.Show(String.Format("Er is een update beschikbaar. De laatste officiële versie van PipView is versie {0}.\n\nU werkt op dit moment met versie {1}. Wilt u PipView bijwerken naar versie {0}?", lastversion, yourversion), "PipView", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
						}
					);

					if (downloadUpdate)
					{
						string updateFilePath = Update.DownloadUpdate(ui);
                        Update.PerformUpdate(updateFilePath);
                        exitApplication = true;                                               
					}
				}
				else
				{
					Run
					(
						delegate()
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

            if (exitApplication)
            {
                Application.Exit();
            }
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
            if (settings.HasDefaultCredentials)
			{
				Run
				(
					delegate()
					{
						notifyIcon.ShowBalloonTip(5000, "Waarschuwing", "U heeft nog geen loginnaam en wachtwoord ingevoerd. Wanneer u deze gegevens invult in het Opties-scherm, kan PipView inloggen op de website van ZeelandNet.", ToolTipIcon.Warning);
					}
				);
			}
			else
			{
                string username = settings.UserName;
                string password = Crypto.Decrypt(settings.Password);
                string version = PipView.VersionInfo;
                bool permanentlogin = settings.PermanentSession;

				try
				{
					if (!HasLicense(username, version))
					{
						Licensing.License lic = Licensing.License.Download(username, version);

                        settings.Licenses.Add(lic);
					}

					Session session = new Session(username, password, permanentlogin);
					session.SignIn();

					string statsPage = session.DownloadStatsPage();

					Parser parser = new Parser(statsPage);
					pd = parser.Parse();

					Run
					(
						delegate()
						{
							mainForm.UpdateData(pd);

                            if (settings.ShowBalloonAfterUpdate)
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
