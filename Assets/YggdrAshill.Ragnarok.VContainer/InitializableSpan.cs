using YggdrAshill.Ragnarok.Periodization;
using System;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Ragnarok.VContainer
{
    internal sealed class InitializableSpan :
        IInitializable,
        IDisposable
    {
        private readonly IOrigination origination;

        private readonly ITermination termination;

        [Inject]
        public InitializableSpan(ISpan span)
        {
            if (span is null)
            {
                throw new ArgumentNullException(nameof(span));
            }

            origination = span.Origination;

            termination = span.Termination;
        }

        public void Initialize()
        {
            origination.Originate();
        }

        public void Dispose()
        {
            termination.Terminate();
        }
    }
}
