using UnityEngine;

namespace Client {
    struct UnitChangePathEvent {
        public Vector3 NewDestination;
        public bool IsDirectionToAttack;
    }
}