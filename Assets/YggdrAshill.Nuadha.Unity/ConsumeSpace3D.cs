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
    }
}
