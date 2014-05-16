using System;
using System.Linq;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Interfaces
{
    public class BusylightUcDeviceController : BusylightDeviceController
    {
        public BusylightUcDeviceController() : base(new BusylightUcController())
        {
        }
    }
}