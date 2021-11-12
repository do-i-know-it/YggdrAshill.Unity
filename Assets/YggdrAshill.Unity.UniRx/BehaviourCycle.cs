using YggdrAshill.Ragnarok.Periodization;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace YggdrAshill.Unity.UniRx
{
    public abstract class BehaviourCycle : BehaviourSpan
    {
        [SerializeField] private UniRxUpdateClock clock;
        protected UniRxUpdateClock Clock => clock;

        protected abstract IExecution Execution { get; }

        protected override void Awake()
        {
            base.Awake();

            switch (clock)
            {
                case UniRxUpdateClock.Update:
                    this.UpdateAsObservable()
                        .Subscribe(_ => Execution.Execute())
                        .AddTo(this);
                    break;
                case UniRxUpdateClock.LateUpdate:
                    this.LateUpdateAsObservable()
                        .Subscribe(_ => Execution.Execute())
                        .AddTo(this);
                    break;
                case UniRxUpdateClock.FixedUpdate:
                    this.FixedUpdateAsObservable()
                        .Subscribe(_ => Execution.Execute())
                        .AddTo(this);
                    break;
                default:
                    throw new NotSupportedException(nameof(UniRxUpdateClock));
            }
        }
    }
}
