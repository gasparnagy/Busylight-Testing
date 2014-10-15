using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using Busylight.Contrib.Interfaces;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Contrib.Emulator.Client
{
    public class BusylightEmulatorController : IBusylightController
    {
        const string BASE_ADDRESS = "http://localhost:12916/";

        private void Invoke(string action, string parametersFormat, params object[] parametersArgs)
        {
            try
            {
                var client = new HttpClient();
                client.GetAsync(BASE_ADDRESS + string.Format("api/BusylightEmulator/{0}?", action) +
                                string.Format(parametersFormat, parametersArgs ?? new object[0]));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "EMULATOR_ERROR");
            }
        }

        public void Light(BusylightColor color)
        {
            Invoke("Light", "r={0}&g={1}&b={2}", color.RedRgbValue, color.GreenRgbValue, color.BlueRgbValue);
        }

        public void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            Invoke("Alert", "r={0}&g={1}&b={2}&clip={3}&vol={4}", color.RedRgbValue, color.GreenRgbValue, color.BlueRgbValue, clip, volume);
        }

        public void Pulse(PulseSequence pulseSequence)
        {
            Invoke("Pulse", "r={0}&g={1}&b={2}&s1={3}&s2={4}&s3={5}&s4={6}&s5={7}&s6={8}&s7={9}&s8={10}",
                pulseSequence.Color.RedRgbValue, pulseSequence.Color.GreenRgbValue, pulseSequence.Color.BlueRgbValue,
                pulseSequence.Step1, pulseSequence.Step2, pulseSequence.Step3, pulseSequence.Step4,
                pulseSequence.Step5, pulseSequence.Step6, pulseSequence.Step7, pulseSequence.Step8);
        }

        public void StopKeepAliveSignaling()
        {
        }

        public void AlertAndReturn(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            Invoke("AlertAndReturn", "r={0}&g={1}&b={2}&clip={3}&vol={4}", color.RedRgbValue, color.GreenRgbValue, color.BlueRgbValue, clip, volume);
        }
    }
}