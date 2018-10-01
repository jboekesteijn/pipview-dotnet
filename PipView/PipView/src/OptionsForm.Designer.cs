namespace PipView
{
	partial class OptionsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.domainNameLabel = new System.Windows.Forms.Label();
			this.passwordLabel = new System.Windows.Forms.Label();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.usernameLabel = new System.Windows.Forms.Label();
			this.usernameTextBox = new System.Windows.Forms.TextBox();
			this.refreshOnStartupCheckBox = new System.Windows.Forms.CheckBox();
			this.permanentSessionCheckBox = new System.Windows.Forms.CheckBox();
			this.autoRefreshCheckBox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.refreshIntervalUpdown = new System.Windows.Forms.NumericUpDown();
			this.showBalloonAfterUpdateCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.refreshIntervalUpdown)).BeginInit();
			this.SuspendLayout();
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(298, 167);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(75, 23);
			this.cancelButton.TabIndex = 13;
			this.cancelButton.Text = "Annuleren";
			this.cancelButton.UseVisualStyleBackColor = true;
			this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
			// 
			// okButton
			// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(217, 167);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(75, 23);
			this.okButton.TabIndex = 12;
			this.okButton.Text = "OK";
			this.okButton.UseVisualStyleBackColor = true;
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// domainNameLabel
			// 
			this.domainNameLabel.AutoSize = true;
			this.domainNameLabel.Location = new System.Drawing.Point(259, 9);
			this.domainNameLabel.Name = "domainNameLabel";
			this.domainNameLabel.Size = new System.Drawing.Size(81, 13);
			this.domainNameLabel.TabIndex = 18;
			this.domainNameLabel.Text = "@zeelandnet.nl";
			// 
			// passwordLabel
			// 
			this.passwordLabel.AutoSize = true;
			this.passwordLabel.Location = new System.Drawing.Point(12, 35);
			this.passwordLabel.Name = "passwordLabel";
			this.passwordLabel.Size = new System.Drawing.Size(71, 13);
			this.passwordLabel.TabIndex = 17;
			this.passwordLabel.Text = "Wachtwoord:";
			// 
			// passwordTextBox
			// 
			this.passwordTextBox.Location = new System.Drawing.Point(95, 32);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.Size = new System.Drawing.Size(245, 20);
			this.passwordTextBox.TabIndex = 16;
			this.passwordTextBox.UseSystemPasswordChar = true;
			// 
			// usernameLabel
			// 
			this.usernameLabel.AutoSize = true;
			this.usernameLabel.Location = new System.Drawing.Point(12, 9);
			this.usernameLabel.Name = "usernameLabel";
			this.usernameLabel.Size = new System.Drawing.Size(62, 13);
			this.usernameLabel.TabIndex = 15;
			this.usernameLabel.Text = "Loginnaam:";
			// 
			// usernameTextBox
			// 
			this.usernameTextBox.Location = new System.Drawing.Point(95, 6);
			this.usernameTextBox.Name = "usernameTextBox";
			this.usernameTextBox.Size = new System.Drawing.Size(158, 20);
			this.usernameTextBox.TabIndex = 14;
			// 
			// refreshOnStartupCheckBox
			// 
			this.refreshOnStartupCheckBox.AutoSize = true;
			this.refreshOnStartupCheckBox.Location = new System.Drawing.Point(95, 68);
			this.refreshOnStartupCheckBox.Name = "refreshOnStartupCheckBox";
			this.refreshOnStartupCheckBox.Size = new System.Drawing.Size(264, 17);
			this.refreshOnStartupCheckBox.TabIndex = 19;
			this.refreshOnStartupCheckBox.Text = "Gegevens vernieuwen bij het starten van PipView.";
			this.refreshOnStartupCheckBox.UseVisualStyleBackColor = true;
			// 
			// permanentSessionCheckBox
			// 
			this.permanentSessionCheckBox.AutoSize = true;
			this.permanentSessionCheckBox.Location = new System.Drawing.Point(95, 138);
			this.permanentSessionCheckBox.Name = "permanentSessionCheckBox";
			this.permanentSessionCheckBox.Size = new System.Drawing.Size(175, 17);
			this.permanentSessionCheckBox.TabIndex = 20;
			this.permanentSessionCheckBox.Text = "Ingelogd blijven bij ZeelandNet.";
			this.permanentSessionCheckBox.UseVisualStyleBackColor = true;
			// 
			// autoRefreshCheckBox
			// 
			this.autoRefreshCheckBox.AutoSize = true;
			this.autoRefreshCheckBox.Location = new System.Drawing.Point(95, 114);
			this.autoRefreshCheckBox.Name = "autoRefreshCheckBox";
			this.autoRefreshCheckBox.Size = new System.Drawing.Size(168, 17);
			this.autoRefreshCheckBox.TabIndex = 21;
			this.autoRefreshCheckBox.Text = "Gegevens vernieuwen om de ";
			this.autoRefreshCheckBox.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(313, 116);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 22;
			this.label1.Text = "minuten.";
			// 
			// refreshIntervalUpdown
			// 
			this.refreshIntervalUpdown.Location = new System.Drawing.Point(259, 112);
			this.refreshIntervalUpdown.Maximum = new decimal(new int[] {
            1440,
            0,
            0,
            0});
			this.refreshIntervalUpdown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.refreshIntervalUpdown.Name = "refreshIntervalUpdown";
			this.refreshIntervalUpdown.Size = new System.Drawing.Size(51, 20);
			this.refreshIntervalUpdown.TabIndex = 23;
			this.refreshIntervalUpdown.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// showBalloonAfterUpdateCheckBox
			// 
			this.showBalloonAfterUpdateCheckBox.AutoSize = true;
			this.showBalloonAfterUpdateCheckBox.Location = new System.Drawing.Point(95, 91);
			this.showBalloonAfterUpdateCheckBox.Name = "showBalloonAfterUpdateCheckBox";
			this.showBalloonAfterUpdateCheckBox.Size = new System.Drawing.Size(250, 17);
			this.showBalloonAfterUpdateCheckBox.TabIndex = 24;
			this.showBalloonAfterUpdateCheckBox.Text = "Ballon tonen na het vernieuwen van gegevens.";
			this.showBalloonAfterUpdateCheckBox.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(379, 198);
			this.Controls.Add(this.showBalloonAfterUpdateCheckBox);
			this.Controls.Add(this.refreshIntervalUpdown);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.autoRefreshCheckBox);
			this.Controls.Add(this.permanentSessionCheckBox);
			this.Controls.Add(this.refreshOnStartupCheckBox);
			this.Controls.Add(this.domainNameLabel);
			this.Controls.Add(this.passwordLabel);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.usernameLabel);
			this.Controls.Add(this.usernameTextBox);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Opties";
			((System.ComponentModel.ISupportInitialize)(this.refreshIntervalUpdown)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Label domainNameLabel;
		private System.Windows.Forms.Label passwordLabel;
		private System.Windows.Forms.Label usernameLabel;
		internal System.Windows.Forms.TextBox usernameTextBox;
		private System.Windows.Forms.TextBox passwordTextBox;
		private System.Windows.Forms.CheckBox refreshOnStartupCheckBox;
		private System.Windows.Forms.CheckBox permanentSessionCheckBox;
		private System.Windows.Forms.CheckBox autoRefreshCheckBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown refreshIntervalUpdown;
		private System.Windows.Forms.CheckBox showBalloonAfterUpdateCheckBox;

	}
}