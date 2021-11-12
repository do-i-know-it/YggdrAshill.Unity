using YggdrAshill.Ragnarok.Periodization;
using YggdrAshill.Ragnarok.Construction;
using UnityEngine;

namespace YggdrAshill.Ragnarok.Unity
{
    public abstract class ServiceCycle : MonoBehaviour
    {
        [SerializeField] private UnityUpdateClock clock;
        protected UnityUpdateClock Clock => clock;

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
        }

        protected virtual void OnDestroy()
        {
            origination = null;

            termination = null;

            execution = null;
        }

        protected virtual void OnEnable()
        {
            origination.Originate();
        }

        protected virtual void OnDisable()
        {
            termination.Terminate();
        }

        protected virtual void FixedUpdate()
        {
            if (Clock != UnityUpdateClock.FixedUpdate)
            {
                return;
            }

            execution.Execute();
        }

        protected virtual void Update()
        {
            if (Clock != UnityUpdateClock.Update)
            {
                return;
            }

            execution.Execute();
        }

        protected virtual void LateUpdate()
        {
            if (Clock != UnityUpdateClock.LateUpdate)
            {
                return;
            }

            execution.Execute();
        }
    }
}
