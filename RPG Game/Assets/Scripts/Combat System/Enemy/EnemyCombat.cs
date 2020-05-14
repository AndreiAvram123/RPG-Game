using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Scheduler))]


    public class EnemyCombat : MonoBehaviour, IActionHandler
    {
        [SerializeField] float attackRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float bareHandsAttackPower =5f;

        private CharacterMovement playerMovement;
        private Scheduler scheduler;
        private float timeSinceLastAttack = Mathf.Infinity;
        private Transform target;


        private void Start()
        {
            playerMovement = GetComponent<CharacterMovement>();
            scheduler = GetComponent<Scheduler>();

        }
        private void Update()
        {
            if (GetComponent<CharacterHealth>().IsCharacterDead()) return;
            TryToAttack();
        }


        public float AttackRange
        {
            get { return attackRange; }
        }

        private void TryToAttack()
        {
            if (target == null) return;
            if (target.GetComponent<CharacterHealth>().IsCharacterDead())
            {
                Cancel();
                return;
            }
            timeSinceLastAttack += Time.deltaTime;
            if (IsTargetWithinRange())
            {
                Attack();

            }
            else
            {
                playerMovement.ChaseTarget(target.position);
            }
        }

        private void Attack()
        {
            RotateCharacter();
            TriggerAttackAnimation();
            scheduler.StartAction(this);
        }


        private void RotateCharacter()
        {
            transform.LookAt(target);
        }

        //a trigger in animation returns to false after the animation finishes
        //this is the basis on which we trigger the attack 
        //after a certain amount of time
        private void TriggerAttackAnimation()
        {

            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                //This animation triggers Hit() 
                GetComponent<Animator>().SetTrigger("attack");
                GetComponent<Animator>().ResetTrigger("locomotion");
                timeSinceLastAttack = 0f;
            }

        }
        //animation event when we hit the target
        private void Hit()
        {
            //hit may be triggered when enemy is already dead
            if (target != null)
            {
                target.GetComponent<CharacterHealth>()
                    .DecreaseHealth(bareHandsAttackPower);
            }
        }

        private bool IsTargetWithinRange()
        {
            return Vector3.Distance(target.position, transform.position) <= attackRange;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }



        public void Cancel()
        {
            GetComponent<Animator>().SetTrigger("locomotion");
            target = null;
        }
    }
}
