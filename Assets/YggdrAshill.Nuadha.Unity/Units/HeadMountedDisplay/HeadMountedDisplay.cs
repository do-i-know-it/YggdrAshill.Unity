using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IHeadMountedDisplayProtocol"/>.
    /// </summary>
    public sealed class HeadMountedDisplay :
        IHeadMountedDisplayHardware,
        IHeadMountedDisplaySoftware,
        IHeadMountedDisplayProtocol
    {
        /// <summary>
        /// Converts <see cref="IHeadMountedDisplayConfiguration"/> into <see cref="ITransmission{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/>.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IHeadMountedDisplayConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/> converted from <see cref="IHeadMountedDisplayConfiguration"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IHeadMountedDisplaySoftware> Transmit(IHeadMountedDisplayConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return WithoutCache().Transmit(configuration);
        }

        /// <summary>
        /// <see cref="IHeadMountedDisplayProtocol"/> without cache.
        /// </summary>
        /// <returns>
        /// <see cref="IHeadMountedDisplayProtocol"/> initialized.
        /// </returns>
        public static IHeadMountedDisplayProtocol WithoutCache()
        {
            return
                new HeadMountedDisplay(
                    PoseTracker.WithoutCache(),
                    HeadTracker.WithoutCache(),
                    HandController.WithoutCache(),
                    HandController.WithoutCache());
        }

        /// <summary>
        /// <see cref="IHeadMountedDisplayProtocol"/> with latest cache.
        /// </summary>
        /// <param name="configuration">
        /// <see cref="IHeadMountedDisplayConfiguration"/> to initialize.
        /// </param>
        /// <returns>
        /// <see cref="IHeadMountedDisplayProtocol"/> initialized.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static IHeadMountedDisplayProtocol WithLatestCache(IHeadMountedDisplayConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return
                new HeadMountedDisplay(
                    PoseTracker.WithLatestCache(configuration.Origin),
                    HeadTracker.WithLatestCache(configuration.Head),
                    HandController.WithLatestCache(configuration.LeftHand),
                    HandController.WithLatestCache(configuration.RightHand));
        }

        /// <summary>
        /// <see cref="IHeadMountedDisplayProtocol"/> with latest cache.
        /// </summary>
        /// <returns>
        /// <see cref="IHeadMountedDisplayProtocol"/> initialized.
        /// </returns>
        public static IHeadMountedDisplayProtocol WithLatestCache()
        {
            return
                new HeadMountedDisplay(
                    PoseTracker.WithLatestCache(),
                    HeadTracker.WithLatestCache(),
                    HandController.WithLatestCache(),
                    HandController.WithLatestCache());
        }

        private HeadMountedDisplay(IPoseTrackerProtocol origin, IHeadTrackerProtocol head, IHandControllerProtocol leftHand, IHandControllerProtocol rightHand)
        {
            Origin = origin;
            Head = head;
            LeftHand = leftHand;
            RightHand = rightHand;
        }

        /// <inheritdoc/>
        public IPoseTrackerProtocol Origin { get; }

        /// <inheritdoc/>
        public IHeadTrackerProtocol Head { get; }

        /// <inheritdoc/>
        public IHandControllerProtocol LeftHand { get; }

        /// <inheritdoc/>
        public IHandControllerProtocol RightHand { get; }

        /// <inheritdoc/>
        public IHeadMountedDisplayHardware Hardware => this;

        /// <inheritdoc/>
        public IHeadMountedDisplaySoftware Software => this;

        /// <inheritdoc/>
        IPoseTrackerHardware IHeadMountedDisplayHardware.Origin => Origin.Hardware;

        /// <inheritdoc/>
        IHeadTrackerHardware IHeadMountedDisplayHardware.Head => Head.Hardware;

        /// <inheritdoc/>
        IHandControllerHardware IHeadMountedDisplayHardware.LeftHand => LeftHand.Hardware;

        /// <inheritdoc/>
        IHandControllerHardware IHeadMountedDisplayHardware.RightHand => RightHand.Hardware;

        /// <inheritdoc/>
        IPoseTrackerSoftware IHeadMountedDisplaySoftware.Origin => Origin.Software;

        /// <inheritdoc/>
        IHeadTrackerSoftware IHeadMountedDisplaySoftware.Head => Head.Software;

        /// <inheritdoc/>
        IHandControllerSoftware IHeadMountedDisplaySoftware.LeftHand => LeftHand.Software;

        /// <inheritdoc/>
        IHandControllerSoftware IHeadMountedDisplaySoftware.RightHand => RightHand.Software;
    }
}
