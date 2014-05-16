using System;
using System.Linq;
using System.Threading;
using Busylight.Contrib.Interfaces;
using Plenom.Components.Busylight.Sdk;

namespace Busylight.Contrib.Emulator
{
    public class FakeBusylightController : IBusylightController
    {
        public static readonly FakeBusylightController Instance = new FakeBusylightController();

        private BusylightColor specifiedColor = BusylightColor.Off;
        private readonly Timer pulseTimer;
        private readonly Timer resetTimer;
        private PulseSequence specifiedPulseSequence;
        private int pulseSequenceIndex;
        private BusylightSoundClip specifiedAlertSoundClip = BusylightSoundClip.Quiet;

        public BusylightColor Color
        {
            get { return specifiedColor; }
        }

        public BusylightSoundClip AlertSoundClip
        {
            get { return specifiedAlertSoundClip; }
        }

        public event Action<FakeBusylightController> Changed; 

        public FakeBusylightController()
        {
            pulseTimer = new Timer(PulseTick, null, Timeout.Infinite, Timeout.Infinite);
            resetTimer = new Timer(Reset, null, Timeout.Infinite, Timeout.Infinite);
        }

        private void Reset(object state)
        {
            specifiedColor = BusylightColor.Off;
            specifiedAlertSoundClip = BusylightSoundClip.Quiet;

            RaiseChanged(false);
        }

        private void PulseTick(object state)
        {
            if (pulseSequenceIndex > 8)
            {
                pulseTimer.Change(Timeout.Infinite, Timeout.Infinite);
                specifiedAlertSoundClip = BusylightSoundClip.Quiet;
                return;
            }

            int stepValue = 0;
            switch (pulseSequenceIndex)
            {
                case 0:
                    stepValue = specifiedPulseSequence.Step1;
                    break;
                case 1:
                    stepValue = specifiedPulseSequence.Step2;
                    break;
                case 2:
                    stepValue = specifiedPulseSequence.Step3;
                    break;
                case 3:
                    stepValue = specifiedPulseSequence.Step4;
                    break;
                case 4:
                    stepValue = specifiedPulseSequence.Step5;
                    break;
                case 5:
                    stepValue = specifiedPulseSequence.Step6;
                    break;
                case 6:
                    stepValue = specifiedPulseSequence.Step7;
                    break;
                case 7:
                    stepValue = specifiedPulseSequence.Step8;
                    break;
            }

            specifiedColor = new BusylightColor()
            {
                RedRgbValue = specifiedPulseSequence.Color.RedRgbValue * stepValue / 255,
                GreenRgbValue = specifiedPulseSequence.Color.GreenRgbValue * stepValue / 255,
                BlueRgbValue = specifiedPulseSequence.Color.BlueRgbValue * stepValue / 255
            };
            pulseSequenceIndex++;
            RaiseChanged();
        }

        public void Light(BusylightColor color)
        {
            specifiedColor = color;
            RaiseChanged();
        }

        private void RaiseChanged(bool scheduleReset = true)
        {
            if (scheduleReset)
                resetTimer.Change(TimeSpan.FromSeconds(10), Timeout.InfiniteTimeSpan);

            if (Changed != null)
                Changed(this);
        }

        public void Alert(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            specifiedColor = color;
            specifiedAlertSoundClip = clip;
            StartPulsing(new PulseSequence()
            {
                Color = color,
                Step1 = 0,
                Step2 = 255,
                Step3 = 0,
                Step4 = 255,
                Step5 = 0,
                Step6 = 255,
                Step7 = 0,
                Step8 = 255
            });
            RaiseChanged();
        }

        public void Pulse(PulseSequence pulseSequence)
        {
            specifiedColor = pulseSequence.Color;
            StartPulsing(pulseSequence);
            RaiseChanged();
        }

        private void StartPulsing(PulseSequence pulseSequence)
        {
            pulseSequenceIndex = 0;
            specifiedPulseSequence = pulseSequence;
            pulseTimer.Change(TimeSpan.Zero, TimeSpan.FromSeconds(1));
        }

        public void StopKeepAliveSignaling()
        {
            
        }

        public void AlertAndReturn(BusylightColor color, BusylightSoundClip clip, BusylightVolume volume)
        {
            Alert(color, clip, volume);
        }
    }
}
