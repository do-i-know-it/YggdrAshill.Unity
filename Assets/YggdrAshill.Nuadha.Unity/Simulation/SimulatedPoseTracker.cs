using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IPoseTrackerConfiguration"/> simulated using <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public sealed class SimulatedPoseTracker :
        IPoseTrackerConfiguration
    {
        public static IPoseTrackerConfiguration FixedPose(Pose pose)
        {
            return new SimulatedPoseTracker(pose);
        }
        private SimulatedPoseTracker(Pose pose)
        {
            Position = GenerateSpace3D.Position(pose.position);

            Rotation = GenerateSpace3D.Rotation(pose.rotation);
        }

        public static IPoseTrackerConfiguration FixedPose()
        {
            return new SimulatedPoseTracker();
        }
        private SimulatedPoseTracker()
        {
            Position = GenerateSpace3D.Position(Vector3.zero);

            Rotation = GenerateSpace3D.Rotation(Quaternion.identity);
        }

        /// <summary>
        /// Simulated <see cref="IPoseTrackerConfiguration"/> to generate absolute position and rotation.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute position and rotation.
        /// </param>
        /// <returns>
        /// <see cref="IPoseTrackerConfiguration"/> created.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IPoseTrackerConfiguration Transform(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulatedPoseTracker(transform);
        }
        private SimulatedPoseTracker(Transform transform)
        {
            Position = GenerateSpace3D.AbsolutePosition(transform);

            Rotation = GenerateSpace3D.AbsoluteRotation(transform);
        }

        /// <summary>
        /// Simulated <see cref="IPoseTrackerConfiguration"/> to generate relative position and rotation.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for origin.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative position and rotation.
        /// </param>
        /// <returns>
        /// <see cref="IPoseTrackerConfiguration"/> created.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IPoseTrackerConfiguration Transform(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulatedPoseTracker(origin, transform);
        }
        private SimulatedPoseTracker(Transform origin, Transform transform)
        {
            Position = GenerateSpace3D.RelativePosition(origin, transform);

            Rotation = GenerateSpace3D.RelativeRotation(origin, transform);
        }

        /// <inheritdoc/>
        public IGeneration<Space3D.Position> Position { get; }

        /// <inheritdoc/>
        public IGeneration<Space3D.Rotation> Rotation { get; }
    }
}
