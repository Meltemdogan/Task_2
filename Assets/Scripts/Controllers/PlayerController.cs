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
            Movement();
            
            LookAtMouse();
        }
        
        
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        }
        private void Movement()
        {
            float moveX = Input.GetAxis("Horizontal");
            float moveZ = Input.GetAxis("Vertical") ;
            
            movement = transform.right * moveX + transform.forward * moveZ;
            
            transform.position += movement * speed * Time.deltaTime;
        }
        
        void LookAtMouse()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float distance))
            {
                Vector3 targetPoint = ray.GetPoint(distance);
                
                Vector3 lookDirection = targetPoint - transform.position;
                lookDirection.y = 0;
                transform.forward = lookDirection.normalized;
            }
        }
        
        void Shot()
        {
            
        }
    }
}

