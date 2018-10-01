using System;
using System.Net;
using System.Net.Cache;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Windows.Forms;

[assembly: AssemblyTitle("PipView")]
[assembly: AssemblyDescription("PipView is een gratis programma waarmee gebruikers van ZeelandNet informatie over hun dataverkeer kunnen opvragen zonder de website van ZeelandNet te hoeven bezoeken.")]
[assembly: AssemblyCompany("Joost-Wim Boekesteijn")]
[assembly: AssemblyProduct("PipView")]
[assembly: AssemblyCopyright("Copyright (c) 2001-2008 Joost-Wim Boekesteijn")]
[assembly: ComVisible(false)]
[assembly: Guid("5c0531fd-091b-4d48-bb8b-3984d4be7e51")]
[assembly: AssemblyVersion("2.0.3.0")]
[assembly: NeutralResourcesLanguageAttribute("nl")]

namespace PipView
{
	public static class PipView
	{
		[STAThread]
		public static void Main(string[] args)
		{
			HttpWebRequest.DefaultCachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.BypassCache);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new PipViewContext());
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
	}
}
