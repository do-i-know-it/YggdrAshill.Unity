using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;
using Touch = YggdrAshill.Nuadha.Signals.Touch;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulateTouch
    {
        /// <summary>
        /// Generates <see cref="Touch"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Touch"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Touch> ToGenerate(Func<bool> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new SimulateTouchToGenerate(generation);
        }
        private sealed class SimulateTouchToGenerate :
            IGeneration<Touch>
        {
            private readonly Func<bool> generation;

            internal SimulateTouchToGenerate(Func<bool> generation)
            {
                this.generation = generation;
            }

            public Touch Generate()
            {
                return generation.Invoke().ToTouch();
            }
        }

        /// <summary>
        /// Generates <see cref="Touch"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="bool"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Touch"/>.
        /// </returns>
        public static IGeneration<Touch> ToGenerate(bool signal)
        {
            return ToGenerate(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Touch"/> from <see cref="Push"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Push"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Touch"/>.
        /// </returns>
        public static IGeneration<Touch> ToGenerate(IGeneration<Push> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new TouchFromPush(generation);
        }
        private sealed class TouchFromPush :
            IGeneration<Touch>
        {
            private readonly IGeneration<Push> generation;

            internal TouchFromPush(IGeneration<Push> generation)
            {
                this.generation = generation;
            }

            public Touch Generate()
            {
                return generation.Generate().ToTouch();
            }
        }

        /// <summary>
        /// Generates <see cref="Touch"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Touch"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Touch"/>.
        /// </returns>
        public static IGeneration<Touch> ToGenerate(KeyCode code)
        {
            return ToGenerate(SimulatePush.ToGenerate(code));
        }

        /// <summary>
        /// Generates <see cref="Touch"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Touch"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Touch"/>.
        /// </returns>
        public static IGeneration<Touch> Any(params KeyCode[] codeList)
        {
            return ToGenerate(SimulatePush.Any(codeList));
        }

        /// <summary>
        /// Generates <see cref="Touch"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Touch"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Touch"/>.
        /// </returns>
        public static IGeneration<Touch> All(params KeyCode[] codeList)
        {
            return ToGenerate(SimulatePush.All(codeList));
        }

        /// <summary>
        /// Consumes <see cref="Touch"/> as <see cref="bool"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Touch"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Touch> ToConsume(Action<bool> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new SimulateTouchToConsume(consumption);
        }
        private sealed class SimulateTouchToConsume :
            IConsumption<Touch>
        {
            private readonly Action<bool> consumption;

            internal SimulateTouchToConsume(Action<bool> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Touch signal)
            {
                consumption.Invoke(signal.ToBoolean());
            }
        }
    }
}
