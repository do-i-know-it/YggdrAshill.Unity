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

            var position = GenerateSpace3D.AbsolutePosition(transform);

            var rotation = GenerateSpace3D.AbsoluteRotation(transform);

            return new SimulatedPoseTracker(position, rotation);
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

            var position = GenerateSpace3D.RelativePosition(origin, transform);

            var rotation = GenerateSpace3D.RelativeRotation(origin, transform);

            return new SimulatedPoseTracker(position, rotation);
        }

        private SimulatedPoseTracker(IGeneration<Space3D.Position> position, IGeneration<Space3D.Rotation> rotation)
        {
            Position = position;

            Rotation = rotation;
        }

        /// <inheritdoc/>
        public IGeneration<Space3D.Position> Position { get; }

        /// <inheritdoc/>
        public IGeneration<Space3D.Rotation> Rotation { get; }
    }
}
