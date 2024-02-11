using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    public class PlayerMovement : Player
    {
        [Header("Walk")]
        [SerializeField] private float walkSpeed;

        [Header("Sprint")]
        [SerializeField] private KeyCode sprintKey;
        [SerializeField] [Range(1, 3)] private float sprintMultiplier;
        [SerializeField] private TrailRenderer sprintTrail;

        [SerializeField] private Animator animator;
        [SerializeField] private Transform graphicsTransform;
        
        private float timeSinceStoppedSprinting;
        
        
        private float sprintSpeed;
        private bool isDashing = false;

        private Vector2 input;

        private Rigidbody2D rb;

        private static readonly int Walking = Animator.StringToHash("Walking");
        private static readonly int Sprinting = Animator.StringToHash("Sprinting");


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, input );
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        protected override void Update()
        {
            base.Update();
            
            input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            
            var isWalking = input != Vector2.zero;
            var isSprinting = Input.GetKey(sprintKey);
            
            
            if (isSprinting)
            {
                sprintSpeed = walkSpeed * sprintMultiplier;
                timeSinceStoppedSprinting = 0;
            }

            sprintTrail.enabled = Input.GetKey(sprintKey);

            animator.SetBool(Walking, isWalking);
            animator.SetBool(Sprinting, isSprinting);
            
            
            timeSinceStoppedSprinting += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (isDashing) return;
            
            var normalizedInput = input.normalized;
            rb.velocity = normalizedInput.magnitude != 0 ? (new Vector2(normalizedInput.x, normalizedInput.y) *
                          (!Input.GetKey(sprintKey) ? walkSpeed : sprintSpeed) * Time.fixedDeltaTime) : Vector2.zero;
            runtimeData.CurrentDirection = input;
            graphicsTransform.localScale = new Vector3(input.x < 0 ? -1 : 1, 1, 1);
        }

    }
}