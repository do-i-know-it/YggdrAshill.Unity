using YggdrAshill.Ragnarok.Periodization;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace YggdrAshill.Unity.UniRx
{
    public abstract class BehaviourSpan : MonoBehaviour
    {
        protected abstract IOrigination Origination { get; }

        protected abstract ITermination Termination { get; }

        protected virtual void Awake()
        {
            this.OnEnableAsObservable()
                .Subscribe(_ => Origination.Originate())
                .AddTo(this);

            this.OnDisableAsObservable()
                .Subscribe(_ => Termination.Terminate())
                .AddTo(this);
        }
    }
}
