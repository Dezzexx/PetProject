using UnityEngine.AI;

namespace Client {
    struct Unit {
        public NavMeshAgent NavMeshAgent;
        public Points UnitDestinationPoint;
        public UnitTypes UnitType;
        public UnitMB UnitMB;
    }
}