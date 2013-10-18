using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace Update
{
	class Program
	{
		private static Assembly assembly = Assembly.GetExecutingAssembly();

		private readonly static char dsp = Path.DirectorySeparatorChar;
		private readonly static string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

		private static string UID;
		private static string file;

		public static string updateResult;

		static void Main(string[] args)
		{
			UID = args[0];
			file = localAppData + dsp + UID + @".exe";

			int timeout = 1000;
			int retryAmount = 10;

			do
			{
				try
				{
					using(Stream input = assembly.GetManifestResourceStream(@"update.WinLog Manager.exe"))
					using(Stream output = File.Create(file))
					{
						CopyStream(input, output);
					}

					updateResult = "success";
					Console.WriteLine("success");
					break;
				}
				catch (Exception e)
				{
					if (retryAmount <= 0)
					{
						updateResult = e.Message.ToString();
						Console.WriteLine("failed");
						break;
					}
					else
					{
						Console.WriteLine("retry");
						Thread.Sleep(timeout);
					}
				}
			} while (retryAmount-- > 0);

			Process logger = new Process();
			logger.StartInfo.FileName = file;
			logger.StartInfo.Arguments = updateResult;
			logger.Start();
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
