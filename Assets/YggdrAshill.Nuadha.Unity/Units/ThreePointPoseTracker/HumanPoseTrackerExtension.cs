using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class HumanPoseTrackerExtension
    {
        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerProtocol"/> into <see cref="ITransmission{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="protocol">
        /// <see cref="IHumanPoseTrackerProtocol"/> to convert.
        /// </param>
        /// <param name="configuration">
        /// <see cref="IHumanPoseTrackerConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/> converted from <see cref="IHumanPoseTrackerProtocol"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="protocol"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IHumanPoseTrackerSoftware> Transmit(this IHumanPoseTrackerProtocol protocol, IHumanPoseTrackerConfiguration configuration)
        {
            if (protocol is null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return ConvertHumanPoseTrackerInto.Transmission(protocol, configuration);
        }

        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerSoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IHumanPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerHardware"/> converted from <see cref="IHumanPoseTrackerSoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IHumanPoseTrackerHardware> Connect(this IHumanPoseTrackerSoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return ConvertHumanPoseTrackerInto.Connection(software);
        }

        /// <summary>
        /// Converts <see cref="IHumanPoseTrackerHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IHumanPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHumanPoseTrackerSoftware"/> converted from <see cref="IHumanPoseTrackerHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IHumanPoseTrackerSoftware> Connect(this IHumanPoseTrackerHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return ConvertHumanPoseTrackerInto.Connection(hardware);
        }
    }
}
