using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IStickConfiguration"/> simulated using <see cref="Input"/>.
    /// </summary>
    public sealed class SimulatedStick :
        IStickConfiguration
    {
        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using keyboard.
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
        /// <see cref="IStickConfiguration"/> created.
        /// </returns>
        public static IStickConfiguration Keyboard(KeyCode forward, KeyCode backward, KeyCode left, KeyCode right)
        {
            var touch = Generate.Touch(() =>
            {
                return Input.GetKey(forward)
                || Input.GetKey(backward)
                || Input.GetKey(left)
                || Input.GetKey(right);
            });

            var tilt = Generate.KeyboardTilt(forward, backward, left, right);

            return new SimulatedStick(touch, tilt);
        }

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using WASD.
        /// </summary>
        public static IStickConfiguration WASD { get; } = Keyboard(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using IJKL.
        /// </summary>
        public static IStickConfiguration IJKL { get; } = Keyboard(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using arrow keys.
        /// </summary>
        public static IStickConfiguration ArrowKeys { get; } = Keyboard(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

        private SimulatedStick(IGeneration<Signals.Touch> touch, IGeneration<Tilt> tilt)
        {
            Touch = touch;

            Tilt = tilt;
        }

        /// <inheritdoc/>
        public IGeneration<Signals.Touch> Touch { get; }

        /// <inheritdoc/>
        public IGeneration<Tilt> Tilt { get; }
    }
}
