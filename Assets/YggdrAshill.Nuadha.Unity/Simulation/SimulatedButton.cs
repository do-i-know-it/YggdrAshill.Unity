using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IButtonConfiguration"/> simulated using <see cref="Input"/>.
    /// </summary>
    public static class SimulatedButton
    {
        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using keyboard.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IButtonConfiguration"/> created.
        /// </returns>
        public static IButtonConfiguration Keyboard(KeyCode code)
        {
            return new KeyboardButton(code);
        }
        private sealed class KeyboardButton :
            IGeneration<Signals.Touch>,
            IGeneration<Push>,
            IButtonConfiguration
        {
            private readonly KeyCode code;

            internal KeyboardButton(KeyCode code)
            {
                this.code = code;
            }

            Signals.Touch IGeneration<Signals.Touch>.Generate()
            {
                return Input.GetKey(code).ToTouch();
            }

            Push IGeneration<Push>.Generate()
            {
                return Input.GetKey(code).ToPush();
            }

            public IGeneration<Signals.Touch> Touch => this;

            public IGeneration<Push> Push => this;
        }

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using left button of mouse.
        /// </summary>
        public static IButtonConfiguration MouseLeftClick { get; } = new MouseButton(0);

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using right button of mouse.
        /// </summary>
        public static IButtonConfiguration MouseRightClick { get; } = new MouseButton(1);

        /// <summary>
        /// Simulated <see cref="IButtonConfiguration"/> using middle button of mouse.
        /// </summary>
        public static IButtonConfiguration MouseMiddleClick { get; } = new MouseButton(2);

        private sealed class MouseButton :
            IGeneration<Signals.Touch>,
            IGeneration<Push>,
            IButtonConfiguration
        {
            private readonly int button;

            internal MouseButton(int button)
            {
                this.button = button;
            }

            Signals.Touch IGeneration<Signals.Touch>.Generate()
            {
                return Input.GetMouseButton(button).ToTouch();
            }

            Push IGeneration<Push>.Generate()
            {
                return Input.GetMouseButton(button).ToPush();
            }

            public IGeneration<Signals.Touch> Touch => this;

            public IGeneration<Push> Push => this;
        }
    }
}
