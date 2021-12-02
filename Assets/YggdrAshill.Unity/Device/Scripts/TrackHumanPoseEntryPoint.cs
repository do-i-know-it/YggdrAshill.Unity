using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    public sealed class TrackHumanPoseEntryPoint :
        IStartable,
        IDisposable
    {
        private readonly IHumanPoseTrackerHardware hardware;

        private readonly IHumanPoseTrackerSoftware software;

        private ICancellation cancellation;

        [Inject]
        public TrackHumanPoseEntryPoint(IHumanPoseTrackerHardware hardware, IHumanPoseTrackerSoftware software)
        {
            this.hardware = hardware;

            this.software = software;
        }

        public void Start()
        {
            cancellation
                = hardware
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
