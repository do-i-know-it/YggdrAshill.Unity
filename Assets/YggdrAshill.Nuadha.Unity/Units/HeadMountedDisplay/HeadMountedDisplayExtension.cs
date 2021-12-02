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
