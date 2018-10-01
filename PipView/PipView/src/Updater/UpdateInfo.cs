using System;

namespace PipView.Updater
{
	public class UpdateInfo
	{
		private Uri uri;

		public Uri Uri
		{
			get { return uri; }
			set { uri = value; }
		}

		private string version;

		public string Version
		{
			get { return version; }
			set { version = value; }
		}

		private string hash;

		public string Hash
		{
			get { return hash; }
			set { hash = value; }
		}
	}
}
