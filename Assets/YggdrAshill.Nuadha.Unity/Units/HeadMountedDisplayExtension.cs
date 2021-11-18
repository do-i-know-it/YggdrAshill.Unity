using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class HeadMountedDisplayExtension
    {
        public static IIgnition<IHeadMountedDisplaySoftware> Ignite(this IHeadMountedDisplayProtocol protocol, IHeadMountedDisplayConfiguration configuration)
        {
            if (protocol is null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new IgniteHeadMountedDisplay(protocol, configuration);
        }
        private sealed class IgniteHeadMountedDisplay :
            IIgnition<IHeadMountedDisplaySoftware>
        {
            private readonly IIgnition<IPoseTrackerSoftware> origin;

            private readonly IIgnition<IHeadTrackerSoftware> head;

            private readonly IIgnition<IHandControllerSoftware> leftHand;

            private readonly IIgnition<IHandControllerSoftware> rightHand;

            internal IgniteHeadMountedDisplay(IHeadMountedDisplayProtocol protocol, IHeadMountedDisplayConfiguration configuration)
            {
                origin = protocol.Origin.Ignite(configuration.Origin);

                head = protocol.Head.Ignite(configuration.Head);

                leftHand = protocol.LeftHand.Ignite(configuration.LeftHand);

                rightHand = protocol.RightHand.Ignite(configuration.RightHand);
            }

            public ICancellation Connect(IHeadMountedDisplaySoftware module)
            {
                if (module == null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                var composite = new CompositeCancellation();

                origin.Connect(module.Origin).Synthesize(composite);
                head.Connect(module.Head).Synthesize(composite);
                leftHand.Connect(module.LeftHand).Synthesize(composite);
                rightHand.Connect(module.RightHand).Synthesize(composite);

                return composite;
            }

            public void Emit()
            {
                origin.Emit();
                head.Emit();
                leftHand.Emit();
                rightHand.Emit();
            }

            public void Dispose()
            {
                origin.Dispose();
                head.Dispose();
                leftHand.Dispose();
                rightHand.Dispose();
            }
        }

        public static IConnection<IThreePointPoseTrackerSoftware> Calibrate(this IHeadMountedDisplayHardware module, IThreePointPoseTrackerConfiguration configuration)
        {
            if (module is null)
            {
                throw new ArgumentNullException(nameof(module));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new ConnectThreePointPoseTracker(module, configuration);
        }
        private sealed class ConnectThreePointPoseTracker :
            IConnection<IThreePointPoseTrackerSoftware>
        {
            private readonly IConnection<IPoseTrackerSoftware> origin;

            private readonly IConnection<IPoseTrackerSoftware> head;

            private readonly IConnection<IPoseTrackerSoftware> leftHand;

            private readonly IConnection<IPoseTrackerSoftware> rightHand;

            internal ConnectThreePointPoseTracker(IHeadMountedDisplayHardware module, IThreePointPoseTrackerConfiguration configuration)
            {
                origin = module.Origin.Calibrate(configuration.Origin);

                head = module.Head.Pose.Calibrate(configuration.Head);

                leftHand = module.LeftHand.Pose.Calibrate(configuration.LeftHand);

                rightHand = module.RightHand.Pose.Calibrate(configuration.RightHand);
            }

            public ICancellation Connect(IThreePointPoseTrackerSoftware module)
            {
                if (module is null)
                {
                    throw new ArgumentNullException(nameof(module));
                }

                var composite = new CompositeCancellation();

                origin.Connect(module.Origin).Synthesize(composite);
                head.Connect(module.Head).Synthesize(composite);
                leftHand.Connect(module.LeftHand).Synthesize(composite);
                rightHand.Connect(module.RightHand).Synthesize(composite);

                return composite;
            }
        }
    }
}
