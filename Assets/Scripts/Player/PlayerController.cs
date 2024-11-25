using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        Rigidbody rb;
        public float speed = 5.0f;
        private Vector3 movement;
        public Camera mainCamera;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        void Update()
        {
            HandleMovementInput();
            LookAtMouse();
        }
        
        private void FixedUpdate()
        {
            MovePlayer();
        }
        private void HandleMovementInput()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical") ;
            
            movement = new Vector3(moveX, 0, moveZ).normalized;
        }

        private void MovePlayer()
        {
            Vector3 move = movement* speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + move);
        }
        void LookAtMouse()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity))
            {
                Vector3 lookDirection = hitInfo.point - transform.position;
                lookDirection.y = 0f; 
                if (lookDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    rb.MoveRotation(targetRotation); 
                }
            }
        }
    }
}

