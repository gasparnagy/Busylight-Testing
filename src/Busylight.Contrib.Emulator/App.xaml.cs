using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Owin.Hosting;

namespace Busylight.Contrib.Emulator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            const string EMULATOR_PROCESS_NAME = "Busylight.Contrib.Emulator";
            if (Process.GetProcessesByName(EMULATOR_PROCESS_NAME).Length > 1)
            {
                MessageBox.Show("An instance of the emulator is already running!", "Busylight Emulator", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown(1);
                return;
            }

            try
            {
                // Start OWIN host 
                WebApp.Start<Startup>(url: "http://localhost:12916/");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error starting emulator: " + Environment.NewLine + ex, "Busylight Emulator", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Shutdown(2);
            }
        }
    }
}
