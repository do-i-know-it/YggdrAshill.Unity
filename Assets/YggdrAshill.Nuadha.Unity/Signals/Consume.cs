using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IConsumption{TSignal}"/> for Unity.
    /// </summary>
    public static class Consume
    {
        /// <summary>
        /// Executes <see cref="Action{T}"/> to consume <see cref="Signals.Tilt"/> as <see cref="Vector2"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Signals.Tilt"/>.
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

            return Nuadha.Consume.Signal<Tilt>(signal =>
            {
                consumption.Invoke(signal.ToVector());
            });
        }
    }
}
