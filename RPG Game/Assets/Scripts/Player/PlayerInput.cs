using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using RPG.Core;
using RPG.Combat;
using System;


namespace RPG.Control
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerCombatSystem))]
    [RequireComponent(typeof(PlayerMovementSystem))]
    public class PlayerInput:MonoBehaviour
    {
        [SerializeField] Texture2D mouseOverPlayerTexture;
        [SerializeField] float mouseRotationSensibility;

        private PlayerCombatSystem playerCombatSystem;
        private PlayerMovementSystem playerMovementSystem;

        private const string attackKey = "attack";

        private CursorMode cursorMode = CursorMode.Auto;
        private Vector2 hotSpot = Vector2.zero;
   
        private void Update()
        {
            CheckInput();
   
        }
        private void Start()
        {
            playerCombatSystem = GetComponent<PlayerCombatSystem>();
            playerMovementSystem = GetComponent<PlayerMovementSystem>();
        }
        private void OnMouseEnter()
        {
            Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(cameraRay);
             foreach(RaycastHit raycastHit in hits)
            {
                if(raycastHit.collider.tag == "Player")
                {
                    ChangeMouseAppearance(mouseOverPlayerTexture);
                   
                }
            }
        }

      
        private void RotateCameraIfPossible()
        {
            if (CrossPlatformInputManager.GetButton("RightButton"))
            {
               
            }
        }

        private void ChangeMouseAppearance(Texture2D newTexture)
        {
            Cursor.SetCursor(newTexture, hotSpot, cursorMode);

        }
        void OnMouseExit()
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }


        private void CheckInput()
        {
            RotateCameraIfPossible();
            if (CrossPlatformInputManager.GetButton(attackKey))
            {
                playerCombatSystem.Attack();
                playerMovementSystem.MovePlayer(0f, 0f);
            }
            else
            {
                playerMovementSystem.MovePlayer(CrossPlatformInputManager.GetAxis("Horizontal"),
                 CrossPlatformInputManager.GetAxis("Vertical"));
            }
            



        }
       

       
        //private bool ShootRayForAttack()
        //{
           
        //    RaycastHit[] hits = Physics.RaycastAll(GetCameraRay());
            
        //    foreach(RaycastHit raycastHit in hits)
        //    {
       
        //        //check to see if there is an enemy
        //        Enemy enemy = raycastHit.transform.GetComponent<Enemy>();
        //        if (!enemy) return false;

        //        if (CrossPlatformInputManager.GetButtonDown("Fire1")){
        //            playerCombatSystem.SetTarget(enemy.transform);
        //            return true;
        //        }
                
        //    }
        //    return false;

        //}

        //private bool ShootRayForMovement()
        //{

        //    RaycastHit raycastHit;
        //    bool hasHit = Physics.Raycast(GetCameraRay(),
        //        out raycastHit
        //        );

        //    if (hasHit)
        //    {
        //        playerMovement.MovePlayer(raycastHit.point);   
        //        return true;
        //    }
        //    return false;
        //}

        //private static Ray GetCameraRay()
        //{
        //    return Camera.main.ScreenPointToRay(Input.mousePosition);
        //}
    }
    
}