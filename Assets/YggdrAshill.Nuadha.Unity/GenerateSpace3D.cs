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
    }
}
