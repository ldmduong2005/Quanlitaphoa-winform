using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices; 

namespace QuanliTapHoaPro
{
    internal static class Program
    {
       
        [DllImport("Shcore.dll")]
        static extern int SetProcessDpiAwareness(int PROCESS_DPI_AWARENESS);

       
        private enum ProcessDpiAwareness
        {
            Process_DpiUnaware = 0,        
            Process_SystemDpiAware = 1,    
            Process_PerMonitorDpiAware = 2 
        }

        [STAThread]
        static void Main()
        {
            try
            {
              
                SetProcessDpiAwareness((int)ProcessDpiAwareness.Process_SystemDpiAware);
            }
            catch (Exception)
            {
                
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}