using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    public sealed class TransmitHeadMountedDisplayEntryPoint :
        IStartable,
        ITickable,
        IDisposable
    {
        private readonly IHeadMountedDisplayConfiguration configuration;

        private readonly IHeadMountedDisplaySoftware software;

        private ITransmission<IHeadMountedDisplaySoftware> transmission;

        private ICancellation cancellation;

        [Inject]
        public TransmitHeadMountedDisplayEntryPoint(IHeadMountedDisplayConfiguration configuration, IHeadMountedDisplaySoftware software)
        {
            this.configuration = configuration;

            this.software = software;
        }

        public void Start()
        {
            transmission = HeadMountedDisplay.Transmit(configuration);

            cancellation = transmission.Connect(software);
        }

        public void Dispose()
        {
            cancellation.Cancel();

            cancellation = null;

            transmission = null;
        }

        public void Tick()
        {
            transmission.Emit();
        }
    }
}
