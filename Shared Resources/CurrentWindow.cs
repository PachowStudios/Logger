using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;

namespace Logger
{
	class CurrentWindow
	{
		[DllImport("user32.dll")]
		static extern int GetForegroundWindow();

		[DllImport("user32.dll")]
		static extern int GetWindowText(int hWnd, StringBuilder text, int count);

		public static string GetActiveWindow()
		{

			const int nChars = 256;
			int handle = 0;
			StringBuilder Buff = new StringBuilder(nChars);

			handle = GetForegroundWindow();

			if (GetWindowText(handle, Buff, nChars) > 0)
			{
				return Buff.ToString();
			}
			return null;
		}
	}
}
