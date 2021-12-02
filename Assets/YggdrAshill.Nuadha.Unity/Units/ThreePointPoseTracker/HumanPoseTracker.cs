using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IHumanPoseTrackerProtocol"/>.
    /// </summary>
    public sealed class HumanPoseTracker :
        IHumanPoseTrackerHardware,
        IHumanPoseTrackerSoftware,
        IHumanPoseTrackerProtocol
    {
        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerConfiguration"/> into <see cref="ITransmission{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IHumanPoseTrackerConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/> converted from <see cref="IHumanPoseTrackerConfiguration"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IHumanPoseTrackerSoftware> Transmit(IHumanPoseTrackerConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return WithoutCache().Transmit(configuration);
        }

        /// <summary>
        /// <see cref="IHumanPoseTrackerProtocol"/> without cache.
        /// </summary>
        /// <returns>
        /// <see cref="IHumanPoseTrackerProtocol"/> initialized.
        /// </returns>
        public static IHumanPoseTrackerProtocol WithoutCache()
        {
            return
                new HumanPoseTracker(
                    PoseTracker.WithoutCache(),
                    PoseTracker.WithoutCache(),
                    PoseTracker.WithoutCache(),
                    PoseTracker.WithoutCache());
        }

        /// <summary>
        /// <see cref="IHumanPoseTrackerProtocol"/> with latest cache.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IHumanPoseTrackerConfiguration"/> to initialize.
        /// </param>
        /// <returns>
        /// <see cref="IHumanPoseTrackerProtocol"/> initialized.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static IHumanPoseTrackerProtocol WithLatestCache(IHumanPoseTrackerConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return
                new HumanPoseTracker(
                    PoseTracker.WithLatestCache(configuration.Origin),
                    PoseTracker.WithLatestCache(configuration.Head),
                    PoseTracker.WithLatestCache(configuration.LeftHand),
                    PoseTracker.WithLatestCache(configuration.RightHand));
        }

        /// <summary>
        /// <see cref="IHumanPoseTrackerProtocol"/> with latest cache.
        /// </summary>
        /// <returns>
        /// <see cref="IHumanPoseTrackerProtocol"/> initialized.
        /// </returns>
        public static IHumanPoseTrackerProtocol WithLatestCache()
        {
            return
                new HumanPoseTracker(
                    PoseTracker.WithLatestCache(),
                    PoseTracker.WithLatestCache(),
                    PoseTracker.WithLatestCache(),
                    PoseTracker.WithLatestCache());
        }

        private HumanPoseTracker(IPoseTrackerProtocol origin, IPoseTrackerProtocol head, IPoseTrackerProtocol leftHand, IPoseTrackerProtocol rightHand)
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
        public IHumanPoseTrackerHardware Hardware => this;

        /// <inheritdoc/>
        public IHumanPoseTrackerSoftware Software => this;

        /// <inheritdoc/>
        IPoseTrackerHardware IHumanPoseTrackerHardware.Origin => Origin.Hardware;

        /// <inheritdoc/>
        IPoseTrackerHardware IHumanPoseTrackerHardware.Head => Head.Hardware;

        /// <inheritdoc/>
        IPoseTrackerHardware IHumanPoseTrackerHardware.LeftHand => LeftHand.Hardware;

        /// <inheritdoc/>
        IPoseTrackerHardware IHumanPoseTrackerHardware.RightHand => RightHand.Hardware;

        /// <inheritdoc/>
        IPoseTrackerSoftware IHumanPoseTrackerSoftware.Origin => Origin.Software;

        /// <inheritdoc/>
        IPoseTrackerSoftware IHumanPoseTrackerSoftware.Head => Head.Software;

        /// <inheritdoc/>
        IPoseTrackerSoftware IHumanPoseTrackerSoftware.LeftHand => LeftHand.Software;

        /// <inheritdoc/>
        IPoseTrackerSoftware IHumanPoseTrackerSoftware.RightHand => RightHand.Software;
    }
}
