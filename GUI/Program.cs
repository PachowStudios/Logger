using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace Logger
{
	internal sealed class Program
	{
		public static bool run = false;
		public static string file = "log.log";
		
		public static MainForm mainForm;
        
		[STAThread]
		private static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			mainForm = new MainForm();
			Application.Run(mainForm);
		}		
	}
}
