using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    public sealed class HumanPoseOfHeadMountedDisplayEntryPoint :
        IStartable,
        IDisposable
    {
        private readonly IHeadMountedDisplayHardware hardware;

        private readonly IHumanPoseTrackerConfiguration configuration;

        private readonly IHumanPoseTrackerSoftware software;

        private ICancellation cancellation;

        [Inject]
        public HumanPoseOfHeadMountedDisplayEntryPoint(IHeadMountedDisplayHardware hardware, IHumanPoseTrackerConfiguration configuration, IHumanPoseTrackerSoftware software)
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
