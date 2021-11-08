using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IGeneration{TSignal}"/> for Unity.
    /// </summary>
    public static class Generate
    {
        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Signals.Tilt"/> from <see cref="Vector2"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Tilt"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Tilt> Tilt(Func<Vector2> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToTilt();
            });
        }
    }
}
