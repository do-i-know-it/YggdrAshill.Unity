using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Nuadha.Unity
{
    /// <summary>
    /// Defines extensions for <see cref="Tilt"/> in Unity.
    /// </summary>
    public static class TiltExtension
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
            if (signal.sqrMagnitude - Tilt.Length > 0.0f)
            {
                signal = signal.normalized;
            }
            
            return new Tilt(signal.x, signal.y);
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
