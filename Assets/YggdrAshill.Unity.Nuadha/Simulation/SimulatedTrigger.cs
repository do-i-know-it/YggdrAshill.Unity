using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Unity.Nuadha
{
    /// <summary>
    /// Defines implementations for <see cref="ITriggerConfiguration"/> simulated using <see cref="Input"/>.
    /// </summary>
    public sealed class SimulatedTrigger :
        ITriggerConfiguration
    {
        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using keyboard.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="ITriggerConfiguration"/> created.
        /// </returns>
        public static ITriggerConfiguration Keyboard(KeyCode code)
        {
            return new SimulatedTrigger(Generate.KeyboardTouch(code), Generate.KeyboardPull(code));
        }

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using left button of mouse.
        /// </summary>
        public static ITriggerConfiguration MouseLeftClick { get; } = new SimulatedTrigger(Generate.MouseLeftTouch, Generate.MouseLeftPull);

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using right button of mouse.
        /// </summary>
        public static ITriggerConfiguration MouseRightClick { get; } = new SimulatedTrigger(Generate.MouseRightTouch, Generate.MouseRightPull);

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using middle button of mouse.
        /// </summary>
        public static ITriggerConfiguration MouseMiddleClick { get; } = new SimulatedTrigger(Generate.MouseMiddleTouch, Generate.MouseMiddlePull);

        private SimulatedTrigger(IGeneration<YggdrAshill.Nuadha.Signals.Touch> touch, IGeneration<Pull> pull)
        {
            Touch = touch;

            Pull = pull;
        }

        /// <inheritdoc/>
        public IGeneration<YggdrAshill.Nuadha.Signals.Touch> Touch { get; }

        /// <inheritdoc/>
        public IGeneration<Pull> Pull { get; }
    }
}
