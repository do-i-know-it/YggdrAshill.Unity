using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines extensions for Signals in Unity.
    /// </summary>
    public static class SignalExtension
    {
        /// <summary>
        /// Converts <see cref="Vector2"/> to <see cref="Tilt"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector2"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Tilt"/> converted.
        /// </returns>
        public static Tilt ToTilt(this Vector2 signal)
        {
            var normalized = signal.normalized;

            return new Tilt(normalized.x, normalized.y);
        }

        /// <summary>
        /// Converts <see cref="Tilt"/> to <see cref="Vector2"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Tilt"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Vector2"/> converted.
        /// </returns>
        public static Vector2 ToVector(this Tilt signal)
        {
            return new Vector2(signal.Horizontal, signal.Vertical);
        }
    }
}
