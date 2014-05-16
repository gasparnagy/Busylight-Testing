using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shell;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Contrib.Emulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Timer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new Timer(Callback, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));

            FakeBusylightController.Instance.Changed += controller => Callback(null);
        }

        private void Callback(object state)
        {
            var color = FakeBusylightController.Instance.Color;
            var alertSoundClip = FakeBusylightController.Instance.AlertSoundClip;

            Dispatcher.BeginInvoke(new Action(() =>
            {
                Rectangle.Fill = new SolidColorBrush(Color.FromRgb((byte)color.RedRgbValue, (byte)color.GreenRgbValue, (byte)color.BlueRgbValue));
                Label.Content = alertSoundClip == BusylightSoundClip.Quiet ? "" : "Alert: " + alertSoundClip;

                TaskBarItemInfo.ProgressValue = 1;
                if (alertSoundClip != BusylightSoundClip.Quiet)
                    TaskBarItemInfo.ProgressState = TaskbarItemProgressState.Indeterminate;
                else if (IsRed(color))
                    TaskBarItemInfo.ProgressState = TaskbarItemProgressState.Error;
                else if (IsGreen(color))
                    TaskBarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
                else if (IsYellow(color))
                    TaskBarItemInfo.ProgressState = TaskbarItemProgressState.Paused;
                else
                    TaskBarItemInfo.ProgressState = TaskbarItemProgressState.None;
            }));
        }

        private bool IsGreen(BusylightColor color)
        {
            return color == BusylightColor.Green || 
                (color.GreenRgbValue == 255 && color.BlueRgbValue < 50 && color.RedRgbValue < 50);
        }

        private bool IsYellow(BusylightColor color)
        {
            return color == BusylightColor.Yellow ||
                (color.GreenRgbValue == 255 && color.BlueRgbValue < 50 && color.RedRgbValue == 255); 
        }

        private bool IsRed(BusylightColor color)
        {
            return color == BusylightColor.Red ||
                (color.RedRgbValue == 255 && color.BlueRgbValue < 50 && color.GreenRgbValue < 50);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            timer.Dispose();
        }

        private void LightTest_Click(object sender, RoutedEventArgs e)
        {
            var c = new Client.BusylightEmulatorController();
            c.Light(BusylightColor.Yellow);
        }

        private void AlertTest_Click(object sender, RoutedEventArgs e)
        {
            var c = new Client.BusylightEmulatorController();
            c.Alert(BusylightColor.Blue, BusylightSoundClip.OpenOffice, BusylightVolume.Middle);
        }

        private void PulseTest_Click(object sender, RoutedEventArgs e)
        {
            var c = new Client.BusylightEmulatorController();
            c.Pulse(new PulseSequence
            {
                Color = BusylightColor.Green,
                Step1 = 0,
                Step2 = 50,
                Step3 = 100,
                Step4 = 255,
                Step5 = 255,
                Step6 = 100,
                Step7 = 50,
                Step8 = 0
            });
        }

        private void OffTest_Click(object sender, RoutedEventArgs e)
        {
            var c = new Client.BusylightEmulatorController();
            c.Light(BusylightColor.Off);
        }
    }
}
