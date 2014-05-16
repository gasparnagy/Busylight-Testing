using System;
using System.Linq;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Interfaces
{
    public interface IBusylightController
    {
        void Light(BusylightColor color);
        void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume);
        void Pulse(PulseSequence pulseSequence);
        void StopKeepAliveSignaling();
        void AlertAndReturn(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume);
    }
}