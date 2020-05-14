using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Scheduler))]

    public class PlayerMovementSystem : MonoBehaviour, IActionHandler
    {

        CharacterController characterController;
        public float speed = 6.0f;
        public float jumpSpeed = 8.0f;
        public float gravity = 20.0f;
        private Vector3 moveDirection = Vector3.zero;
        private float horizontalSensibility = 0f;
        private float verticalSensibility = 0f;


        // Start is called before the first frame update
        void Start()
        {
            characterController = GetComponent<CharacterController>();


        }

        // Update is called once per frame
        void Update()
        {
            MovePlayer();

        }


        private void MovePlayer()
        {
            if (characterController.isGrounded)
            {
                // We are grounded, so recalculate
                // move direction directly from axes
                moveDirection = new Vector3(horizontalSensibility, 0.0f,
                    verticalSensibility);
                RotatePlayer();
                moveDirection *= speed;
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
            AdjustMovementAnimation();

        }
        private void AdjustMovementAnimation()
        {
            Vector3 localSpeed = transform.InverseTransformDirection(moveDirection);
            //get the z speed
            float speed = localSpeed.z;
            GetComponent<Animator>().SetFloat("forward", speed);
        }
        private void RotatePlayer()
        {
            if (moveDirection != Vector3.zero)
                transform.rotation = Quaternion.LookRotation(moveDirection);

        }

        public void MovePlayer(float horizontalSensibility, float verticalSensibility)
        {

            this.horizontalSensibility = horizontalSensibility;
            this.verticalSensibility = verticalSensibility;
        }

        public void Cancel()
        {
            MovePlayer(0f, 0f);
        }
    }
}
