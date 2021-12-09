using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using UnityEngine;
using Touch = YggdrAshill.Nuadha.Signals.Touch;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulateTrigger :
        ITriggerConfiguration
    {
        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="ITriggerConfiguration"/> created.
        /// </returns>
        public static ITriggerConfiguration ToConfigure(KeyCode code)
        {
            var push = SimulatePush.ToGenerate(code);

            return new SimulateTrigger(push);
        }

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Signals.Touch"/> and <see cref="Signals.Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="ITriggerConfiguration"/> created.
        /// </returns>
        public static ITriggerConfiguration Any(params KeyCode[] codeList)
        {
            var push = SimulatePush.Any(codeList);

            return new SimulateTrigger(push);
        }

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Signals.Touch"/> and <see cref="Signals.Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="ITriggerConfiguration"/> created.
        /// </returns>
        public static ITriggerConfiguration All(params KeyCode[] codeList)
        {
            var push = SimulatePush.All(codeList);

            return new SimulateTrigger(push);
        }

        private SimulateTrigger(IGeneration<Push> push)
        {
            Touch = SimulateTouch.ToGenerate(push);

            Pull = SimulatePull.ToGenerate(push);
        }

        /// <inheritdoc/>
        public IGeneration<Touch> Touch { get; }

        /// <inheritdoc/>
        public IGeneration<Pull> Pull { get; }
    }
}
