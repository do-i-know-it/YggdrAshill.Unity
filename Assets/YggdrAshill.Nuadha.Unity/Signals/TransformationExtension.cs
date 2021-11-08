using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Transformation;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines extensions for Transformation in Unity.
    /// </summary>
    public static class TransformationExtension
    {
        /// <summary>
        /// Calibrates <see cref="Space3D.Position"/> using <see cref="Vector3"/>.
        /// </summary>
        /// <param name="production">
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Position"/>.
        /// </param>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Vector3"/>.
        /// </param>
        /// <returns>
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="production"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IProduction<Space3D.Position> Calibrate(this IProduction<Space3D.Position> production, Func<Vector3> generation)
        {
            if (production == null)
            {
                throw new ArgumentNullException(nameof(production));
            }
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return production.Convert(Space3DPositionTo.Calibrate(GenerateSpace3D.Position(generation)));
        }

        /// <summary>
        /// Calibrates <see cref="Space3D.Position"/> using <see cref="Vector3"/>.
        /// </summary>
        /// <param name="production">
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Position"/>.
        /// </param>
        /// <param name="offset">
        /// <see cref="Vector3"/> to calibrate.
        /// </param>
        /// <returns>
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Position"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="production"/> is null.
        /// </exception>
        public static IProduction<Space3D.Position> Calibrate(this IProduction<Space3D.Position> production, Vector3 offset)
        {
            if (production == null)
            {
                throw new ArgumentNullException(nameof(production));
            }

            return production.Convert(Space3DPositionTo.Calibrate(GenerateSpace3D.Position(offset)));
        }

        /// <summary>
        /// Calibrates <see cref="Space3D.Rotation"/> using <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="production">
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Rotation"/>.
        /// </param>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Quaternion"/>.
        /// </param>
        /// <returns>
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="production"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IProduction<Space3D.Rotation> Calibrate(this IProduction<Space3D.Rotation> production, Func<Quaternion> generation)
        {
            if (production == null)
            {
                throw new ArgumentNullException(nameof(production));
            }
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return production.Convert(Space3DRotationTo.Calibrate(GenerateSpace3D.Rotation(generation)));
        }

        /// <summary>
        /// Calibrates <see cref="Space3D.Rotation"/> using <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="production">
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Rotation"/>.
        /// </param>
        /// <param name="offset">
        /// <see cref="Quaternion"/> to calibrate.
        /// </param>
        /// <returns>
        /// <see cref="IProduction{TSignal}"/> to send <see cref="Space3D.Rotation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="production"/> is null.
        /// </exception>
        public static IProduction<Space3D.Rotation> Calibrate(this IProduction<Space3D.Rotation> production, Quaternion offset)
        {
            if (production == null)
            {
                throw new ArgumentNullException(nameof(production));
            }

            return production.Convert(Space3DRotationTo.Calibrate(GenerateSpace3D.Rotation(offset)));
        }
    }
}
