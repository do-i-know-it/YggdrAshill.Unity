using YggdrAshill.Ragnarok.Periodization;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity.VContainer
{
    internal sealed class PostTickableCycle :
        IInitializable,
        IPostTickable,
        IDisposable
    {
        private readonly IOrigination origination;

        private readonly ITermination termination;

        private readonly IExecution execution;

        [Inject]
        public PostTickableCycle(ICycle cycle)
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

        public void PostTick()
        {
            execution.Execute();
        }
    }
}
