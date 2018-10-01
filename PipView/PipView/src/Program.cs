using PipView.Configuration;
using Microsoft.Win32;
using System;
using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Cache;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

[assembly: AssemblyTitle("PipView")]
[assembly: AssemblyDescription("PipView is een gratis programma waarmee gebruikers van ZeelandNet informatie over hun dataverkeer kunnen opvragen zonder de website van ZeelandNet te hoeven bezoeken.")]
[assembly: AssemblyCompany("Joost-Wim Boekesteijn")]
[assembly: AssemblyProduct("PipView")]
[assembly: AssemblyCopyright("Copyright (c) 2001-2007 Joost-Wim Boekesteijn")]
[assembly: ComVisible(false)]
[assembly: Guid("5c0531fd-091b-4d48-bb8b-3984d4be7e51")]
[assembly: AssemblyVersion("2.0.2.0")]
[assembly: NeutralResourcesLanguageAttribute("nl")]

namespace PipView
{
	public static class Program
	{
		internal static Settings Settings;
		internal static string[] MonthNames = { "januari", "februari", "maart", "april", "mei", "juni", "juli", "augustus", "september", "oktober", "november", "december" };

		[STAThread]
		public static void Main(string[] args)
		{
			string requiredExeName = "pipview.exe";
			string updateExeName = "pipview.update";

			string currentExeName = CurrentExeName;

			if ((currentExeName == requiredExeName) || (currentExeName == updateExeName))
			{
				// check if we are an update exefile
				if (currentExeName == updateExeName && args != null && args.Length == 1)
				{
					// first commandline argument should be pid of the old exe
					try
					{
						Process oldProcess = Process.GetProcessById(Convert.ToInt32(args[0]));
						oldProcess.WaitForExit();
					}
					catch (Exception) {}

					File.Delete(requiredExeName);
					File.Move(updateExeName, requiredExeName);

					MessageBox.Show(String.Format("PipView is bijgewerkt naar versie {0}", Program.VersionInfo), "PipView", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				HttpWebRequest.DefaultCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

				SystemEvents.SessionEnded += new SessionEndedEventHandler(SystemEvents_SessionEnded);

				Program.Settings = Manager.Load();

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new PipViewContext());

				Manager.Save(Program.Settings);

				SystemEvents.SessionEnded -= new SessionEndedEventHandler(SystemEvents_SessionEnded);
			}
			else
			{
				MessageBox.Show("PipView kan niet worden gestart. Het programmabestand van PipView moet de naam '" + requiredExeName + "' hebben.", "PipView", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private static void SystemEvents_SessionEnded(object sender, SessionEndedEventArgs e)
		{
			Manager.Save(Program.Settings);
		}

		internal static string CurrentExeName
		{
			get
			{
				return Path.GetFileName(Process.GetCurrentProcess().MainModule.FileName);
			}
		}

		internal static string VersionInfo
		{
			get
			{
				Version vi = Assembly.GetExecutingAssembly().GetName().Version;

				string format;

				if (vi.Revision == 0 && vi.Build == 0)
				{
					format = "{0}.{1}";
				}
				else if (vi.Revision == 0)
				{
					format = "{0}.{1}.{2}";
				}
				else
				{
					format = "{0}.{1}.{2}.{3}";
				}

				return String.Format(format, vi.Major, vi.Minor, vi.Build, vi.Revision);
			}
		}

		internal static bool ArrayMatch(byte[] a, byte[] b)
		{
			if (a.Length == b.Length)
			{
				for (int pos = 0; pos < a.Length; pos++)
				{
					if (a[pos] != b[pos])
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}
