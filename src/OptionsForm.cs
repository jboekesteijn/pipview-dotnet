using System.Windows.Forms;
using System;
using PipView.Configuration;

namespace PipView
{
	public partial class OptionsForm : Form
	{
        private Settings settings;

		public OptionsForm(Settings pipViewSettings)
		{
			InitializeComponent();

            this.settings = pipViewSettings;

            usernameTextBox.Text = settings.UserName;
            passwordTextBox.Text = Crypto.Decrypt(settings.Password);
            refreshOnStartupCheckBox.Checked = settings.RefreshOnStartup;
            permanentSessionCheckBox.Checked = settings.PermanentSession;
            autoRefreshCheckBox.Checked = settings.AutoRefresh;
            refreshIntervalUpdown.Value = settings.RefreshInterval;
            showBalloonAfterUpdateCheckBox.Checked = settings.ShowBalloonAfterUpdate;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
            settings.UserName = usernameTextBox.Text;
            settings.Password = Crypto.Encrypt(passwordTextBox.Text);
            settings.RefreshOnStartup = refreshOnStartupCheckBox.Checked;
            settings.PermanentSession = permanentSessionCheckBox.Checked;
            settings.AutoRefresh = autoRefreshCheckBox.Checked;
            settings.RefreshInterval = (int)refreshIntervalUpdown.Value;
            settings.ShowBalloonAfterUpdate = showBalloonAfterUpdateCheckBox.Checked;

			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
