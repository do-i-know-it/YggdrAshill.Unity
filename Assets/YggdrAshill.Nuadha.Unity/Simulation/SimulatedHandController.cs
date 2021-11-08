using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IPoseTrackerConfiguration"/> simulated using <see cref="Input"/> and <see cref="Transform"/>.
    /// </summary>
    public sealed class SimulatedHandController :
        IHandControllerConfiguration
    {
        private static IHandControllerConfiguration Left(IPoseTrackerConfiguration pose)
        {
            var thumb = SimulatedStick.WASD;

            var indexFinger = SimulatedTrigger.Keyboard(KeyCode.F);

            var handGrip = SimulatedTrigger.Keyboard(KeyCode.C);

            return new SimulatedHandController(pose, thumb, indexFinger, handGrip);
        }

        private static IHandControllerConfiguration Right(IPoseTrackerConfiguration pose)
        {
            var thumb = SimulatedStick.IJKL;

            var indexFinger = SimulatedTrigger.Keyboard(KeyCode.H);

            var handGrip = SimulatedTrigger.Keyboard(KeyCode.M);

            return new SimulatedHandController(pose, thumb, indexFinger, handGrip);
        }

        public static IHandControllerConfiguration Left(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return Left(SimulatedPoseTracker.Transform(transform));
        }

        public static IHandControllerConfiguration Right(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return Right(SimulatedPoseTracker.Transform(transform));
        }

        public static IHandControllerConfiguration Left(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return Left(SimulatedPoseTracker.Transform(origin, transform));
        }

        public static IHandControllerConfiguration Right(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return Right(SimulatedPoseTracker.Transform(origin, transform));
        }

        private SimulatedHandController(IPoseTrackerConfiguration pose, IStickConfiguration thumb, ITriggerConfiguration indexFinger, ITriggerConfiguration handGrip)
        {
            Pose = pose;

            Thumb = thumb;
            
            IndexFinger = indexFinger;
            
            HandGrip = handGrip;
        }

        /// <inheritdoc/>
        public IPoseTrackerConfiguration Pose { get; }

        /// <inheritdoc/>
        public IStickConfiguration Thumb { get; }

        /// <inheritdoc/>
        public ITriggerConfiguration IndexFinger { get; }

        /// <inheritdoc/>
        public ITriggerConfiguration HandGrip { get; }
    }
}
