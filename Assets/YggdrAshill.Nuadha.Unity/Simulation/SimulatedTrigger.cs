using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="ITriggerConfiguration"/> simulated using <see cref="Input"/>.
    /// </summary>
    public static class SimulatedTrigger
    {
        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using keyboard.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="ITriggerConfiguration"/> created.
        /// </returns>
        public static ITriggerConfiguration Keyboard(KeyCode code)
        {
            return new KeyboardTrigger(code);
        }
        private sealed class KeyboardTrigger :
            IGeneration<Signals.Touch>,
            IGeneration<Pull>,
            ITriggerConfiguration
        {
            private readonly KeyCode code;

            internal KeyboardTrigger(KeyCode code)
            {
                this.code = code;
            }

            Signals.Touch IGeneration<Signals.Touch>.Generate()
            {
                return Input.GetKey(code).ToTouch();
            }

            Pull IGeneration<Pull>.Generate()
            {
                var signal = Input.GetKey(code) ? 1f : 0f;

                return signal.ToPull();
            }

            public IGeneration<Signals.Touch> Touch => this;

            public IGeneration<Pull> Pull => this;
        }

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using left button of mouse.
        /// </summary>
        public static ITriggerConfiguration MouseLeftClick { get; } = new MouseTrigger(0);

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using right button of mouse.
        /// </summary>
        public static ITriggerConfiguration MouseRightClick { get; } = new MouseTrigger(1);

        /// <summary>
        /// Simulated <see cref="ITriggerConfiguration"/> using middle button of mouse.
        /// </summary>
        public static ITriggerConfiguration MouseMiddleClick { get; } = new MouseTrigger(2);

        private sealed class MouseTrigger :
            IGeneration<Signals.Touch>,
            IGeneration<Pull>,
            ITriggerConfiguration
        {
            private readonly int button;

            internal MouseTrigger(int button)
            {
                this.button = button;
            }

            Signals.Touch IGeneration<Signals.Touch>.Generate()
            {
                return Input.GetMouseButton(button).ToTouch();
            }

            Pull IGeneration<Pull>.Generate()
            {
                var signal = Input.GetMouseButton(button) ? 1f : 0f;

                return signal.ToPull();
            }

            public IGeneration<Signals.Touch> Touch => this;

            public IGeneration<Pull> Pull => this;
        }
    }
}
