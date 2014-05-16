using System;
using System.Linq;
using System.Web.Http;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Contrib.Emulator
{
    public class BusylightEmulatorController : ApiController
    {
        private static BusylightColor GetColor(int r, int g, int b)
        {
            return new BusylightColor()
            {
                RedRgbValue = r,
                GreenRgbValue = g,
                BlueRgbValue = b
            };
        }

        [HttpGet]
        public void Light(int r = 0, int g = 0, int b = 0)
        {
            FakeBusylightController.Instance.Light(GetColor(r, g, b));
        }

        [HttpGet]
        public void Alert(int r = 0, int g = 0, int b = 0, BusylightSoundClip clip = BusylightSoundClip.Quiet, BusylightVolume vol = BusylightVolume.Middle)
        {
            FakeBusylightController.Instance.Alert(GetColor(r, g, b), clip, vol);
        }

        [HttpGet]
        public void Pulse(int r = 0, int g = 0, int b = 0, int s1 = 0, int s2 = 0, int s3 = 0, int s4 = 0, int s5 = 0, int s6 = 0, int s7 = 0, int s8 = 0)
        {
            FakeBusylightController.Instance.Pulse(new PulseSequence()
            {
                Color = GetColor(r, g, b),
                Step1 = s1,
                Step2 = s2,
                Step3 = s3,
                Step4 = s4,
                Step5 = s5,
                Step6 = s6,
                Step7 = s7,
                Step8 = s8
            });
        }

        [HttpGet]
        public void AlertAndReturn(int r = 0, int g = 0, int b = 0, BusylightSoundClip clip = BusylightSoundClip.Quiet, BusylightVolume vol = BusylightVolume.Middle)
        {
            FakeBusylightController.Instance.Alert(GetColor(r, g, b), clip, vol);
        }

    }
}
