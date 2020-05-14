using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Waypoint : MonoBehaviour
    {
        //leave null if it is the first waypoint
        public Waypoint previousWaypoint;
      

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.grey;
            Gizmos.DrawSphere(transform.position,
                0.3f);

           
           if (previousWaypoint == null) return;
            Gizmos.DrawLine(previousWaypoint.transform.position,
                transform.position);
        }

    }
}
