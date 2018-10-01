using System.Collections.Generic;
using System.Collections.ObjectModel;
using PipView.Extensions;
using PipView.Licensing;

namespace PipView.Configuration
{
	public class Settings
	{
		public string UserName { get; set; }
		public byte[] Password { get; set; }
		public bool PermanentSession { get; set; }
		public int RefreshInterval { get; set; }
		public bool RefreshOnStartup { get; set; }
		public bool AutoRefresh { get; set; }
		public Window Window { get; set; }
		public bool ShowBalloonAfterUpdate { get; set; }

		private Collection<License> licenses;

		public Collection<License> Licenses
		{
			get { return licenses; }
		}

		public Settings()
		{
			UserName = "loginnaam";
			Password = Crypto.Encrypt("wachtwoord");
			RefreshInterval = 60;
			RefreshOnStartup = true;
			AutoRefresh = true;
			ShowBalloonAfterUpdate = true;
			Window = new Window { Left = 10, Top = 10 };
			licenses = new Collection<License>(new List<License>());
		}

		public bool HasDefaultCredentials
		{
			get
			{
				return
				(
					(UserName == "loginnaam") &&
					Password.EqualsArray(Crypto.Encrypt("wachtwoord"))
				);
			}
		}
	}
}
