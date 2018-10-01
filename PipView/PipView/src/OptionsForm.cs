using System.Windows.Forms;
using System;
using PipView.Configuration;

namespace PipView
{
	public partial class OptionsForm : Form
	{
		public event EventHandler TimerPropertiesChanged;

		public OptionsForm()
		{
			InitializeComponent();

			usernameTextBox.Text = Program.Settings.UserName;
			passwordTextBox.Text = Crypto.Decrypt(Program.Settings.GetPasswordBytes());
			refreshOnStartupCheckBox.Checked = Program.Settings.RefreshOnStartup;
			permanentSessionCheckBox.Checked = Program.Settings.PermanentSession;
			autoRefreshCheckBox.Checked = Program.Settings.AutoRefresh;
			refreshIntervalUpdown.Value = Program.Settings.RefreshInterval;
			showBalloonAfterUpdateCheckBox.Checked = Program.Settings.ShowBalloonAfterUpdate;
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			SaveSettings();

			Close();
		}

		private void SaveSettings()
		{
			bool generateEvent = (Program.Settings.AutoRefresh != autoRefreshCheckBox.Checked) || (Program.Settings.RefreshInterval != (int)refreshIntervalUpdown.Value);

			Program.Settings.UserName = usernameTextBox.Text;
			Program.Settings.SetPasswordBytes(Crypto.Encrypt(passwordTextBox.Text));
			Program.Settings.RefreshOnStartup = refreshOnStartupCheckBox.Checked;
			Program.Settings.PermanentSession = permanentSessionCheckBox.Checked;
			Program.Settings.AutoRefresh = autoRefreshCheckBox.Checked;
			Program.Settings.RefreshInterval = (int)refreshIntervalUpdown.Value;
			Program.Settings.ShowBalloonAfterUpdate = showBalloonAfterUpdateCheckBox.Checked;

			if (generateEvent)
			{
				TimerPropertiesChanged(this, new EventArgs());
			}
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
