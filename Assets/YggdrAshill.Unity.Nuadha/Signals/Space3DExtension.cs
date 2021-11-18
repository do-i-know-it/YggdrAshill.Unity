using YggdrAshill.Nuadha.Signals;
using UnityEngine;

namespace YggdrAshill.Unity.Nuadha
{
    /// <summary>
    /// Defines extensions for <see cref="Space3D"/> in Unity.
    /// </summary>
    public static class Space3DExtension
    {
        /// <summary>
        /// Converts <see cref="Vector3"/> to <see cref="Space3D.Position"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector3"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Space3D.Position"/> converted.
        /// </returns>
        public static Space3D.Position ToPosition(this Vector3 signal)
        {
            return new Space3D.Position(signal.x, signal.y, signal.z);
        }

        /// <summary>
        /// Converts <see cref="Space3D.Position"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Space3D.Position"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Vector3"/> converted.
        /// </returns>
        public static Vector3 ToVector(this Space3D.Position signal)
        {
            return new Vector3(signal.Horizontal, signal.Vertical, signal.Frontal);
        }

        /// <summary>
        /// Converts <see cref="Vector3"/> to <see cref="Space3D.Direction"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Vector3"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Space3D.Direction"/> converted.
        /// </returns>
        public static Space3D.Direction ToDirection(this Vector3 signal)
        {
            var normalized = signal.normalized;

            return new Space3D.Direction(normalized.x, normalized.y, normalized.z);
        }

        /// <summary>
        /// Converts <see cref="Space3D.Direction"/> to <see cref="Vector3"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Space3D.Direction"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Vector3"/> converted.
        /// </returns>
        public static Vector3 ToVector(this Space3D.Direction signal)
        {
            return new Vector3(signal.Horizontal, signal.Vertical, signal.Frontal);
        }

        /// <summary>
        /// Converts <see cref="Quaternion"/> to <see cref="Space3D.Rotation"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Quaternion"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Space3D.Rotation"/> converted.
        /// </returns>
        public static Space3D.Rotation ToRotation(this Quaternion signal)
        {
            var normalized = signal.normalized;

            return new Space3D.Rotation(normalized.x, normalized.y, normalized.z, normalized.w);
        }

        /// <summary>
        /// Converts <see cref="Space3D.Rotation"/> to <see cref="Quaternion"/>.
        /// </summary>
        /// <param name="signal">
        /// <see cref="Space3D.Rotation"/> to covert.
        /// </param>
        /// <returns>
        /// <see cref="Quaternion"/> converted.
        /// </returns>
        public static Quaternion ToQuaternion(this Space3D.Rotation signal)
        {
            return new Quaternion(signal.Horizontal, signal.Vertical, signal.Frontal, signal.Real);
        }
    }
}
