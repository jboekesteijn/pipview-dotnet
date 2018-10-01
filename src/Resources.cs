using System.Reflection;
using System.Resources;
using System.Drawing;

namespace PipView
{
	internal static class Resources
	{
		private static ResourceManager rm = new ResourceManager("PipView.Icons", Assembly.GetExecutingAssembly());

		internal static Icon Icon
		{
			get
			{
				return (Icon)rm.GetObject("MainIcon");
			}
		}

		internal static Icon SmallIcon
		{
			get
			{
				return new Icon(Icon, 16, 16);
			}
		}
	}
}
