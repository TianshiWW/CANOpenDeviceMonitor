using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CANopenLib
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			try
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				//Application.Run(new ModbusRTUMultiThreadTestForm());
				//Application.Run(new ModbusRTUTestForm());
				//Application.Run(new EthernetSCLTestForm());
				//Application.Run(new UnifiedSCLTestForm());
				Application.Run(new MainForm());
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
	}
}
