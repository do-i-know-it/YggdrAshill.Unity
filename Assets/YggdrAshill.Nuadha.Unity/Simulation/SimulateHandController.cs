using YggdrAshill.Nuadha.Units;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulateHandController :
        IHandControllerConfiguration
    {
        public static IHandControllerConfiguration ToConfigure(Transform transform, KeyCode forward, KeyCode backward, KeyCode left, KeyCode right, KeyCode indexFinger, KeyCode handGrip)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(transform),
                Thumb = SimulateStick.ToConfigure(forward, backward, left, right),
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration WASD(Transform transform, KeyCode indexFinger, KeyCode handGrip)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(transform),
                Thumb = SimulateStick.WASD,
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration WASDFC(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(transform),
                Thumb = SimulateStick.WASD,
                IndexFinger = SimulateTrigger.ToConfigure(KeyCode.F),
                HandGrip = SimulateTrigger.ToConfigure(KeyCode.C),
            };
        }

        public static IHandControllerConfiguration IJKL(Transform transform, KeyCode indexFinger, KeyCode handGrip)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(transform),
                Thumb = SimulateStick.IJKL,
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration IJKLHM(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(transform),
                Thumb = SimulateStick.IJKL,
                IndexFinger = SimulateTrigger.ToConfigure(KeyCode.H),
                HandGrip = SimulateTrigger.ToConfigure(KeyCode.M),
            };
        }

        public static IHandControllerConfiguration ArrowKeys(Transform transform, KeyCode indexFinger, KeyCode handGrip)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(transform),
                Thumb = SimulateStick.ArrowKeys,
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration ToConfigure(Transform origin, Transform transform, KeyCode forward, KeyCode backward, KeyCode left, KeyCode right, KeyCode indexFinger, KeyCode handGrip)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(origin, transform),
                Thumb = SimulateStick.ToConfigure(forward, backward, left, right),
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration WASD(Transform origin, Transform transform, KeyCode indexFinger, KeyCode handGrip)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(origin, transform),
                Thumb = SimulateStick.WASD,
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration WASDFC(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(origin, transform),
                Thumb = SimulateStick.WASD,
                IndexFinger = SimulateTrigger.ToConfigure(KeyCode.F),
                HandGrip = SimulateTrigger.ToConfigure(KeyCode.C),
            };
        }

        public static IHandControllerConfiguration IJKL(Transform origin, Transform transform, KeyCode indexFinger, KeyCode handGrip)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(origin, transform),
                Thumb = SimulateStick.IJKL,
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        public static IHandControllerConfiguration IJKLHM(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(origin, transform),
                Thumb = SimulateStick.IJKL,
                IndexFinger = SimulateTrigger.ToConfigure(KeyCode.H),
                HandGrip = SimulateTrigger.ToConfigure(KeyCode.M),
            };
        }

        public static IHandControllerConfiguration ArrowKeys(Transform origin, Transform transform, KeyCode indexFinger, KeyCode handGrip)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new SimulateHandController()
            {
                Pose = SimulatePoseTracker.ToConfigure(origin, transform),
                Thumb = SimulateStick.ArrowKeys,
                IndexFinger = SimulateTrigger.ToConfigure(indexFinger),
                HandGrip = SimulateTrigger.ToConfigure(handGrip),
            };
        }

        /// <inheritdoc/>
        public IPoseTrackerConfiguration Pose { get; private set; }

        /// <inheritdoc/>
        public IStickConfiguration Thumb { get; private set; }

        /// <inheritdoc/>
        public ITriggerConfiguration IndexFinger { get; private set; }

        /// <inheritdoc/>
        public ITriggerConfiguration HandGrip { get; private set; }
    }
}
