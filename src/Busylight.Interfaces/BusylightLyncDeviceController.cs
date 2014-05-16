using System;
using System.Linq;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Interfaces
{
    public class BusylightLyncDeviceController : BusylightDeviceController
    {
        public BusylightLyncDeviceController() : base(new BusylightLyncController())
        {
        }
    }
}