using YggdrAshill.Nuadha.Signalization;
using YggdrAshill.Nuadha.Signals;
using YggdrAshill.Nuadha.Unity;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace YggdrAshill.Unity
{
    public sealed class NormalDetectionEntryPoint :
        ITickable
    {
        private readonly INormalCandidateFinder finder;

        private readonly IConsumption<Space3D.Direction> consumption;

        [Inject]
        public NormalDetectionEntryPoint(INormalCandidateFinder finder, IConsumption<Space3D.Direction> consumption)
        {
            this.finder = finder;

            this.consumption = consumption;
        }

        public void Tick()
        {
            var normalList
                = finder.Find();

            var x = normalList.Average(normal => normal.x);
            var y = normalList.Average(normal => normal.y);
            var z = normalList.Average(normal => normal.z);

            var normal = new Vector3(x, y, z);

            consumption.Consume(normal.ToDirection());
        }
    }
}
