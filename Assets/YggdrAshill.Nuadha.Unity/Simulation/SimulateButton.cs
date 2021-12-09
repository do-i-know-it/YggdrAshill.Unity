using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using UnityEngine;
using Touch = YggdrAshill.Nuadha.Signals.Touch;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulateButton :
        IButtonConfiguration
    {
        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IButtonConfiguration"/> created.
        /// </returns>
        public static IButtonConfiguration Keyboard(KeyCode code)
        {
            var push = SimulatePush.ToGenerate(code);

            return new SimulateButton(push);
        }

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Signals.Touch"/> and <see cref="Signals.Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IButtonConfiguration"/> created.
        /// </returns>
        public static IButtonConfiguration Any(params KeyCode[] codeList)
        {
            var push = SimulatePush.Any(codeList);

            return new SimulateButton(push);
        }

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="codeList">
        /// <see cref="KeyCode"/>s to generate <see cref="Signals.Touch"/> and <see cref="Signals.Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IButtonConfiguration"/> created.
        /// </returns>
        public static IButtonConfiguration All(params KeyCode[] codeList)
        {
            var push = SimulatePush.All(codeList);

            return new SimulateButton(push);
        }

        private SimulateButton(IGeneration<Push> push)
        {
            Push = push;

            Touch = SimulateTouch.ToGenerate(Push);
        }

        /// <inheritdoc/>
        public IGeneration<Touch> Touch { get; }

        /// <inheritdoc/>
        public IGeneration<Push> Push { get; }
    }
}
