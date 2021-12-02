using YggdrAshill.Nuadha.Unitization;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class PulsatedHeadMountedDisplayExtension
    {
        /// <summary>
        /// Converts <see cref="IPulsatedHeadMountedDisplaySoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplayHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IPulsatedHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplayHardware"/> converted from <see cref="IPulsatedHeadMountedDisplaySoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IPulsatedHeadMountedDisplayHardware> Connect(this IPulsatedHeadMountedDisplaySoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return ConvertPulsatedHeadMountedDisplayInto.Connection(software);
        }

        /// <summary>
        /// Converts <see cref="IPulsatedHeadMountedDisplayHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplaySoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IPulsatedHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IPulsatedHeadMountedDisplaySoftware"/> converted from <see cref="IPulsatedHeadMountedDisplayHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IPulsatedHeadMountedDisplaySoftware> Connect(this IPulsatedHeadMountedDisplayHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return ConvertPulsatedHeadMountedDisplayInto.Connection(hardware);
        }
    }
}
