using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IButtonConfiguration"/> simulated using <see cref="Input"/>.
    /// </summary>
    public sealed class SimulatedButton :
        IButtonConfiguration
    {
        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using keyboard.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IButtonConfiguration"/> created.
        /// </returns>
        public static IButtonConfiguration Keyboard(KeyCode code)
        {
            return new SimulatedButton(Generate.KeyboardTouch(code), Generate.KeyboardPush(code));
        }

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using left button of mouse.
        /// </summary>
        public static IButtonConfiguration MouseLeftClick { get; } = new SimulatedButton(Generate.MouseLeftTouch, Generate.MouseLeftPush);

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using right button of mouse.
        /// </summary>
        public static IButtonConfiguration MouseRightClick { get; } = new SimulatedButton(Generate.MouseRightTouch, Generate.MouseRightPush);

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using middle button of mouse.
        /// </summary>
        public static IButtonConfiguration MouseMiddleClick { get; } = new SimulatedButton(Generate.MouseMiddleTouch, Generate.MouseMiddlePush);

        private SimulatedButton(IGeneration<Signals.Touch> touch, IGeneration<Push> push)
        {
            Touch = touch;

            Push = push;
        }

        /// <inheritdoc/>
        public IGeneration<Signals.Touch> Touch { get; }

        /// <inheritdoc/>
        public IGeneration<Push> Push { get; }
    }
}
