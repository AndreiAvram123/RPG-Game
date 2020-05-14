using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
namespace RPG.Combat
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Scheduler))]
    public class PlayerCombatSystem : MonoBehaviour,IActionHandler
    {
        [SerializeField] float attackRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float bareHandsAttackPower = 5f;


        private float timeSinceLastAttack = Mathf.Infinity;
    

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

        }


        public void Attack()
        {
            TriggerAttackAnimation();

        }

        public void StopAttack()
        {
            GetComponent<Animator>().SetTrigger("locomotion");
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
        
     private void AddDamageToSurrounding()
        {
            Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position
                , attackRange);
            foreach(Collider collider in hitColliders)
            {
                if (IsEnemyHit(collider)) return;
            }
        }
        private bool IsEnemyHit(Collider collider)
        {
            EnemyHealth enemyHealth = collider.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.DecreaseHealth(bareHandsAttackPower);
                return true;
            }
            return false;
        }
    
    //animation event when we hit the target
    private void Hit()
    {
       AddDamageToSurrounding();
    }

        public void Cancel()
        {
            StopAttack();
        }
    }
    }
