using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    public sealed class TransmitHumanPoseTrackerEntryPoint :
        IStartable,
        ITickable,
        IDisposable
    {
        private readonly IHumanPoseTrackerConfiguration configuration;

        private readonly IHumanPoseTrackerSoftware software;

        private ITransmission<IHumanPoseTrackerSoftware> transmission;

        private ICancellation cancellation;

        [Inject]
        public TransmitHumanPoseTrackerEntryPoint(IHumanPoseTrackerConfiguration configuration, IHumanPoseTrackerSoftware software)
        {
            this.configuration = configuration;

            this.software = software;
        }

        public void Start()
        {
            transmission = HumanPoseTracker.Transmit(configuration);

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
