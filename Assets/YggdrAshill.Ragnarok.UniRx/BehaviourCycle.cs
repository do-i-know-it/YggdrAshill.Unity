﻿using YggdrAshill.Ragnarok.Periodization;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace YggdrAshill.Ragnarok.UniRx
{
    public abstract class BehaviourCycle : BehaviourSpan
    {
        [SerializeField] private Clock clock;
        protected Clock Clock => clock;

        protected abstract IExecution Execution { get; }

        protected override void Awake()
        {
            base.Awake();

            switch (Clock)
            {
                case Clock.Update:
                    this.UpdateAsObservable()
                        .Subscribe(_ => Execution.Execute())
                        .AddTo(this);
                    break;
                case Clock.LateUpdate:
                    this.LateUpdateAsObservable()
                        .Subscribe(_ => Execution.Execute())
                        .AddTo(this);
                    break;
                case Clock.FixedUpdate:
                    this.FixedUpdateAsObservable()
                        .Subscribe(_ => Execution.Execute())
                        .AddTo(this);
                    break;
                default:
                    throw new NotSupportedException(nameof(UniRx.Clock));
            }
        }
    }
}
