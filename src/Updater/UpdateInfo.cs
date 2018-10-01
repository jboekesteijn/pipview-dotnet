using System;

namespace PipView.Updater
{
	public class UpdateInfo
	{
		public Uri Uri { get; set; }
		public string Version { get; set; }
		public string Hash { get; set; }
	}
}
