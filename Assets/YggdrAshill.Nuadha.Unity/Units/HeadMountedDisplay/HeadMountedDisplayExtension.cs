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

        public static IThreePointPoseTrackerHardware Correct(this IHeadMountedDisplayHardware hardware, IThreePointPoseTrackerCorrection correction)
        {
            if (hardware is null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (correction is null)
            {
                throw new ArgumentNullException(nameof(correction));
            }

            return ConvertHeadMountedDisplayInto.ThreePointPoseTracker(hardware, correction);
        }

        public static IThreePointPoseTrackerHardware Calibrate(this IHeadMountedDisplayHardware hardware, IThreePointPoseTrackerConfiguration configuration)
        {
            if (hardware is null)
            {
                throw new ArgumentNullException(nameof(hardware));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return hardware.Correct(new ThreePointPoseTrackerCorrection(configuration));
        }
        private sealed class ThreePointPoseTrackerCorrection :
            IThreePointPoseTrackerCorrection
        {
            internal ThreePointPoseTrackerCorrection(IThreePointPoseTrackerConfiguration configuration)
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
