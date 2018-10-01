namespace PipView
{
	partial class MainForm
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.periodLabel = new System.Windows.Forms.Label();
            this.periodValueLabel = new System.Windows.Forms.Label();
            this.supernewsLabel = new System.Windows.Forms.Label();
            this.supernewsValueLabel = new System.Windows.Forms.Label();
            this.giganewsLabel = new System.Windows.Forms.Label();
            this.giganewsValueLabel = new System.Windows.Forms.Label();
            this.trafficTodayLabel = new System.Windows.Forms.Label();
            this.trafficTodayValueLabel = new System.Windows.Forms.Label();
            this.trafficTotalLabel = new System.Windows.Forms.Label();
            this.trafficTotalValueLabel = new System.Windows.Forms.Label();
            this.trafficUpLabel = new System.Windows.Forms.Label();
            this.trafficUpValueLabel = new System.Windows.Forms.Label();
            this.trafficDownLabel = new System.Windows.Forms.Label();
            this.trafficDownValueLabel = new System.Windows.Forms.Label();
            this.periodBar = new System.Windows.Forms.ProgressBar();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel.Controls.Add(this.periodLabel, 0, 6);
            this.tableLayoutPanel.Controls.Add(this.periodValueLabel, 1, 6);
            this.tableLayoutPanel.Controls.Add(this.supernewsLabel, 0, 5);
            this.tableLayoutPanel.Controls.Add(this.supernewsValueLabel, 1, 5);
            this.tableLayoutPanel.Controls.Add(this.giganewsLabel, 0, 4);
            this.tableLayoutPanel.Controls.Add(this.giganewsValueLabel, 1, 4);
            this.tableLayoutPanel.Controls.Add(this.trafficTodayLabel, 0, 3);
            this.tableLayoutPanel.Controls.Add(this.trafficTodayValueLabel, 1, 3);
            this.tableLayoutPanel.Controls.Add(this.trafficTotalLabel, 0, 2);
            this.tableLayoutPanel.Controls.Add(this.trafficTotalValueLabel, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.trafficUpLabel, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.trafficUpValueLabel, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.trafficDownLabel, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.trafficDownValueLabel, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.periodBar, 0, 7);
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 8;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel.Size = new System.Drawing.Size(340, 165);
            this.tableLayoutPanel.TabIndex = 0;
            this.tableLayoutPanel.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tableLayoutPanel_CellPaint);
            // 
            // periodLabel
            // 
            this.periodLabel.AutoSize = true;
            this.periodLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.periodLabel.Location = new System.Drawing.Point(3, 120);
            this.periodLabel.Name = "periodLabel";
            this.periodLabel.Size = new System.Drawing.Size(43, 20);
            this.periodLabel.TabIndex = 24;
            this.periodLabel.Text = "Periode";
            this.periodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // periodValueLabel
            // 
            this.periodValueLabel.AutoSize = true;
            this.periodValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.periodValueLabel.Location = new System.Drawing.Point(304, 120);
            this.periodValueLabel.Name = "periodValueLabel";
            this.periodValueLabel.Size = new System.Drawing.Size(33, 20);
            this.periodValueLabel.TabIndex = 23;
            this.periodValueLabel.Text = "0,0 %";
            this.periodValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // supernewsLabel
            // 
            this.supernewsLabel.AutoSize = true;
            this.supernewsLabel.BackColor = System.Drawing.Color.Transparent;
            this.supernewsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.supernewsLabel.Location = new System.Drawing.Point(3, 100);
            this.supernewsLabel.Name = "supernewsLabel";
            this.supernewsLabel.Size = new System.Drawing.Size(60, 20);
            this.supernewsLabel.TabIndex = 15;
            this.supernewsLabel.Text = "Supernews";
            this.supernewsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // supernewsValueLabel
            // 
            this.supernewsValueLabel.AutoSize = true;
            this.supernewsValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.supernewsValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.supernewsValueLabel.Location = new System.Drawing.Point(296, 100);
            this.supernewsValueLabel.Name = "supernewsValueLabel";
            this.supernewsValueLabel.Size = new System.Drawing.Size(41, 20);
            this.supernewsValueLabel.TabIndex = 14;
            this.supernewsValueLabel.Text = "0,0 MB";
            this.supernewsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // giganewsLabel
            // 
            this.giganewsLabel.AutoSize = true;
            this.giganewsLabel.BackColor = System.Drawing.Color.Transparent;
            this.giganewsLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.giganewsLabel.Location = new System.Drawing.Point(3, 80);
            this.giganewsLabel.Name = "giganewsLabel";
            this.giganewsLabel.Size = new System.Drawing.Size(54, 20);
            this.giganewsLabel.TabIndex = 13;
            this.giganewsLabel.Text = "Giganews";
            this.giganewsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // giganewsValueLabel
            // 
            this.giganewsValueLabel.AutoSize = true;
            this.giganewsValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.giganewsValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.giganewsValueLabel.Location = new System.Drawing.Point(296, 80);
            this.giganewsValueLabel.Name = "giganewsValueLabel";
            this.giganewsValueLabel.Size = new System.Drawing.Size(41, 20);
            this.giganewsValueLabel.TabIndex = 12;
            this.giganewsValueLabel.Text = "0,0 MB";
            this.giganewsValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trafficTodayLabel
            // 
            this.trafficTodayLabel.AutoSize = true;
            this.trafficTodayLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficTodayLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.trafficTodayLabel.Location = new System.Drawing.Point(3, 60);
            this.trafficTodayLabel.Name = "trafficTodayLabel";
            this.trafficTodayLabel.Size = new System.Drawing.Size(94, 20);
            this.trafficTodayLabel.TabIndex = 11;
            this.trafficTodayLabel.Text = "Vandaag verbruikt";
            this.trafficTodayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trafficTodayValueLabel
            // 
            this.trafficTodayValueLabel.AutoSize = true;
            this.trafficTodayValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficTodayValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.trafficTodayValueLabel.Location = new System.Drawing.Point(296, 60);
            this.trafficTodayValueLabel.Name = "trafficTodayValueLabel";
            this.trafficTodayValueLabel.Size = new System.Drawing.Size(41, 20);
            this.trafficTodayValueLabel.TabIndex = 10;
            this.trafficTodayValueLabel.Text = "0,0 MB";
            this.trafficTodayValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trafficTotalLabel
            // 
            this.trafficTotalLabel.AutoSize = true;
            this.trafficTotalLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficTotalLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.trafficTotalLabel.Location = new System.Drawing.Point(3, 40);
            this.trafficTotalLabel.Name = "trafficTotalLabel";
            this.trafficTotalLabel.Size = new System.Drawing.Size(37, 20);
            this.trafficTotalLabel.TabIndex = 7;
            this.trafficTotalLabel.Text = "Totaal";
            this.trafficTotalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trafficTotalValueLabel
            // 
            this.trafficTotalValueLabel.AutoSize = true;
            this.trafficTotalValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficTotalValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.trafficTotalValueLabel.Location = new System.Drawing.Point(296, 40);
            this.trafficTotalValueLabel.Name = "trafficTotalValueLabel";
            this.trafficTotalValueLabel.Size = new System.Drawing.Size(41, 20);
            this.trafficTotalValueLabel.TabIndex = 6;
            this.trafficTotalValueLabel.Text = "0,0 MB";
            this.trafficTotalValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trafficUpLabel
            // 
            this.trafficUpLabel.AutoSize = true;
            this.trafficUpLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficUpLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.trafficUpLabel.Location = new System.Drawing.Point(3, 20);
            this.trafficUpLabel.Name = "trafficUpLabel";
            this.trafficUpLabel.Size = new System.Drawing.Size(52, 20);
            this.trafficUpLabel.TabIndex = 5;
            this.trafficUpLabel.Text = "Verstuurd";
            this.trafficUpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trafficUpValueLabel
            // 
            this.trafficUpValueLabel.AutoSize = true;
            this.trafficUpValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficUpValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.trafficUpValueLabel.Location = new System.Drawing.Point(296, 20);
            this.trafficUpValueLabel.Name = "trafficUpValueLabel";
            this.trafficUpValueLabel.Size = new System.Drawing.Size(41, 20);
            this.trafficUpValueLabel.TabIndex = 4;
            this.trafficUpValueLabel.Text = "0,0 MB";
            this.trafficUpValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // trafficDownLabel
            // 
            this.trafficDownLabel.AutoSize = true;
            this.trafficDownLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficDownLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.trafficDownLabel.Location = new System.Drawing.Point(3, 0);
            this.trafficDownLabel.Name = "trafficDownLabel";
            this.trafficDownLabel.Size = new System.Drawing.Size(60, 20);
            this.trafficDownLabel.TabIndex = 3;
            this.trafficDownLabel.Text = "Ontvangen";
            this.trafficDownLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // trafficDownValueLabel
            // 
            this.trafficDownValueLabel.AutoSize = true;
            this.trafficDownValueLabel.BackColor = System.Drawing.Color.Transparent;
            this.trafficDownValueLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.trafficDownValueLabel.Location = new System.Drawing.Point(296, 0);
            this.trafficDownValueLabel.Name = "trafficDownValueLabel";
            this.trafficDownValueLabel.Size = new System.Drawing.Size(41, 20);
            this.trafficDownValueLabel.TabIndex = 2;
            this.trafficDownValueLabel.Text = "0,0 MB";
            this.trafficDownValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // periodBar
            // 
            this.tableLayoutPanel.SetColumnSpan(this.periodBar, 2);
            this.periodBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.periodBar.Location = new System.Drawing.Point(3, 143);
            this.periodBar.Name = "periodBar";
            this.periodBar.Size = new System.Drawing.Size(334, 19);
            this.periodBar.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(340, 167);
            this.Controls.Add(this.tableLayoutPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PipView";
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.Label supernewsLabel;
		private System.Windows.Forms.Label supernewsValueLabel;
		private System.Windows.Forms.Label giganewsLabel;
		private System.Windows.Forms.Label giganewsValueLabel;
		private System.Windows.Forms.Label trafficTodayLabel;
		private System.Windows.Forms.Label trafficTodayValueLabel;
		private System.Windows.Forms.Label trafficTotalLabel;
		private System.Windows.Forms.Label trafficTotalValueLabel;
		private System.Windows.Forms.Label trafficUpLabel;
		private System.Windows.Forms.Label trafficUpValueLabel;
		private System.Windows.Forms.Label trafficDownLabel;
		private System.Windows.Forms.Label trafficDownValueLabel;
		private System.Windows.Forms.ProgressBar periodBar;
		private System.Windows.Forms.Label periodLabel;
		private System.Windows.Forms.Label periodValueLabel;
	}
}

