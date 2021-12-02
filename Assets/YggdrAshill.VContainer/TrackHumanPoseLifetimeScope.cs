using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    [DisallowMultipleComponent]
    public abstract class TrackHumanPoseLifetimeScope : LifetimeScope
    {
        protected abstract IHumanPoseTrackerHardware Hardware { get; }

        protected abstract IHumanPoseTrackerSoftware Software { get; }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(Hardware)
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
            private readonly IHumanPoseTrackerHardware hardware;

            private readonly IHumanPoseTrackerSoftware software;

            private ICancellation cancellation;

            [Inject]
            public EntryPoint(IHumanPoseTrackerHardware hardware, IHumanPoseTrackerSoftware software)
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
}
