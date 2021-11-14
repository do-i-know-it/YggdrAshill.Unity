using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Unity.Nuadha
{
    /// <summary>
    /// Defines implementations of <see cref="IConsumption{TSignal}"/> for Unity.
    /// </summary>
    public static class Consume
    {
        #region Touch

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Touch"/> as <see cref="bool"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Touch"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<YggdrAshill.Nuadha.Signals.Touch> Touch(Action<bool> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return YggdrAshill.Nuadha.Consume.Signal<YggdrAshill.Nuadha.Signals.Touch>(signal =>
            {
                consumption.Invoke(signal.ToBoolean());
            });
        }

        #endregion

        #region Push

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Push"/> as <see cref="bool"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Push"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Push> Push(Action<bool> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return YggdrAshill.Nuadha.Consume.Signal<Push>(signal =>
            {
                consumption.Invoke(signal.ToBoolean());
            });
        }

        #endregion

        #region Pull

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Pull"/> as <see cref="float"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="float"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Pull"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Pull> Pull(Action<float> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return YggdrAshill.Nuadha.Consume.Signal<Pull>(signal =>
            {
                consumption.Invoke(signal.ToSingle());
            });
        }

        #endregion

        #region Tilt

        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Tilt"/> as <see cref="Vector2"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="YggdrAshill.Nuadha.Signals.Tilt"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Tilt> Tilt(Action<Vector2> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return YggdrAshill.Nuadha.Consume.Signal<Tilt>(signal =>
            {
                consumption.Invoke(signal.ToVector());
            });
        }

        #endregion
    }
}
