using System;
using System.Linq;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Interfaces
{
    public abstract class BusylightDeviceController : IBusylightController
    {
        protected readonly BusylightController DeviceController;

        protected BusylightDeviceController(BusylightController deviceController)
        {
            this.DeviceController = deviceController;
        }

        public void Light(BusylightColor color)
        {
            DeviceController.Light(color);
        }

        public void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            DeviceController.Alert(color, clip, volume);
        }

        public void Pulse(PulseSequence pulseSequence)
        {
            DeviceController.Pulse(pulseSequence);
        }

        public void StopKeepAliveSignaling()
        {
            DeviceController.StopKeepAliveSignaling();
        }

        public void AlertAndReturn(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            DeviceController.AlertAndReturn(color, clip, volume);
        }
    }
}