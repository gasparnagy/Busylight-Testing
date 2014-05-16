using System;
using System.Linq;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Contrib.Interfaces
{
    public class BusylightUcDeviceController : BusylightDeviceController
    {
        public BusylightUcDeviceController() : base(new BusylightUcController())
        {
        }
    }
}