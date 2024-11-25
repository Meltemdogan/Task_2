using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        Rigidbody rb;
        public float speed = 5.0f;
        private Vector3 movement;
        public Camera mainCamera;
        [SerializeField] private HealthBar healthBar;
        
        private void Awake()
        {
            Instance = this;
        }
        
        [SerializeField] private float m_MaxHealth = 100f;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            healthBar.initialize(m_MaxHealth);
        }
        
        void Update()
        {
            HandleMovementInput();
            LookAtMouse();
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }
        
        private void FixedUpdate()
        {
            // MovePlayer();
        }
        
        public void TakeDamage(float damage)
        {
            healthBar.TakeDamage(damage);
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

