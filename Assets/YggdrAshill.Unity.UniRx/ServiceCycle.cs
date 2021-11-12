﻿using YggdrAshill.Ragnarok;
using YggdrAshill.Ragnarok.Periodization;
using YggdrAshill.Ragnarok.Construction;
using System;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace YggdrAshill.Unity.UniRx
{
    public abstract class ServiceCycle : MonoBehaviour
    {
        [SerializeField] private UniRxUpdateClock clock;
        protected UniRxUpdateClock Clock => clock;

        private IOrigination origination;

        private ITermination termination;

        private IExecution execution;

        protected abstract IService Configure(IService service);

        protected virtual void Awake()
        {
            var cycle = Configure(Service.Default).Build();

            origination = cycle.Span.Origination;

            termination = cycle.Span.Termination;

            execution = cycle.Execution;

            this.OnEnableAsObservable()
                .Subscribe(_ => origination.Originate())
                .AddTo(this);

            this.OnDisableAsObservable()
                .Subscribe(_ => termination.Terminate())
                .AddTo(this);

            switch (clock)
            {
                case UniRxUpdateClock.Update:
                    this.UpdateAsObservable()
                        .Subscribe(_ => execution.Execute())
                        .AddTo(this);
                    break;
                case UniRxUpdateClock.LateUpdate:
                    this.LateUpdateAsObservable()
                        .Subscribe(_ => execution.Execute())
                        .AddTo(this);
                    break;
                case UniRxUpdateClock.FixedUpdate:
                    this.FixedUpdateAsObservable()
                        .Subscribe(_ => execution.Execute())
                        .AddTo(this);
                    break;
                default:
                    throw new NotSupportedException(nameof(UniRxUpdateClock));
            }
        }

        protected virtual void OnDestroy()
        {
            origination = null;

            termination = null;

            execution = null;
        }
    }
}
