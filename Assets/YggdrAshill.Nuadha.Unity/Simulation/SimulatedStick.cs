using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines implementations for <see cref="IStickConfiguration"/> simulated using <see cref="Input"/>.
    /// </summary>
    public sealed class SimulatedStick :
        IGeneration<Signals.Touch>,
        IGeneration<Tilt>,
        IStickConfiguration
    {
        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using WASD.
        /// </summary>
        public static IStickConfiguration WASD { get; } = new SimulatedStick(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using IJKL.
        /// </summary>
        public static IStickConfiguration IJKL { get; } = new SimulatedStick(KeyCode.W, KeyCode.S, KeyCode.A, KeyCode.D);

        /// <summary>
        /// Simulated <see cref="IStickConfiguration"/> using arrow keys.
        /// </summary>
        public static IStickConfiguration ArrowKey { get; } = new SimulatedStick(KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.LeftArrow, KeyCode.RightArrow);

        private readonly KeyCode forward;
        private readonly KeyCode backward;
        private readonly KeyCode left;
        private readonly KeyCode right;

        private SimulatedStick(KeyCode forward, KeyCode backward, KeyCode left, KeyCode right)
        {
            this.forward = forward;
            this.backward = backward;
            this.left = left;
            this.right = right;
        }

        Signals.Touch IGeneration<Signals.Touch>.Generate()
        {
            var signal
                = Input.GetKey(forward)
                || Input.GetKey(backward)
                || Input.GetKey(left)
                || Input.GetKey(right);

            return signal.ToTouch();
        }

        Tilt IGeneration<Tilt>.Generate()
        {
            var right = Input.GetKey(this.right) ? 1f : 0f;
            var left = Input.GetKey(this.left) ? -1f : 0f;

            var horizontal = right + left;

            var forward = Input.GetKey(this.forward) ? 1f : 0f;
            var backward = Input.GetKey(this.backward) ? -1f : 0f;

            var vertical = forward + backward;

            return new Vector2(horizontal, vertical).ToTilt();
        }

        /// <inheritdoc/>
        public IGeneration<Signals.Touch> Touch => this;

        /// <inheritdoc/>
        public IGeneration<Tilt> Tilt => this;
    }
}
