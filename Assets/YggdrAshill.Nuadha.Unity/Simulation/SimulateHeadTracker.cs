using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulateHeadTracker :
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
        public static IHeadTrackerConfiguration ToConfigure(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var pose = SimulatePoseTracker.ToConfigure(transform);

            return new SimulateHeadTracker(pose);
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
        public static IHeadTrackerConfiguration ToConfigure(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            var pose = SimulatePoseTracker.ToConfigure(origin, transform);

            return new SimulateHeadTracker(pose);
        }

        private SimulateHeadTracker(IPoseTrackerConfiguration pose)
        {
            Pose = pose;

            Direction = SimulateSpace3D.ToGenerateDirection(Vector3.forward);
        }

        /// <inheritdoc/>
        public IPoseTrackerConfiguration Pose { get; }

        /// <inheritdoc/>
        public IGeneration<Space3D.Direction> Direction { get; }
    }
}
