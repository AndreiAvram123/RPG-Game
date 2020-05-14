using UnityEngine;
using UnityEngine.AI;
using RPG.Core;
using System;
using RPG.Combat;

//use namespaces to see dependencies better
namespace RPG.Core
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Scheduler))]
    [RequireComponent(typeof(CharacterHealth))]

    public class CharacterMovement : MonoBehaviour, IActionHandler
    {
        private NavMeshAgent navMeshAgent;
        private Scheduler scheduler;
        [SerializeField] private float maxSpeed = 6f; 
        
        // Start is called before the first frame update
        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            scheduler = GetComponent<Scheduler>();

        }

        // Update is called once per frame
        void Update()
        {
            if (isCharacterDead()) return;
            AdjustAnimation();

        }

        private bool isCharacterDead()
        {
            return GetComponent<CharacterHealth>().IsCharacterDead();
        }

        private void AdjustAnimation()
        {
            Vector3 navAgentSpeed = navMeshAgent.velocity;
            //convert from world speed to local speed 
            Vector3 localSpeed = transform.InverseTransformDirection(navAgentSpeed);
            //get the z speed
            float speed = localSpeed.z;
            GetComponent<Animator>().SetFloat("forward", speed);
        }

        //be able to call this method from outside

        public void ChaseTarget(Vector3 target)
        {
            if (isCharacterDead()) return;
            StartNavAgent(target);
        }

        public void MovePlayer(Vector3 position)
        {
            if (isCharacterDead()) return;
            scheduler.StartAction(this);
            StartNavAgent(position);
        }

        public void MovePlayer(Vector3 position,
            float patrolSpeedRatio)
        {
            MovePlayer(position);
            
        }
        

        private void StartNavAgent(Vector3 position)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(position);
        }


        public void Cancel()
        {
            navMeshAgent.isStopped = true;

        }

    }
}

