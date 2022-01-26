using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulatePull
    {
        /// <summary>
        /// Generates <see cref="Pull"/> from <see cref="float"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="float"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Pull> ToGenerate(Func<float> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToPull();
            });
        }
        private sealed class SimulatePullToGenerate :
            IGeneration<Pull>
        {
            private readonly Func<float> generation;

            internal SimulatePullToGenerate(Func<float> generation)
            {
                this.generation = generation;
            }

            public Pull Generate()
            {
                return generation.Invoke().ToPull();
            }
        }

        /// <summary>
        /// Generates <see cref="Pull"/> from <see cref="float"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="float"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        public static IGeneration<Pull> ToGenerate(float signal)
        {
            return ToGenerate(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Pull"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Pull> ToGenerate(Func<bool> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return ToGenerate(() => generation.Invoke() ? Pull.Maximum : Pull.Minimum);
        }

        /// <summary>
        /// Generates <see cref="Pull"/> from <see cref="float"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="float"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        public static IGeneration<Pull> ToGenerate(bool signal)
        {
            return ToGenerate(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Pull"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Pull> ToGenerate(IGeneration<Push> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new PullFromPush(generation);
        }
        private sealed class PullFromPush :
            IGeneration<Pull>
        {
            private readonly IGeneration<Push> generation;

            internal PullFromPush(IGeneration<Push> generation)
            {
                this.generation = generation;
            }

            public Pull Generate()
            {
                return generation.Generate().ToPull();
            }
        }

        /// <summary>
        /// Generates <see cref="Pull"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        public static IGeneration<Pull> ToGenerate(KeyCode code)
        {
            return ToGenerate(SimulatePush.ToGenerate(code));
        }

        /// <summary>
        /// Generates <see cref="Pull"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        public static IGeneration<Pull> Any(params KeyCode[] codeList)
        {
            return ToGenerate(SimulatePush.Any(codeList));
        }

        /// <summary>
        /// Generates <see cref="Pull"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Pull"/>.
        /// </returns>
        public static IGeneration<Pull> All(params KeyCode[] codeList)
        {
            return ToGenerate(SimulatePush.All(codeList));
        }

        /// <summary>
        /// Consumes <see cref="Pull"/> as <see cref="float"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="float"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Pull"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Pull> ToConsume(Action<float> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new SimulatePullToConsume(consumption);
        }
        private sealed class SimulatePullToConsume :
            IConsumption<Pull>
        {
            private readonly Action<float> consumption;

            internal SimulatePullToConsume(Action<float> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Pull signal)
            {
                consumption.Invoke(signal.ToSingle());
            }
        }
    }
}
