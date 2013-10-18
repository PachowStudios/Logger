namespace Logger
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.start = new System.Windows.Forms.Button();
			this.output = new System.Windows.Forms.TextBox();
			this.stop = new System.Windows.Forms.Button();
			this.systray = new System.Windows.Forms.NotifyIcon(this.components);
			this.getWindowTimer = new System.Windows.Forms.Timer(this.components);
			this.mouseKeyEventProvider = new MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider();
			this.SuspendLayout();
			// 
			// start
			// 
			this.start.Location = new System.Drawing.Point(12, 125);
			this.start.Name = "start";
			this.start.Size = new System.Drawing.Size(121, 23);
			this.start.TabIndex = 1;
			this.start.Text = "Start";
			this.start.UseVisualStyleBackColor = true;
			this.start.Click += new System.EventHandler(this.Start_Click);
			// 
			// output
			// 
			this.output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
			this.output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.output.Cursor = System.Windows.Forms.Cursors.Default;
			this.output.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.output.Location = new System.Drawing.Point(12, 12);
			this.output.Multiline = true;
			this.output.Name = "output";
			this.output.ReadOnly = true;
			this.output.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.output.Size = new System.Drawing.Size(259, 107);
			this.output.TabIndex = 2;
			// 
			// stop
			// 
			this.stop.Location = new System.Drawing.Point(150, 125);
			this.stop.Name = "stop";
			this.stop.Size = new System.Drawing.Size(121, 23);
			this.stop.TabIndex = 3;
			this.stop.Text = "Stop";
			this.stop.UseVisualStyleBackColor = true;
			this.stop.Click += new System.EventHandler(this.Stop_Click);
			// 
			// systray
			// 
			this.systray.Icon = ((System.Drawing.Icon)(resources.GetObject("systray.Icon")));
			this.systray.Text = "Logger";
			this.systray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SystrayMouse_DoubleClick);
			// 
			// getWindowTimer
			// 
			this.getWindowTimer.Enabled = true;
			this.getWindowTimer.Tick += new System.EventHandler(this.getWindowTimer_Tick);
			// 
			// mouseKeyEventProvider
			// 
			this.mouseKeyEventProvider.Enabled = true;
			this.mouseKeyEventProvider.HookType = MouseKeyboardActivityMonitor.Controls.HookType.Global;
			this.mouseKeyEventProvider.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.HookManager_KeyPress);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(283, 160);
			this.Controls.Add(this.stop);
			this.Controls.Add(this.output);
			this.Controls.Add(this.start);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "Logger";
			this.Resize += new System.EventHandler(this.MainForm_Resize);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.NotifyIcon systray;
		private System.Windows.Forms.Button stop;
		public System.Windows.Forms.TextBox output;
		private System.Windows.Forms.Button start;
		private System.Windows.Forms.Timer getWindowTimer;
		private MouseKeyboardActivityMonitor.Controls.MouseKeyEventProvider mouseKeyEventProvider;
	}
}
