using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class HeadMountedDisplayExtension
    {
        public static ITransmission<IHeadMountedDisplaySoftware> Transmit(this IHeadMountedDisplayProtocol protocol, IHeadMountedDisplayConfiguration configuration)
        {
            if (protocol is null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return ConvertHeadMountedDisplayInto.Transmission(protocol, configuration);
        }

        /// <summary>
        /// Converts <see cref="IHeadMountedDisplaySoftware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplayHardware"/>.
        /// </summary>
        /// <param name="software">
        /// <see cref="IHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplayHardware"/> converted from <see cref="IHeadMountedDisplaySoftware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="software"/> is null.
        /// </exception>
        public static IConnection<IHeadMountedDisplayHardware> Connect(this IHeadMountedDisplaySoftware software)
        {
            if (software == null)
            {
                throw new ArgumentNullException(nameof(software));
            }

            return ConvertHeadMountedDisplayInto.Connection(software);
        }

        /// <summary>
        /// Converts <see cref="IHeadMountedDisplayHardware"/> into <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/>.
        /// </summary>
        /// <param name="hardware">
        /// <see cref="IHeadMountedDisplaySoftware"/> to convert.
        /// </param>
        /// <returns>
        /// <see cref="IConnection{TModule}"/> for <see cref="IHeadMountedDisplaySoftware"/> converted from <see cref="IHeadMountedDisplayHardware"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="hardware"/> is null.
        /// </exception>
        public static IConnection<IHeadMountedDisplaySoftware> Connect(this IHeadMountedDisplayHardware hardware)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }

            return ConvertHeadMountedDisplayInto.Connection(hardware);
        }

        public static IPulsatedHeadMountedDisplayHardware Pulsate(this IHeadMountedDisplayHardware hardware, IHeadMountedDisplayPulsation pulsation)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (pulsation == null)
            {
                throw new ArgumentNullException(nameof(pulsation));
            }

            return ConvertHeadMountedDisplayInto.PulsatedHeadMountedDisplay(hardware, pulsation);
        }

        public static IPulsatedHeadMountedDisplayHardware Pulsate(this IHeadMountedDisplayHardware hardware, HeadMountedDisplayThreshold threshold)
        {
            if (hardware == null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (threshold == null)



            {
                throw new ArgumentNullException(nameof(threshold));
            }

            return hardware.Pulsate(new HeadMountedDisplayPulsation(threshold));
        }
        private sealed class HeadMountedDisplayPulsation :
            IHeadMountedDisplayPulsation
        {
            internal HeadMountedDisplayPulsation(HeadMountedDisplayThreshold threshold)
            {
                LeftHand = Nuadha.Pulsate.HandController(threshold.LeftHand);

                RightHand = Nuadha.Pulsate.HandController(threshold.RightHand);
            }

            public IHandControllerPulsation LeftHand { get; }

            public IHandControllerPulsation RightHand { get; }
        }

        public static IHumanPoseTrackerHardware Correct(this IHeadMountedDisplayHardware hardware, IHumanPoseTrackerCorrection correction)
        {
            if (hardware is null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (correction is null)
            {
                throw new ArgumentNullException(nameof(correction));
            }

            return ConvertHeadMountedDisplayInto.HumanPoseTracker(hardware, correction);
        }

        public static IHumanPoseTrackerHardware Calibrate(this IHeadMountedDisplayHardware hardware, IHumanPoseTrackerConfiguration configuration)
        {
            if (hardware is null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return hardware.Correct(new HumanPoseTrackerCorrection(configuration));
        }
        private sealed class HumanPoseTrackerCorrection :
            IHumanPoseTrackerCorrection
        {
            internal HumanPoseTrackerCorrection(IHumanPoseTrackerConfiguration configuration)
            {
                Origin = PoseTrackerTo.Calibrate(configuration.Origin);

                Head = PoseTrackerTo.Calibrate(configuration.Head);

                LeftHand = PoseTrackerTo.Calibrate(configuration.LeftHand);

                RightHand = PoseTrackerTo.Calibrate(configuration.RightHand);
            }

            public IPoseTrackerCorrection Origin { get; }

            public IPoseTrackerCorrection Head { get; }

            public IPoseTrackerCorrection LeftHand { get; }

            public IPoseTrackerCorrection RightHand { get; }
        }
    }
}
