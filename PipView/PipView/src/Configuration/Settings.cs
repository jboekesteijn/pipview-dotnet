using PipView.Licensing;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PipView.Configuration
{
	public class Settings
	{
		private string userName;

		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		private byte[] password;

		public byte[] GetPasswordBytes()
		{
			return (byte[])password.Clone();
		}

		public void SetPasswordBytes(byte[] password)
		{
			this.password = password;
		}

		private bool permanentSession;

		public bool PermanentSession
		{
			get { return permanentSession; }
			set { permanentSession = value; }
		}

		private int refreshInterval;

		public int RefreshInterval
		{
			get { return refreshInterval; }
			set { refreshInterval = value; }
		}

		private bool refreshOnStartup;

		public bool RefreshOnStartup
		{
			get { return refreshOnStartup; }
			set { refreshOnStartup = value;  }
		}

		private bool autoRefresh;

		public bool AutoRefresh
		{
			get { return autoRefresh; }
			set { autoRefresh = value; }
		}

		private Window window;

		public Window Window
		{
			get { return window; }
			set { window = value; }
		}

		private Collection<License> licenses;

		public Collection<License> Licenses
		{
			get { return licenses; }
		}

		private bool showBalloonAfterUpdate;

		public bool ShowBalloonAfterUpdate
		{
			get { return showBalloonAfterUpdate; }
			set { showBalloonAfterUpdate = value; }
		}
	

		public Settings()
		{
			userName = "loginnaam";
			password = Crypto.Encrypt("wachtwoord");
			refreshInterval = 60;
			refreshOnStartup = true;
			autoRefresh = true;
			showBalloonAfterUpdate = true;
			window = new Window();
			licenses = new Collection<License>(new List<License>());
		}

		public bool DefaultCredentials
		{
			get
			{
				return
				(
					(userName == "loginnaam") &&
					(Program.ArrayMatch(password, Crypto.Encrypt("wachtwoord")))
				);
			}
		}
	}
}
