using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    public sealed class PulsateHeadMountedDisplayEntryPoint :
        IStartable,
        IDisposable
    {
        private readonly IHeadMountedDisplayHardware hardware;

        private readonly HeadMountedDisplayThreshold threshold;

        private readonly IPulsatedHeadMountedDisplaySoftware software;

        private ICancellation cancellation;

        [Inject]
        public PulsateHeadMountedDisplayEntryPoint(IHeadMountedDisplayHardware hardware, HeadMountedDisplayThreshold threshold, IPulsatedHeadMountedDisplaySoftware software)
        {
            this.hardware = hardware;

            this.threshold = threshold;

            this.software = software;
        }

        public void Start()
        {
            cancellation
                = hardware
                .Pulsate(threshold)
                .Connect()
                .Connect(software);
        }

        public void Dispose()
        {
            cancellation.Cancel();

            cancellation = null;
        }
    }
}
