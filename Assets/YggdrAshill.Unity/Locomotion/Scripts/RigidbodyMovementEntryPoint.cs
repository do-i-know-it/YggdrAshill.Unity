using YggdrAshill.Nuadha;
using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Conduction;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Unity;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    public sealed class RigidbodyMovementEntryPoint :
        IStartable,
        IPostTickable,
        IFixedTickable,
        IDisposable
    {
        private readonly IProduction<Tilt> tilt;

        private readonly IProduction<Space3D.Direction> direction;

        private readonly Rigidbody rigidbody;

        private IDisposable disposable;

        private Vector3 cachedDirection;

        private Vector2 cachedTilt;

        private Vector3 velocity;

        [Inject]
        public RigidbodyMovementEntryPoint(IProduction<Tilt> tilt, IProduction<Space3D.Direction> direction, Rigidbody rigidbody)
        {
            this.tilt = tilt;

            this.direction = direction;

            this.rigidbody = rigidbody;
        }

        public void Start()
        {
            disposable
                = CancellationSource.Default
                .Synthesize(tilt.Produce(SimulateTilt.ToConsume(signal =>
                {
                    cachedTilt = signal;
                })))
                .Synthesize(direction.Produce(SimulateSpace3D.ToConsumeDirection(signal =>
                {
                    cachedDirection = signal;
                })))
                .Build()
                .ToDisposable();
        }

        public void Dispose()
        {
            disposable.Dispose();

            disposable = null;
        }

        public void PostTick()
        {
            velocity = Vector3.ProjectOnPlane(new Vector3(cachedTilt.x, 0.0f, cachedTilt.y), cachedDirection);
        }

        public void FixedTick()
        {
            rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        }
    }
}
