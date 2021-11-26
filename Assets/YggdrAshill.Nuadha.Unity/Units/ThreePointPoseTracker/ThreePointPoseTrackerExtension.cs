using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ThreePointPoseTrackerExtension
    {
        /// <summary>
        /// Converts <see cref="IThreePointPoseTrackerProtocol"/> into <see cref="ITransmission{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="protocol">
        /// <see cref="IThreePointPoseTrackerProtocol"/> to convert.
        /// </param>
        /// <param name="configuration">
        /// <see cref="IThreePointPoseTrackerConfiguration"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="ITransmission{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/> converted from <see cref="IThreePointPoseTrackerProtocol"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="protocol"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="configuration"/> is null.
        /// </exception>
        public static ITransmission<IThreePointPoseTrackerSoftware> Transmit(this IThreePointPoseTrackerProtocol protocol, IThreePointPoseTrackerConfiguration configuration)
        {
            if (protocol is null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return ConvertThreePointPoseTrackerInto.Transmission(protocol, configuration);
        }

        /// <summary>
        /// Converts <see cref="IThreePointPoseTrackerSoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IThreePointPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerHardware"/> converted from <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IThreePointPoseTrackerHardware> Connect(this IThreePointPoseTrackerSoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return ConvertThreePointPoseTrackerInto.Connection(software);
        }

        /// <summary>
        /// Converts <see cref="IThreePointPoseTrackerHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IThreePointPoseTrackerSoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IThreePointPoseTrackerSoftware"/> converted from <see cref="IThreePointPoseTrackerHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IThreePointPoseTrackerSoftware> Connect(this IThreePointPoseTrackerHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return ConvertThreePointPoseTrackerInto.Connection(hardware);
        }
    }
}
