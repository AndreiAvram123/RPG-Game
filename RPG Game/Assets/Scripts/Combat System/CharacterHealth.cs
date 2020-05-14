using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Combat
{
    [RequireComponent(typeof(Scheduler))]
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] float healthPoints = 20f;
        private bool characterDead = false;

        public bool IsCharacterDead()
        {
            return characterDead;
        }

        public void DecreaseHealth(float damageAmount)
        {
            healthPoints -= damageAmount;
            if (healthPoints <= 0)
            { 
                TriggerDeathSequence();
                characterDead = true;
            }
        }

        protected virtual void TriggerDeathSequence()
        {
            TriggerDieAnimation();
            GetComponent<Scheduler>().CancelCurrentAction();
        }  

        protected void TriggerDieAnimation()
        {
            if (characterDead) return;
            GetComponent<Animator>().SetTrigger("die");
        }
    }
}
