using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class ToTrackPose
    {
        public static IPoseTrackerSoftware Absolute(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var position = ConsumeSpace3D.AbsolutePosition(transform);

            var rotation = ConsumeSpace3D.AbsoluteRotation(transform);

            return new PoseTrackerSoftware(position, rotation);
        }

        public static IPoseTrackerSoftware Relative(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var position = ConsumeSpace3D.RelativePosition(origin, transform);

            var rotation = ConsumeSpace3D.RelativeRotation(origin, transform);

            return new PoseTrackerSoftware(position, rotation);
        }

        private sealed class PoseTrackerSoftware :
            IPoseTrackerSoftware
        {
            internal PoseTrackerSoftware(IConsumption<Space3D.Position> position, IConsumption<Space3D.Rotation> rotation)
            {
                Position = position;

                Rotation = rotation;
            }

            public IConsumption<Space3D.Position> Position { get; }

            public IConsumption<Space3D.Rotation> Rotation { get; }
        }
    }
}
