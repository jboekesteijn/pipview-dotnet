using System.Runtime.InteropServices;

namespace PipView.Pip
{
	internal static class NativeMethods
	{
		[DllImport("wininet.dll", CharSet = CharSet.Auto)]
		internal static extern bool InternetSetCookie(string url, string cookieName, string cookieData);
	}
}
