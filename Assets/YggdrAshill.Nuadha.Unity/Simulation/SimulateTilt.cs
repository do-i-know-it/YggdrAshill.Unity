using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulateTilt
    {
        /// <summary>
        /// Generates <see cref="Tilt"/> from <see cref="Vector2"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Tilt"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Tilt> ToGenerate(Func<Vector2> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new SimulateTiltToGenerate(generation);
        }
        private sealed class SimulateTiltToGenerate :
            IGeneration<Tilt>
        {
            private readonly Func<Vector2> generation;

            internal SimulateTiltToGenerate(Func<Vector2> generation)
            {
                this.generation = generation;
            }

            public Tilt Generate()
            {
                return generation.Invoke().ToTilt();
            }
        }

        /// <summary>
        /// Generates <see cref="Tilt"/> from <see cref="Vector2"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector2"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Tilt"/>.
        /// </returns>
        public static IGeneration<Tilt> ToGenerate(Vector2 signal)
        {
            return ToGenerate(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Tilt"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="forward">
        /// <see cref="KeyCode"/> to generate <see cref="Tilt"/>.
        /// </param>
        /// <param name="backward">
        /// <see cref="KeyCode"/> to generate <see cref="Tilt"/>.
        /// </param>
        /// <param name="left">
        /// <see cref="KeyCode"/> to generate <see cref="Tilt"/>.
        /// </param>
        /// <param name="right">
        /// <see cref="KeyCode"/> to generate <see cref="Tilt"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Tilt"/>.
        /// </returns>
        public static IGeneration<Tilt> ToGenerate(KeyCode forward, KeyCode backward, KeyCode left, KeyCode right)
        {
            return ToGenerate(() =>
            {
                var horizontal = 0.0f;
                horizontal += Input.GetKey(right) ? 1f : 0f;
                horizontal += Input.GetKey(left) ? -1f : 0f;

                var vertical = 0.0f;
                vertical += Input.GetKey(forward) ? 1f : 0f;
                vertical += Input.GetKey(backward) ? -1f : 0f;

                return new Vector2(horizontal, vertical);
            });
        }

        /// <summary>
        /// Consumes <see cref="Tilt"/> as <see cref="Vector2"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Tilt"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Tilt> ToConsume(Action<Vector2> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new SimulateTiltToConsume(consumption);
        }
        private sealed class SimulateTiltToConsume :
            IConsumption<Tilt>
        {
            private readonly Action<Vector2> consumption;

            internal SimulateTiltToConsume(Action<Vector2> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Tilt signal)
            {
                consumption.Invoke(signal.ToVector());
            }
        }
    }
}
