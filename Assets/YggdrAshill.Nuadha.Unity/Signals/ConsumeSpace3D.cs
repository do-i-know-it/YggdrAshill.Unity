using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IConsumption{TSignal}"/> for Unity.
    /// </summary>
    public static class ConsumeSpace3D
    {
        #region Position

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="Space3D.Position"/> as <see cref="Vector3"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Position> Position(Action<Vector3> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return Nuadha.Consume.Signal<Space3D.Position>(signal =>
            {
                consumption.Invoke(signal.ToVector());
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Position"/> as absolute position of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute position.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Position> AbsolutePosition(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new ConsumeAbsolutePosition(transform);
        }
        private sealed class ConsumeAbsolutePosition :
            IConsumption<Space3D.Position>
        {
            private readonly Transform transform;

            internal ConsumeAbsolutePosition(Transform transform)
            {
                this.transform = transform;
            }

            private Transform Transform
            {
                get
                {
                    if (transform == null)
                    {
                        throw new InvalidOperationException($"{nameof(transform)} is null.");
                    }

                    return transform;
                }
            }

            public void Consume(Space3D.Position signal)
            {
                Transform.position = signal.ToVector();
            }
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Position"/> as relative position of <see cref="Transform"/>.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for origin.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative position.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Position> RelativePosition(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new ConsumeRelativePosition(origin, transform);
        }
        private sealed class ConsumeRelativePosition :
            IConsumption<Space3D.Position>
        {
            private readonly Transform origin;

            private readonly Transform transform;

            internal ConsumeRelativePosition(Transform origin, Transform transform)
            {
                this.origin = origin;

                this.transform = transform;
            }

            private Transform Origin
            {
                get
                {
                    if (origin == null)
                    {
                        throw new InvalidOperationException($"{nameof(origin)} is null.");
                    }

                    return origin;
                }
            }

            private Transform Transform
            {
                get
                {
                    if (transform == null)
                    {
                        throw new InvalidOperationException($"{nameof(transform)} is null.");
                    }

                    return transform;
                }
            }

            public void Consume(Space3D.Position signal)
            {
                Transform.position = Origin.position + Origin.rotation * signal.ToVector();
            }
        }

        #endregion

        #region Direction

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="Space3D.Direction"/> as <see cref="Vector3"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Direction"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Direction> Direction(Action<Vector3> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return Nuadha.Consume.Signal<Space3D.Direction>(signal =>
            {
                consumption.Invoke(signal.ToVector());
            });
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="Space3D.Rotation"/> as <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="Quaternion"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Rotation> Rotation(Action<Quaternion> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return Nuadha.Consume.Signal<Space3D.Rotation>(signal =>
            {
                consumption.Invoke(signal.ToQuaternion());
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Rotation"/> as absolute rotation of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute rotation.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Rotation> AbsoluteRotation(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new ConsumeAbsoluteRotation(transform);
        }
        private sealed class ConsumeAbsoluteRotation :
            IConsumption<Space3D.Rotation>
        {
            private readonly Transform transform;

            internal ConsumeAbsoluteRotation(Transform transform)
            {
                this.transform = transform;
            }

            private Transform Transform
            {
                get
                {
                    if (transform == null)
                    {
                        throw new InvalidOperationException($"{nameof(transform)} is null.");
                    }

                    return transform;
                }
            }

            public void Consume(Space3D.Rotation signal)
            {
                Transform.rotation = signal.ToQuaternion();
            }
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Rotation"/> as relative rotation of <see cref="Transform"/>.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for origin.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative rotation.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IConsumption<Space3D.Rotation> RelativeRotation(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new ConsumeRelativeRotation(origin, transform);
        }
        private sealed class ConsumeRelativeRotation :
            IConsumption<Space3D.Rotation>
        {
            private readonly Transform origin;

            private readonly Transform transform;

            internal ConsumeRelativeRotation(Transform origin, Transform transform)
            {
                this.origin = origin;

                this.transform = transform;
            }

            private Transform Origin
            {
                get
                {
                    if (origin == null)
                    {
                        throw new InvalidOperationException($"{nameof(origin)} is null.");
                    }

                    return origin;
                }
            }

            private Transform Transform
            {
                get
                {
                    if (transform == null)
                    {
                        throw new InvalidOperationException($"{nameof(transform)} is null.");
                    }

                    return transform;
                }
            }

            public void Consume(Space3D.Rotation signal)
            {
                Transform.rotation = signal.ToQuaternion() * Origin.rotation;
            }
        }

        #endregion
    }
}
