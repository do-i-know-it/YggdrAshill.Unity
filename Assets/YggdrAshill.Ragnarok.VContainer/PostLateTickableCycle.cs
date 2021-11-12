using YggdrAshill.Ragnarok.Periodization;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Ragnarok.VContainer
{
    internal sealed class PostLateTickableCycle :
        IInitializable,
        IPostLateTickable,
        IDisposable
    {
        private readonly IOrigination origination;

        private readonly ITermination termination;

        private readonly IExecution execution;

        [Inject]
        public PostLateTickableCycle(ICycle cycle)
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

        public void PostLateTick()
        {
            execution.Execute();
        }
    }
}
