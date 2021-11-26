using YggdrAshill.Nuadha.Units;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IPulsatedHeadMountedDisplayProtocol"/>.
    /// </summary>
    public sealed class PulsatedHeadMountedDisplay :
        IPulsatedHeadMountedDisplayHardware,
        IPulsatedHeadMountedDisplaySoftware,
        IPulsatedHeadMountedDisplayProtocol
    {
        
        /// <summary>
        /// <see cref="IPulsatedHeadMountedDisplayProtocol"/> without cache.
        /// </summary>
        /// <returns>
        /// <see cref="IPulsatedHeadMountedDisplayProtocol"/> initialized.
        /// </returns>
        public static IPulsatedHeadMountedDisplayProtocol WithoutCache()
        {
            return new PulsatedHeadMountedDisplay(PulsatedHandController.WithoutCache(), PulsatedHandController.WithoutCache());
        }

        /// <summary>
        /// <see cref="IPulsatedHeadMountedDisplayProtocol"/> with latest cache.
        /// </summary>
        /// <returns>
        /// <see cref="IPulsatedHeadMountedDisplayProtocol"/> initialized.
        /// </returns>
        public static IPulsatedHeadMountedDisplayProtocol WithLatestCache()
        {
            return new PulsatedHeadMountedDisplay(PulsatedHandController.WithLatestCache(), PulsatedHandController.WithLatestCache());
        }

        private PulsatedHeadMountedDisplay(IPulsatedHandControllerProtocol leftHand, IPulsatedHandControllerProtocol rightHand)
        {
            LeftHand = leftHand;
            RightHand = rightHand;
        }

        /// <inheritdoc/>
        public IPulsatedHandControllerProtocol LeftHand { get; }

        /// <inheritdoc/>
        public IPulsatedHandControllerProtocol RightHand { get; }

        /// <inheritdoc/>
        public IPulsatedHeadMountedDisplayHardware Hardware => this;

        /// <inheritdoc/>
        public IPulsatedHeadMountedDisplaySoftware Software => this;

        /// <inheritdoc/>
        IPulsatedHandControllerHardware IPulsatedHeadMountedDisplayHardware.LeftHand => LeftHand.Hardware;

        /// <inheritdoc/>
        IPulsatedHandControllerHardware IPulsatedHeadMountedDisplayHardware.RightHand => RightHand.Hardware;

        /// <inheritdoc/>
        IPulsatedHandControllerSoftware IPulsatedHeadMountedDisplaySoftware.LeftHand => LeftHand.Software;

        /// <inheritdoc/>
        IPulsatedHandControllerSoftware IPulsatedHeadMountedDisplaySoftware.RightHand => RightHand.Software;
    }
}
