using UnityEngine;

namespace Client {
    struct DamageEvent {
        public int EntityTarget;
        public float Damage;
        public Vector3 Epicenter;
        public float ForceAmount;
    }
}