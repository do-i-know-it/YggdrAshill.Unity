using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    public static class SimulatePush
    {
        /// <summary>
        /// Generates <see cref="Push"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Push"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Push> ToGenerate(Func<bool> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return new SimulatePushToGenerate(generation);
        }
        private sealed class SimulatePushToGenerate :
            IGeneration<Push>
        {
            private readonly Func<bool> generation;

            internal SimulatePushToGenerate(Func<bool> generation)
            {
                this.generation = generation;
            }

            public Push Generate()
            {
                return generation.Invoke().ToPush();
            }
        }

        /// <summary>
        /// Generates <see cref="Push"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="bool"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Push"/>.
        /// </returns>
        public static IGeneration<Push> ToGenerate(bool signal)
        {
            return ToGenerate(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Push"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Push"/>.
        /// </returns>
        public static IGeneration<Push> ToGenerate(KeyCode code)
        {
            return ToGenerate(() => Input.GetKey(code));
        }

        /// <summary>
        /// Generates <see cref="Push"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Push"/>.
        /// </returns>
        public static IGeneration<Push> Any(params KeyCode[] codeList)
        {
            return ToGenerate(() =>
            {
                foreach (var code in codeList)
                {
                    if (Input.GetKey(code))
                    {
                        return true;
                    }
                }

                return false;
            });
        }

        /// <summary>
        /// Generates <see cref="Push"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Push"/>.
        /// </returns>
        public static IGeneration<Push> All(params KeyCode[] codeList)
        {
            return ToGenerate(() =>
            {
                foreach (var code in codeList)
                {
                    if (!Input.GetKey(code))
                    {
                        return false;
                    }
                }

                return true;
            });
        }

        /// <summary>
        /// Consumes <see cref="Push"/> as <see cref="bool"/>.
        /// </summary>
        /// <param name="consumption">
        /// <see cref="Action{T}"/> to consume <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IConsumption{TSignal}"/> to consume <see cref="Push"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="consumption"/> is null.
        /// </exception>
        public static IConsumption<Push> ToConsume(Action<bool> consumption)
        {
            if (consumption == null)
            {
                throw new ArgumentNullException(nameof(consumption));
            }

            return new SimulatePushToConsume(consumption);
        }
        private sealed class SimulatePushToConsume :
            IConsumption<Push>
        {
            private readonly Action<bool> consumption;

            internal SimulatePushToConsume(Action<bool> consumption)
            {
                this.consumption = consumption;
            }

            public void Consume(Push signal)
            {
                consumption.Invoke(signal.ToBoolean());
            }
        }
    }
}
