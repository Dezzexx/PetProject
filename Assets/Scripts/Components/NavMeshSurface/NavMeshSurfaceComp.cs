using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

namespace Client {
    struct NavMeshSurfaceComp {
        public NavMeshSurface Surface;
        public List<Transform> DestinationPoints;
        public List<UnitMB> AllFriendlyUnits;
    }
}