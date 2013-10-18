using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Logger
{
	static class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			MainForm mainForm = new MainForm();

			if (args.Length != 0)
			{
				MainForm.updateResult = args[0];
			}

			SystemEvents.SessionEnding += new SessionEndingEventHandler(mainForm.OnStop);

			Application.Run(mainForm);
		}
	}
}
