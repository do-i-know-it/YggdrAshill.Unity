using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unity;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    public abstract class PulsateHeadMountedDisplayLifetimeScope : LifetimeScope
    {
        protected abstract IHeadMountedDisplayHardware Hardware { get; }

        protected abstract HeadMountedDisplayThreshold Threshold { get; }

        protected abstract IPulsatedHeadMountedDisplaySoftware Software { get; }

        protected override void Configure(IContainerBuilder builder)
        {
            builder
                .RegisterInstance(Hardware)
                .AsSelf();
            builder
                .RegisterInstance(Threshold)
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

            private readonly HeadMountedDisplayThreshold threshold;

            private readonly IPulsatedHeadMountedDisplaySoftware software;

            private ICancellation cancellation;

            [Inject]
            public EntryPoint(IHeadMountedDisplayHardware hardware, HeadMountedDisplayThreshold threshold, IPulsatedHeadMountedDisplaySoftware software)
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
}
