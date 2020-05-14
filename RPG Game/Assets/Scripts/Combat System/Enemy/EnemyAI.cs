using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [SelectionBase]
    [RequireComponent(typeof(EnemyCombat))]
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(Scheduler))]


    public class EnemyAI : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 4f;
        [SerializeField] float waypointDwellingTime = 2f;
        [SerializeField] private float patrolSpeedFraction = 0.25f;

        private Transform player;
        private Vector3 startPosition;
        private EnemyCombat characterCombatSystem;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        //waypoint system
        private float waypointTolerance = 1.5f;
        private Waypoint currentWaypoint;
        private EnemyPath enemyPath;

        private void Start()
        {
            characterCombatSystem = GetComponent<EnemyCombat>();
            player = GameObject.FindGameObjectWithTag("Player").transform;
            enemyPath = GetComponent<EnemyPath>();
            SetStartWaypoint();
            startPosition = transform.position;
        }

        private void SetStartWaypoint()
        {
            if (enemyPath == null) return;

            if (enemyPath.isPathValid())
            {
                currentWaypoint = enemyPath.GetPath()[0];
               
            }

        }

        private void Update()
        {
            TryToChase();
        }

        //Called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }

        private void TryToChase()
        {

            if (IsPlayerWithinChaseDistance())
            {
                ChasePlayer();
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                StartSuspicionBehavior();
            }
            else
            {
            
                StartPatrolBehaviour();
            }
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void StartSuspicionBehavior()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            GetComponent<Scheduler>().CancelCurrentAction();
        }


        private void ChasePlayer()
        {
            timeSinceLastSawPlayer = 0f;
            characterCombatSystem.SetTarget(player);
        }

        private void StartPatrolBehaviour()
        {
            //if there is no path
            Vector3 positionToMove = startPosition;

            if (enemyPath != null &&
                enemyPath.isPathValid())
            {
      
                if (IsPlayerAtWaypoint())
                {
                    currentWaypoint = GetNextWaypoint();
                    timeSinceArrivedAtWaypoint = 0;
                }
              
                positionToMove = currentWaypoint.transform.position;
          
           if(timeSinceArrivedAtWaypoint >= waypointDwellingTime)
                GetComponent<CharacterMovement>()
               .MovePlayer(positionToMove,patrolSpeedFraction);
            }
            else
            {
                GetComponent<CharacterMovement>()
              .MovePlayer(positionToMove);
            }

          
           
        }

        private bool IsPlayerAtWaypoint()
        {
            return Vector3.Distance(transform.position,
                currentWaypoint.transform.position)
                <= waypointTolerance;
        }

        private Waypoint GetNextWaypoint()
        {
            int nextWaypointIndex = enemyPath.GetPath().IndexOf(currentWaypoint) +1;
          
            if (nextWaypointIndex >= enemyPath.GetPath().Count)
            {
                nextWaypointIndex = 0;
            }
         
            return enemyPath.GetPath()[nextWaypointIndex];

        }

        private bool IsPlayerWithinChaseDistance()
        {
            return Vector3.Distance(player.position,
                transform.position) <= chaseDistance;
        }
    }
}
