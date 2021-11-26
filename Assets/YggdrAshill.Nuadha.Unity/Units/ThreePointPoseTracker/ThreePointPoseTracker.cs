using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IThreePointPoseTrackerProtocol"/>.
    /// </summary>
    public sealed class ThreePointPoseTracker :
        IThreePointPoseTrackerHardware,
        IThreePointPoseTrackerSoftware,
        IThreePointPoseTrackerProtocol
    {
        /// <summary>
        /// Converts <see cref="IThreePointPoseTrackerConfiguration"/> into <see cref="ITransmission{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IThreePointPoseTrackerConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/> converted from <see cref="IThreePointPoseTrackerConfiguration"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IThreePointPoseTrackerSoftware> Transmit(IThreePointPoseTrackerConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return WithoutCache().Transmit(configuration);
        }

        /// <summary>
        /// <see cref="IThreePointPoseTrackerProtocol"/> without cache.
        /// </summary>
        /// <returns>
        /// <see cref="IThreePointPoseTrackerProtocol"/> initialized.
        /// </returns>
        public static IThreePointPoseTrackerProtocol WithoutCache()
        {
            return
                new ThreePointPoseTracker(
                    PoseTracker.WithoutCache(),
                    PoseTracker.WithoutCache(),
                    PoseTracker.WithoutCache(),
                    PoseTracker.WithoutCache());
        }

        /// <summary>
        /// <see cref="IThreePointPoseTrackerProtocol"/> with latest cache.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IThreePointPoseTrackerConfiguration"/> to initialize.
        /// </param>
        /// <returns>
        /// <see cref="IThreePointPoseTrackerProtocol"/> initialized.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static IThreePointPoseTrackerProtocol WithLatestCache(IThreePointPoseTrackerConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return
                new ThreePointPoseTracker(
                    PoseTracker.WithLatestCache(configuration.Origin),
                    PoseTracker.WithLatestCache(configuration.Head),
                    PoseTracker.WithLatestCache(configuration.LeftHand),
                    PoseTracker.WithLatestCache(configuration.RightHand));
        }

        /// <summary>
        /// <see cref="IThreePointPoseTrackerProtocol"/> with latest cache.
        /// </summary>
        /// <returns>
        /// <see cref="IThreePointPoseTrackerProtocol"/> initialized.
        /// </returns>
        public static IThreePointPoseTrackerProtocol WithLatestCache()
        {
            return
                new ThreePointPoseTracker(
                    PoseTracker.WithLatestCache(),
                    PoseTracker.WithLatestCache(),
                    PoseTracker.WithLatestCache(),
                    PoseTracker.WithLatestCache());
        }

        private ThreePointPoseTracker(IPoseTrackerProtocol origin, IPoseTrackerProtocol head, IPoseTrackerProtocol leftHand, IPoseTrackerProtocol rightHand)
        {
            Origin = origin;
            Head = head;
            LeftHand = leftHand;
            RightHand = rightHand;
        }

        /// <inheritdoc/>
        public IPoseTrackerProtocol Origin { get; }

        /// <inheritdoc/>
        public IPoseTrackerProtocol Head { get; }

        /// <inheritdoc/>
        public IPoseTrackerProtocol LeftHand { get; }

        /// <inheritdoc/>
        public IPoseTrackerProtocol RightHand { get; }

        /// <inheritdoc/>
        public IThreePointPoseTrackerHardware Hardware => this;

        /// <inheritdoc/>
        public IThreePointPoseTrackerSoftware Software => this;

        /// <inheritdoc/>
        IPoseTrackerHardware IThreePointPoseTrackerHardware.Origin => Origin.Hardware;

        /// <inheritdoc/>
        IPoseTrackerHardware IThreePointPoseTrackerHardware.Head => Head.Hardware;

        /// <inheritdoc/>
        IPoseTrackerHardware IThreePointPoseTrackerHardware.LeftHand => LeftHand.Hardware;

        /// <inheritdoc/>
        IPoseTrackerHardware IThreePointPoseTrackerHardware.RightHand => RightHand.Hardware;

        /// <inheritdoc/>
        IPoseTrackerSoftware IThreePointPoseTrackerSoftware.Origin => Origin.Software;

        /// <inheritdoc/>
        IPoseTrackerSoftware IThreePointPoseTrackerSoftware.Head => Head.Software;

        /// <inheritdoc/>
        IPoseTrackerSoftware IThreePointPoseTrackerSoftware.LeftHand => LeftHand.Software;

        /// <inheritdoc/>
        IPoseTrackerSoftware IThreePointPoseTrackerSoftware.RightHand => RightHand.Software;
    }
}
