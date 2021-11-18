using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;

namespace YggdrAshill.Unity
{
    /// <summary>
    /// Implementation of <see cref="IHeadMountedDisplayProtocol"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public sealed class HeadMountedDisplay : Singleton<HeadMountedDisplay>,
        IHeadMountedDisplayProtocol
    {
        /// <summary>
        /// Ignites <see cref="IHeadMountedDisplaySoftware"/> with <see cref="IHeadMountedDisplayConfiguration"/>.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IHeadMountedDisplayConfiguration"/> to ignite.
        /// </param>
        /// <returns>
        /// <see cref="IIgnition{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static IIgnition<IHeadMountedDisplaySoftware> Ignite(IHeadMountedDisplayConfiguration configuration)
        {
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new Protocol().Ignite(configuration);
        }

        /// <inheritdoc/>
        public IPoseTrackerProtocol Origin => protocol.Origin;

        /// <inheritdoc/>
        public IHeadTrackerProtocol Head => protocol.Head;

        /// <inheritdoc/>
        public IHandControllerProtocol LeftHand => protocol.LeftHand;

        /// <inheritdoc/>
        public IHandControllerProtocol RightHand => protocol.RightHand;

        /// <inheritdoc/>
        public IHeadMountedDisplayHardware Hardware => protocol.Hardware;

        /// <inheritdoc/>
        public IHeadMountedDisplaySoftware Software => protocol.Software;

        /// <inheritdoc/>
        public void Dispose()
        {
            protocol.Dispose();
        }

        protected override void OnDestroy()
        {
            Dispose();

            base.OnDestroy();
        }

        private readonly Protocol protocol = new Protocol();
        private sealed class Protocol :
            IHeadMountedDisplayHardware,
            IHeadMountedDisplaySoftware,
            IHeadMountedDisplayProtocol
        {
            public IPoseTrackerProtocol Origin { get; } = PoseTracker.WithLatestCache();

            public IHeadTrackerProtocol Head { get; } = HeadTracker.WithLatestCache();

            public IHandControllerProtocol LeftHand { get; } = HandController.WithLatestCache();

            public IHandControllerProtocol RightHand { get; } = HandController.WithLatestCache();

            public IHeadMountedDisplayHardware Hardware => this;

            public IHeadMountedDisplaySoftware Software => this;

            IPoseTrackerHardware IHeadMountedDisplayHardware.Origin => Origin.Hardware;

            IHeadTrackerHardware IHeadMountedDisplayHardware.Head => Head.Hardware;

            IHandControllerHardware IHeadMountedDisplayHardware.LeftHand => LeftHand.Hardware;

            IHandControllerHardware IHeadMountedDisplayHardware.RightHand => RightHand.Hardware;

            IPoseTrackerSoftware IHeadMountedDisplaySoftware.Origin => Origin.Software;

            IHeadTrackerSoftware IHeadMountedDisplaySoftware.Head => Head.Software;

            IHandControllerSoftware IHeadMountedDisplaySoftware.LeftHand => LeftHand.Software;

            IHandControllerSoftware IHeadMountedDisplaySoftware.RightHand => RightHand.Software;

            public void Dispose()
            {
                Origin.Dispose();

                Head.Dispose();

                LeftHand.Dispose();

                RightHand.Dispose();
            }
        }
    }
}
