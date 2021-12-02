using System;
using UnityEngine;

namespace YggdrAshill.UniRx
{
    [Serializable]
    public struct PoseAdjustment
    {
        [SerializeField] private Vector3 position;

        [SerializeField] private Vector3 rotation;

        public Vector3 Position => position;

        public Quaternion Rotation => Quaternion.Euler(rotation);

        public Pose Pose => new Pose(Position, Rotation);
    }
}
