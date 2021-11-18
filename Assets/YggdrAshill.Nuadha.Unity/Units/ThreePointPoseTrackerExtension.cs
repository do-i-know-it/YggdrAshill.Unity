using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Unitization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Units;
using System;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ThreePointPoseTrackerExtension
    {
        public static IIgnition<IThreePointPoseTrackerSoftware> Ignite(this IThreePointPoseTrackerProtocol protocol, IThreePointPoseTrackerConfiguration configuration)
        {
            if (protocol is null)
            {
                throw new ArgumentNullException(nameof(protocol));
            }
            if (configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            return new IgniteThreePointPoseTracker(protocol, configuration);
        }
        private sealed class IgniteThreePointPoseTracker :
            IIgnition<IThreePointPoseTrackerSoftware>
        {
            private readonly IIgnition<IPoseTrackerSoftware> origin;

            private readonly IIgnition<IPoseTrackerSoftware> head;

            private readonly IIgnition<IPoseTrackerSoftware> leftHand;

            private readonly IIgnition<IPoseTrackerSoftware> rightHand;

            internal IgniteThreePointPoseTracker(IThreePointPoseTrackerProtocol protocol, IThreePointPoseTrackerConfiguration configuration)
            {
                origin = protocol.Origin.Ignite(configuration.Origin);

                head = protocol.Head.Ignite(configuration.Head);

                leftHand = protocol.LeftHand.Ignite(configuration.LeftHand);

                rightHand = protocol.RightHand.Ignite(configuration.RightHand);
            }

            public ICancellation Connect(IThreePointPoseTrackerSoftware module)
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

        public static IConnection<IThreePointPoseTrackerSoftware> Calibrate(this IThreePointPoseTrackerHardware module, IThreePointPoseTrackerConfiguration configuration)
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

            internal ConnectThreePointPoseTracker(IThreePointPoseTrackerHardware module, IThreePointPoseTrackerConfiguration configuration)
            {
                origin = module.Origin.Calibrate(configuration.Origin);

                head = module.Head.Calibrate(configuration.Head);

                leftHand = module.LeftHand.Calibrate(configuration.LeftHand);

                rightHand = module.RightHand.Calibrate(configuration.RightHand);
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
