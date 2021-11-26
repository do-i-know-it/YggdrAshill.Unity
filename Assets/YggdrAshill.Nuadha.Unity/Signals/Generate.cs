using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using System;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations of <see cref="IGeneration{TSignal}"/> for Unity.
    /// </summary>
    public static class Generate
    {
        #region Touch

        /// <summary>
        /// Generates <see cref="Signals.Touch"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Touch"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Signals.Touch> Touch(Func<bool> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToTouch();
            });
        }

        /// <summary>
        /// Generates <see cref="Signals.Touch"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="bool"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Touch"/>.
        /// </returns>
        public static IGeneration<Signals.Touch> Touch(bool signal)
        {
            return Touch(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Signals.Touch"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Touch"/>.
        /// </returns>
        public static IGeneration<Signals.Touch> KeyboardTouch(KeyCode code)
        {
            return new GenerateKeyboardTouch(code);
        }
        private sealed class GenerateKeyboardTouch :
            IGeneration<Signals.Touch>
        {
            private readonly KeyCode code;

            internal GenerateKeyboardTouch(KeyCode code)
            {
                this.code = code;
            }

            public Signals.Touch Generate()
            {
                return Input.GetKey(code).ToTouch();
            }
        }

        /// <summary>
        /// Generates <see cref="Signals.Touch"/> using left button of mouse.
        /// </summary>
        public static IGeneration<Signals.Touch> MouseLeftTouch { get; } = new GenerateMouseTouch(0);

        /// <summary>
        /// Generates <see cref=".YggdrAshill.NuadhaSignals.Touch"/> using right button of mouse.
        /// </summary>
        public static IGeneration<Signals.Touch> MouseRightTouch { get; } = new GenerateMouseTouch(1);

        /// <summary>
        /// Generates <see cref="Signals.Touch"/> using middle button of mouse.
        /// </summary>
        public static IGeneration<Signals.Touch> MouseMiddleTouch { get; } = new GenerateMouseTouch(2);

        private sealed class GenerateMouseTouch :
            IGeneration<Signals.Touch>
        {
            private readonly int button;

            internal GenerateMouseTouch(int button)
            {
                this.button = button;
            }

            public Signals.Touch Generate()
            {
                return Input.GetMouseButton(button).ToTouch();
            }
        }

        #endregion

        #region Push

        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Signals.Push"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="bool"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Push"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Push> Push(Func<bool> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToPush();
            });
        }

        /// <summary>
        /// Generates <see cref="Signals.Push"/> from <see cref="bool"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="bool"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Push"/>.
        /// </returns>
        public static IGeneration<Push> Push(bool signal)
        {
            return Push(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Signals.Push"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Push"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Push"/>.
        /// </returns>
        public static IGeneration<Push> KeyboardPush(KeyCode code)
        {
            return new GenerateKeyboardPush(code);
        }
        private sealed class GenerateKeyboardPush :
            IGeneration<Push>
        {
            private readonly KeyCode code;

            internal GenerateKeyboardPush(KeyCode code)
            {
                this.code = code;
            }

            public Push Generate()
            {
                return Input.GetKey(code).ToPush();
            }
        }

        /// <summary>
        /// Generates <see cref="Signals.Push"/> using left button of mouse.
        /// </summary>
        public static IGeneration<Push> MouseLeftPush { get; } = new GenerateMousePush(0);

        /// <summary>
        /// Generates <see cref="Signals.Push"/> using right button of mouse.
        /// </summary>
        public static IGeneration<Push> MouseRightPush { get; } = new GenerateMousePush(1);

        /// <summary>
        /// Generates <see cref="Signals.Push"/> using middle button of mouse.
        /// </summary>
        public static IGeneration<Push> MouseMiddlePush { get; } = new GenerateMousePush(2);

        private sealed class GenerateMousePush :
            IGeneration<Push>
        {
            private readonly int button;

            internal GenerateMousePush(int button)
            {
                this.button = button;
            }

            public Push Generate()
            {
                return Input.GetMouseButton(button).ToPush();
            }
        }

        #endregion

        #region Pull

        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Signals.Pull"/> from <see cref="float"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="float"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Pull"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Pull> Pull(Func<float> generation)
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

        /// <summary>
        /// Generates <see cref="Signals.Pull"/> from <see cref="float"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="float"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Pull"/>.
        /// </returns>
        public static IGeneration<Pull> Pull(float signal)
        {
            return Pull(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Signals.Pull"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="code">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Pull"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Pull"/>.
        /// </returns>
        public static IGeneration<Pull> KeyboardPull(KeyCode code)
        {
            return new GenerateKeyboardPull(code);
        }
        private sealed class GenerateKeyboardPull :
            IGeneration<Pull>
        {
            private readonly KeyCode code;

            internal GenerateKeyboardPull(KeyCode code)
            {
                this.code = code;
            }

            public Pull Generate()
            {
                var signal = Input.GetKey(code) ? 1f : 0f;

                return signal.ToPull();
            }
        }

        /// <summary>
        /// Generates <see cref="Signals.Pull"/> using left button of mouse.
        /// </summary>
        public static IGeneration<Pull> MouseLeftPull { get; } = new GenerateMousePull(0);

        /// <summary>
        /// Generates <see cref="Signals.Pull"/> using right button of mouse.
        /// </summary>
        public static IGeneration<Pull> MouseRightPull { get; } = new GenerateMousePull(1);

        /// <summary>
        /// Generates <see cref="Signals.Pull"/> using middle button of mouse.
        /// </summary>
        public static IGeneration<Pull> MouseMiddlePull { get; } = new GenerateMousePull(2);

        private sealed class GenerateMousePull :
            IGeneration<Pull>
        {
            private readonly int button;

            internal GenerateMousePull(int button)
            {
                this.button = button;
            }

            public Pull Generate()
            {
                var signal = Input.GetMouseButton(button) ? 1f : 0f;

                return signal.ToPull();
            }
        }

        #endregion

        #region Tilt

        /// <summary>
        /// Executes <see cref="Func{TResult}"/> to generate <see cref="Signals.Tilt"/> from <see cref="Vector2"/>.
        /// </summary>
        /// <param name="generation">
        /// <see cref="Func{TResult}"/> to generate <see cref="Vector2"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Tilt"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="generation"/> is null.
        /// </exception>
        public static IGeneration<Tilt> Tilt(Func<Vector2> generation)
        {
            if (generation == null)
            {
                throw new ArgumentNullException(nameof(generation));
            }

            return Nuadha.Generate.Signal(() =>
            {
                return generation.Invoke().ToTilt();
            });
        }

        /// <summary>
        /// Generates <see cref="Signals.Tilt"/> from <see cref="Vector2"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector2"/> to generate.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Tilt"/>.
        /// </returns>
        public static IGeneration<Tilt> Tilt(Vector2 signal)
        {
            return Tilt(() => signal);
        }

        /// <summary>
        /// Generates <see cref="Signals.Tilt"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="forward">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Tilt"/>.
        /// </param>
        /// <param name="backward">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Tilt"/>.
        /// </param>
        /// <param name="left">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Tilt"/>.
        /// </param>
        /// <param name="right">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Tilt"/>.
        /// </param>
        /// <returns>
        /// <see cref="IGeneration{TSignal}"/> to generate <see cref="Signals.Tilt"/>.
        /// </returns>
        public static IGeneration<Tilt> KeyboardTilt(KeyCode forward, KeyCode backward, KeyCode left, KeyCode right)
        {
            return new GenerateKeyboardTilt(forward, backward, left, right);
        }
        private sealed class GenerateKeyboardTilt :
            IGeneration<Tilt>
        {
            private readonly KeyCode forward;
            private readonly KeyCode backward;
            private readonly KeyCode left;
            private readonly KeyCode right;

            internal GenerateKeyboardTilt(KeyCode forward, KeyCode backward, KeyCode left, KeyCode right)
            {
                this.forward = forward;
                this.backward = backward;
                this.left = left;
                this.right = right;
            }

            public Tilt Generate()
            {
                var right = Input.GetKey(this.right) ? 1f : 0f;
                var left = Input.GetKey(this.left) ? -1f : 0f;

                var horizontal = right + left;

                var forward = Input.GetKey(this.forward) ? 1f : 0f;
                var backward = Input.GetKey(this.backward) ? -1f : 0f;

                var vertical = forward + backward;

                return new Vector2(horizontal, vertical).ToTilt();
            }
        }

        /// <summary>
        /// Generates <see cref="Signals.Tilt"/> using WASD.
        /// </summary>
        public static IGeneration<Tilt> WASD { get; } = KeyboardTilt(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Generates <see cref="Signals.Tilt"/> using IJKL.
        /// </summary>
        public static IGeneration<Tilt> IJKL { get; } = KeyboardTilt(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Generates <see cref="Signals.Tilt"/> using ArrowKeys.
        /// </summary>
        public static IGeneration<Tilt> ArrowKeys { get; } = KeyboardTilt(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

        #endregion
    }
}
