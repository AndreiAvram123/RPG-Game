using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Combat
{
  
    public class EnemyHealth : CharacterHealth
    {

        protected override void TriggerDeathSequence()
        {
            base.TriggerDeathSequence();
            DisableComponents();

        }

        private void DisableComponents()
        {
            GetComponent<CapsuleCollider>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = false;
        }
    }
}
