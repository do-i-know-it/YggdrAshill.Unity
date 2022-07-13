using Cysharp.Threading.Tasks;
using System.Threading;
using System;

namespace YggdrAshill.Samples
{
    public interface ICapsule<TResponse> :
        IDisposable
    {
        UniTask<TResponse> LoadAysnc(CancellationToken token);
    }
}
