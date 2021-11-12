using YggdrAshill.Ragnarok.Periodization;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity.VContainer
{
    internal sealed class PreTickableCycle :
        IInitializable,
        IDisposable
    {
        private readonly IOrigination origination;

        private readonly ITermination termination;

        private readonly IExecution execution;

        [Inject]
        public PreTickableCycle(ICycle cycle)
        {
            if (cycle is null)
            {
                throw new ArgumentNullException(nameof(cycle));
            }

            origination = cycle.Span.Origination;

            termination = cycle.Span.Termination;

            execution = cycle.Execution;
        }

        public void Initialize()
        {
            origination.Originate();
        }

        public void Dispose()
        {
            termination.Terminate();
        }

        public void Tick()
        {
            execution.Execute();
        }
    }
}
