using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IHeadTrackerConfiguration"/> simulated using <see cref="UnityEngine.Transform"/>.
    /// </summary>
    public sealed class SimulatedHeadTracker :
        IHeadTrackerConfiguration
    {
        /// <summary>
        /// Simulated <see cref="IHeadTrackerConfiguration"/> to generate absolute position, direction and rotation.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute position, direction and rotation.
        /// </param>
        /// <returns>
        /// <see cref="IHeadTrackerConfiguration"/> created.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IHeadTrackerConfiguration Transform(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var pose = SimulatedPoseTracker.Transform(transform);

            var direction = GenerateSpace3D.AbsoluteDirection(transform);

            return new SimulatedHeadTracker(pose, direction);
        }

        /// <summary>
        /// Simulated <see cref="IHeadTrackerConfiguration"/> to generate relative position, direction and rotation.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for origin.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative position, direction and rotation.
        /// </param>
        /// <returns>
        /// <see cref="IHeadTrackerConfiguration"/> created.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IHeadTrackerConfiguration Transform(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var pose = SimulatedPoseTracker.Transform(origin, transform);

            var direction = GenerateSpace3D.RelativeDirection(origin, transform);

            return new SimulatedHeadTracker(pose, direction);
        }

        private SimulatedHeadTracker(IPoseTrackerConfiguration pose, IGeneration<Space3D.Direction> direction)
        {
            Pose = pose;

            Direction = direction;
        }

        /// <inheritdoc/>
        public IPoseTrackerConfiguration Pose { get; }

        /// <inheritdoc/>
        public IGeneration<Space3D.Direction> Direction { get; }
    }
}
