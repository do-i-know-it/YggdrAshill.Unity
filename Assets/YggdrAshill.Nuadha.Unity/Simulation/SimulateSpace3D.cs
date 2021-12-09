using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulateSpace3D
    {
        #region Position

        /// <summary>
        /// Generates <see cref="Space3D.Position"/> from <see cref="Vector3"/>.
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
        public static IGeneration<Space3D.Position> ToGeneratePosition(Func<Vector3> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new GeneratePosition(generation);
        }
        private sealed class GeneratePosition :
            IGeneration<Space3D.Position>
        {
            private readonly Func<Vector3> generation;

            internal GeneratePosition(Func<Vector3> generation)
            {
                this.generation = generation;
            }

            public Space3D.Position Generate()
            {
                return generation.Invoke().ToPosition();
            }
        }

        /// <summary>
        /// Generates <see cref="Space3D.Position"/> from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector3"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Position"/>.
        /// </returns>
        public static IGeneration<Space3D.Position> ToGeneratePosition(Vector3 signal)
        {
            return ToGeneratePosition(() => signal);
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
        public static IGeneration<Space3D.Position> ToGeneratePosition(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToGeneratePosition(() =>
            {
                return transform.position;
            });
        }

        /// <summary>
        /// Generates <see cref="Space3D.Position"/> from relative position of <see cref="Transform"/>s.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for relative position.
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
        public static IGeneration<Space3D.Position> ToGeneratePosition(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToGeneratePosition(() =>
            {
                return Quaternion.Inverse(origin.rotation) * (transform.position - origin.position);
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Position"/> as <see cref="Vector3"/>.
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
        public static IConsumption<Space3D.Position> ToConsumePosition(Action<Vector3> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new ConsumePosition(consumption);
        }
        private sealed class ConsumePosition :
            IConsumption<Space3D.Position>
        {
            private readonly Action<Vector3> consumption;

            internal ConsumePosition(Action<Vector3> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Space3D.Position signal)
            {
                consumption.Invoke(signal.ToVector());
            }
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
        public static IConsumption<Space3D.Position> ToConsumePosition(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToConsumePosition(signal =>
            {
                transform.position = signal;
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Position"/> as relative position of <see cref="Transform"/>.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for relative position.
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
        public static IConsumption<Space3D.Position> ToConsumePosition(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToConsumePosition(signal =>
            {
                transform.position = origin.position + origin.rotation * signal;
            });
        }

        #endregion

        #region Direction

        /// <summary>
        /// Generates <see cref="Space3D.Direction"/> from <see cref="Vector3"/>.
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
        public static IGeneration<Space3D.Direction> ToGenerateDirection(Func<Vector3> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new GenerateDirection(generation);
        }
        private sealed class GenerateDirection :
            IGeneration<Space3D.Direction>
        {
            private readonly Func<Vector3> generation;

            internal GenerateDirection(Func<Vector3> generation)
            {
                this.generation = generation;
            }

            public Space3D.Direction Generate()
            {
                return generation.Invoke().ToDirection();
            }
        }

        /// <summary>
        /// Generates <see cref="Space3D.Direction"/> from <see cref="Vector3"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector3"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Direction"/>.
        /// </returns>
        public static IGeneration<Space3D.Direction> ToGenerateDirection(Vector3 signal)
        {
            return ToGenerateDirection(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Space3D.Direction"/> from absolute position of <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">
        /// <see cref="Transform"/> for absolute position.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Direction"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Direction> ToGenerateDirection(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToGenerateDirection(() =>
            {
                return transform.rotation * Vector3.forward;
            });
        }

        /// <summary>
        /// Generates <see cref="Space3D.Direction"/> from relative position of <see cref="Transform"/>s.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for relative position.
        /// </param>
        /// <param name="transform">
        /// <see cref="Transform"/> for relative position.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Direction"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="origin"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="transform"/> is null.
        /// </exception>
        public static IGeneration<Space3D.Direction> ToGenerateDirection(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToGenerateDirection(() =>
            {
                return transform.rotation * Quaternion.Inverse(origin.rotation) * Vector3.forward;
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Direction"/> as <see cref="Vector3"/>.
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
        public static IConsumption<Space3D.Direction> ToConsumeDirection(Action<Vector3> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new ConsumeDirection(consumption);
        }
        private sealed class ConsumeDirection :
            IConsumption<Space3D.Direction>
        {
            private readonly Action<Vector3> consumption;

            internal ConsumeDirection(Action<Vector3> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Space3D.Direction signal)
            {
                consumption.Invoke(signal.ToVector());
            }
        }

        #endregion

        #region Rotation

        /// <summary>
        /// Generates <see cref="Space3D.Rotation"/> from <see cref="Quaternion"/>.
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
        public static IGeneration<Space3D.Rotation> ToGenerateRotation(Func<Quaternion> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new GenerateRotation(generation);
        }
        private sealed class GenerateRotation :
            IGeneration<Space3D.Rotation>
        {
            private readonly Func<Quaternion> generation;

            internal GenerateRotation(Func<Quaternion> generation)
            {
                this.generation = generation;
            }

            public Space3D.Rotation Generate()
            {
                return generation.Invoke().ToRotation();
            }
        }

        /// <summary>
        /// Generates <see cref="Space3D.Rotation"/> from <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Quaternion"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Space3D.Rotation"/>.
        /// </returns>
        public static IGeneration<Space3D.Rotation> ToGenerateRotation(Quaternion signal)
        {
            return ToGenerateRotation(() => signal);
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
        public static IGeneration<Space3D.Rotation> ToGenerateRotation(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToGenerateRotation(() =>
            {
                return transform.rotation;
            });
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
        public static IGeneration<Space3D.Rotation> ToGenerateRotation(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToGenerateRotation(() =>
            {
                return transform.rotation * Quaternion.Inverse(origin.rotation);
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Rotation"/> as <see cref="Quaternion"/>.
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
        public static IConsumption<Space3D.Rotation> ToConsumeRotation(Action<Quaternion> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new ConsumeRotation(consumption);
        }
        private sealed class ConsumeRotation :
            IConsumption<Space3D.Rotation>
        {
            private readonly Action<Quaternion> consumption;

            internal ConsumeRotation(Action<Quaternion> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Space3D.Rotation signal)
            {
                consumption.Invoke(signal.ToQuaternion());
            }
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
        public static IConsumption<Space3D.Rotation> ToConsumeRotation(Transform transform)
        {
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToConsumeRotation(signal =>
            {
                transform.rotation = signal;
            });
        }

        /// <summary>
        /// Consumes <see cref="Space3D.Rotation"/> as relative rotation of <see cref="Transform"/>.
        /// </summary>
        /// <param name="origin">
        /// <see cref="Transform"/> for relative rotation.
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
        public static IConsumption<Space3D.Rotation> ToConsumeRotation(Transform origin, Transform transform)
        {
            if (origin == null)
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (transform == null)
            {
                throw new ArgumentNullException(nameof(transform));
            }

            return ToConsumeRotation(signal =>
            {
                transform.rotation = signal * origin.rotation;
            });
        }

        #endregion
    }
}
