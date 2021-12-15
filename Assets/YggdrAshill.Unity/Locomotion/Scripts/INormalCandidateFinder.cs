using UnityEngine;
using System.Collections.Generic;

namespace YggdrAshill.Unity
{
    public interface INormalCandidateFinder
    {
        IEnumerable<Vector3> Find();
    }
}
