using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Units;
using UnityEngine;
using Touch = YggdrAshill.Nuadha.Signals.Touch;

namespace YggdrAshill.Nuadha.Unity
{
    public sealed class SimulateStick :
        IStickConfiguration
    {
        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        /// <param name="forward">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Tilt"/>.
        /// </param>
        /// <param name="backward">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Tilt"/>.
        /// </param>
        /// <param name="left">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Tilt"/>.
        /// </param>
        /// <param name="right">
        /// <see cref="KeyCode"/> to generate <see cref="Signals.Touch"/> and <see cref="Signals.Tilt"/>.
        /// </param>
        /// <returns>
        /// <see cref="IStickConfiguration"/> created.
        /// </returns>
        public static IStickConfiguration ToConfigure(KeyCode forward, KeyCode backward, KeyCode left, KeyCode right)
        {
            return new SimulateStick()
            {
                Touch = SimulateTouch.Any(forward, backward, left, right),
                Tilt = SimulateTilt.ToGenerate(forward, backward, left, right),
            };
        }

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        public static IStickConfiguration WASD { get; } = ToConfigure(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        public static IStickConfiguration IJKL { get; } = ToConfigure(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using <see cref="Input.GetKey(KeyCode)"/>.
        /// </summary>
        public static IStickConfiguration ArrowKeys { get; } = ToConfigure(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

        /// <inheritdoc/>
        public IGeneration<Touch> Touch { get; private set; }

        /// <inheritdoc/>
        public IGeneration<Tilt> Tilt { get; private set; }
    }
}
