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
    public sealed class ThreePointPoseTracker : Singleton<ThreePointPoseTracker>,
        IThreePointPoseTrackerProtocol
    {
        /// <summary>
        /// Ignites <see cref="IThreePointPoseTrackerSoftware"/> with <see cref="IThreePointPoseTrackerConfiguration"/>.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IThreePointPoseTrackerConfiguration"/> to ignite.
        /// </param>
        /// <returns>
        /// <see cref="IIgnition{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static IIgnition<IThreePointPoseTrackerSoftware> Ignite(IThreePointPoseTrackerConfiguration configuration)
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
        public IPoseTrackerProtocol Head => protocol.Head;

        /// <inheritdoc/>
        public IPoseTrackerProtocol LeftHand => protocol.LeftHand;

        /// <inheritdoc/>
        public IPoseTrackerProtocol RightHand => protocol.RightHand;

        /// <inheritdoc/>
        public IThreePointPoseTrackerHardware Hardware => protocol.Hardware;

        /// <inheritdoc/>
        public IThreePointPoseTrackerSoftware Software => protocol.Software;

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
            IThreePointPoseTrackerHardware,
            IThreePointPoseTrackerSoftware,
            IThreePointPoseTrackerProtocol
        {
            public IPoseTrackerProtocol Origin { get; } = PoseTracker.WithLatestCache();

            public IPoseTrackerProtocol Head { get; } = PoseTracker.WithLatestCache();

            public IPoseTrackerProtocol LeftHand { get; } = PoseTracker.WithLatestCache();

            public IPoseTrackerProtocol RightHand { get; } = PoseTracker.WithLatestCache();

            public IThreePointPoseTrackerHardware Hardware => this;

            public IThreePointPoseTrackerSoftware Software => this;

            IPoseTrackerHardware IThreePointPoseTrackerHardware.Origin => Origin.Hardware;

            IPoseTrackerHardware IThreePointPoseTrackerHardware.Head => Head.Hardware;

            IPoseTrackerHardware IThreePointPoseTrackerHardware.LeftHand => LeftHand.Hardware;

            IPoseTrackerHardware IThreePointPoseTrackerHardware.RightHand => RightHand.Hardware;

            IPoseTrackerSoftware IThreePointPoseTrackerSoftware.Origin => Origin.Software;

            IPoseTrackerSoftware IThreePointPoseTrackerSoftware.Head => Head.Software;

            IPoseTrackerSoftware IThreePointPoseTrackerSoftware.LeftHand => LeftHand.Software;

            IPoseTrackerSoftware IThreePointPoseTrackerSoftware.RightHand => RightHand.Software;

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
