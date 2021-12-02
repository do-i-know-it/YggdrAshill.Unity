using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.VContainer
{
    [DisallowMultipleComponent]
    public abstract class TransmitHeadMountedDisplayLifetimeScope : LifetimeScope
    {
        protected abstract IHeadMountedDisplayConfiguration Configuration { get; }

        protected abstract IHeadMountedDisplaySoftware Software { get; }

        protected override void Configure(IContainerBuilder builder)
        {
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
            ITickable,
            IDisposable
        {
            private readonly IHeadMountedDisplayConfiguration configuration;

            private readonly IHeadMountedDisplaySoftware software;

            private ITransmission<IHeadMountedDisplaySoftware> transmission;

            private ICancellation cancellation;

            public EntryPoint(IHeadMountedDisplayConfiguration configuration, IHeadMountedDisplaySoftware software)
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
}
