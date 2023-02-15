using UnityEngine;
using Client;

namespace Client {
    public class UnitDestinationMB : MonoBehaviour
    {
        public Transform[] UnitDestinations;

        [ExecuteInEditMode]
        private void OnDrawGizmos()
        {   
            if (UnitDestinations.Length < 2)
            {
                return;
            }

            for (int i = 0; i < UnitDestinations.Length; i++)
            {
                if (i + 1 >= UnitDestinations.Length)
                {
                    Gizmos.color = Color.red;
                    Gizmos.DrawSphere(UnitDestinations[i].position, 0.1f);
                    // Gizmos.DrawLine(PatrolPoints[i].position, PatrolPoints[0].position);


                    // Gizmos.color = Color.blue;
                    // Gizmos.DrawLine(PatrolPoints[i].position, PatrolPoints[0].position);

                    break;
                }

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(UnitDestinations[i].position, 0.1f);
                Gizmos.DrawLine(UnitDestinations[i].position, UnitDestinations[i + 1].position);

                Gizmos.color = Color.blue;
                Gizmos.DrawLine(UnitDestinations[i].position, UnitDestinations[i+1].position);
            }
        }
    }
    public enum Points {First, Two, Three, Four};
}   
