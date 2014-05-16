using System;
using System.Linq;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Contrib.Interfaces
{
    public class BusylightLyncDeviceController : BusylightDeviceController
    {
        public BusylightLyncDeviceController() : base(new BusylightLyncController())
        {
        }
    }
}