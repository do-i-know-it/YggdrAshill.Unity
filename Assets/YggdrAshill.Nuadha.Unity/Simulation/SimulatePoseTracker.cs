using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulatePoseTracker
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
        public static IPoseTrackerConfiguration ToConfigure(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new PoseTrackerConfiguration()
            {
                Position = SimulateSpace3D.ToGeneratePosition(transform),
                Rotation = SimulateSpace3D.ToGenerateRotation(transform),
            };
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
        public static IPoseTrackerConfiguration ToConfigure(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new PoseTrackerConfiguration()
            {
                Position = SimulateSpace3D.ToGeneratePosition(origin, transform),
                Rotation = SimulateSpace3D.ToGenerateRotation(origin, transform),
            };
        }

        public static IPoseTrackerConfiguration ToConfigure(Pose pose)
        {
            return new PoseTrackerConfiguration()
            {
                Position = SimulateSpace3D.ToGeneratePosition(pose.position),
                Rotation = SimulateSpace3D.ToGenerateRotation(pose.rotation),
            };
        }

        public static IPoseTrackerConfiguration FixedPose { get; } = ToConfigure(Pose.identity);

        private sealed class PoseTrackerConfiguration :
            IPoseTrackerConfiguration
        {
            public IGeneration<Space3D.Position> Position { get; set; }

            public IGeneration<Space3D.Rotation> Rotation { get; set; }
        }

        public static IPoseTrackerSoftware ToTrack(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new PoseTrackerSoftware()
            {
                Position = SimulateSpace3D.ToConsumePosition(transform),
                Rotation = SimulateSpace3D.ToConsumeRotation(transform),
            };
        }

        public static IPoseTrackerSoftware ToTrack(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new PoseTrackerSoftware()
            {
                Position = SimulateSpace3D.ToConsumePosition(origin, transform),
                Rotation = SimulateSpace3D.ToConsumeRotation(origin, transform),
            };
        }

        private sealed class PoseTrackerSoftware :
            IPoseTrackerSoftware
        {
            public IConsumption<Space3D.Position> Position { get; set; }

            public IConsumption<Space3D.Rotation> Rotation { get; set; }
        }
    }
}
