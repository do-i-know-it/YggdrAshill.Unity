using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    public sealed class HeadMountedDisplayIntoHumanPoseTrackerEntryPoint :
        IStartable,
        IDisposable
    {
        private readonly IHeadMountedDisplayHardware hardware;

        private readonly IHumanPoseTrackerConfiguration configuration;

        private readonly IHumanPoseTrackerSoftware software;

        private ICancellation cancellation;

        [Inject]
        public HeadMountedDisplayIntoHumanPoseTrackerEntryPoint(IHeadMountedDisplayHardware hardware, IHumanPoseTrackerConfiguration configuration, IHumanPoseTrackerSoftware software)
        {
            this.hardware = hardware;

            this.configuration = configuration;

            this.software = software;
        }

        public void Start()
        {
            cancellation
                = hardware
                .Calibrate(configuration)
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
