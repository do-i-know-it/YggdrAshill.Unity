using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IGeneration{TSignal}"/> for Unity.
    /// </summary>
    public static class GenerateSpace3D
    {
        #region Position

        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Space3D.Position"/> from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Position> Position(Func<Vector3> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToPosition();
            });
        }

        /// <summary>
        /// Generates <see cref="Space3D.Position"/> from absolute position of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute position.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Position> AbsolutePosition(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new GenerateAbsolutePosition(transform);
        }
        private sealed class GenerateAbsolutePosition :
            IGeneration<Space3D.Position>
        {
            private readonly Transform transform;

            internal GenerateAbsolutePosition(Transform transform)
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

            public Space3D.Position Generate()
            {
                return Transform.position.ToPosition();
            }
        }

        /// <summary>
        /// Generates <see cref="Space3D.Position"/> from relative position of <see cref="Transform"/>.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for origin.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative position.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Position> RelativePosition(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new GenerateRelativePosition(origin, transform);
        }
        private sealed class GenerateRelativePosition :
            IGeneration<Space3D.Position>
        {
            private readonly Transform origin;

            private readonly Transform transform;

            internal GenerateRelativePosition(Transform origin, Transform transform)
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

            public Space3D.Position Generate()
            {
                var signal = Quaternion.Inverse(Origin.rotation) * (Transform.position - Origin.position);

                return signal.ToPosition();
            }
        }

        #endregion

        #region Direction

        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Space3D.Direction"/> from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Direction"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Direction> Direction(Func<Vector3> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToDirection();
            });
        }

        /// <summary>
        /// Generates <see cref="Space3D.Direction"/> from absolute direction of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute direction.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Direction"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Direction> AbsoluteDirection(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new GenerateAbsoluteDirection(transform);
        }
        private sealed class GenerateAbsoluteDirection :
            IGeneration<Space3D.Direction>
        {
            private readonly Transform transform;

            internal GenerateAbsoluteDirection(Transform transform)
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

            public Space3D.Direction Generate()
            {
                var rotation = Transform.rotation;

                var signal = rotation * Vector3.forward;

                return signal.ToDirection();
            }
        }

        /// <summary>
        /// Generates <see cref="Space3D.Direction"/> from relative direction of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative direction.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Direction"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Direction> RelativeDirection(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new GenerateRelativeDirection(origin, transform);
        }
        private sealed class GenerateRelativeDirection :
            IGeneration<Space3D.Direction>
        {
            private readonly Transform origin;

            private readonly Transform transform;

            internal GenerateRelativeDirection(Transform origin, Transform transform)
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

            public Space3D.Direction Generate()
            {
                var rotation = Transform.rotation * Quaternion.Inverse(Origin.rotation);

                var signal = rotation * Vector3.forward;

                return signal.ToDirection();
            }
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Space3D.Rotation"/> from <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Quaternion"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Rotation> Rotation(Func<Quaternion> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToRotation();
            });
        }

        /// <summary>
        /// Generates <see cref="Space3D.Rotation"/> from absolute rotation of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute rotation.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Rotation> AbsoluteRotation(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new GenerateAbsoluteRotation(transform);
        }
        private sealed class GenerateAbsoluteRotation :
            IGeneration<Space3D.Rotation>
        {
            private readonly Transform transform;

            internal GenerateAbsoluteRotation(Transform transform)
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

            public Space3D.Rotation Generate()
            {
                return Transform.rotation.ToRotation();
            }
        }

        /// <summary>
        /// Generates <see cref="Space3D.Rotation"/> from relative rotation of <see cref="Transform"/>.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for origin.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative rotation.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Rotation> RelativeRotation(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return new GenerateRelativeRotation(origin, transform);
        }
        private sealed class GenerateRelativeRotation :
            IGeneration<Space3D.Rotation>
        {
            private readonly Transform origin;

            private readonly Transform transform;

            internal GenerateRelativeRotation(Transform origin, Transform transform)
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

            public Space3D.Rotation Generate()
            {
                var signal = Transform.rotation * Quaternion.Inverse(Origin.rotation);

                return signal.ToRotation();
            }
        }

        #endregion
    }
}
