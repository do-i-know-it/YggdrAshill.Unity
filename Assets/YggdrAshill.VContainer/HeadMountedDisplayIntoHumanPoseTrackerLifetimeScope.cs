using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    [DisallowMultipleComponent]
    public abstract class HeadMountedDisplayIntoHumanPoseTrackerLifetimeScope : LifetimeScope
    {
        protected abstract IHeadMountedDisplayHardware Hardware { get; }

        protected abstract IHumanPoseTrackerConfiguration Configuration { get; }

        protected abstract IHumanPoseTrackerSoftware Software { get; }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(Hardware)
                .AsSelf();
            builder
                .RegisterInstance(Configuration)
                .AsSelf();
            builder
                .RegisterInstance(Software)
                .AsSelf();

            builder.RegisterEntryPoint<EntryPoint>();
        }
        public sealed class EntryPoint :
            IStartable,
            IDisposable
        {
            private readonly IHeadMountedDisplayHardware hardware;

            private readonly IHumanPoseTrackerConfiguration configuration;

            private readonly IHumanPoseTrackerSoftware software;

            private ICancellation cancellation;

            [Inject]
            public EntryPoint(IHeadMountedDisplayHardware hardware, IHumanPoseTrackerConfiguration configuration, IHumanPoseTrackerSoftware software)
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
}
