using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    public class PlayerMovement : Player
    {
        [Header("Dash")]
        [SerializeField] private KeyCode dashKey;
        [SerializeField] private float dashSpeed = 150f;
        [SerializeField] private float dashDuration = 0.2f;
        [SerializeField] private List<TrailRenderer> dashTrails = new();

        [Header("Stamina")]
        [SerializeField] private float maxStamina = 20f;
        [SerializeField][Range(0,1)] private float staminaDrainRate = 0.5f;
        [SerializeField][Range(0,2)] private float staminaRegenRate = 0.5f;
        [SerializeField][Range(0,1)] private float staminaRegenDelay = 0.5f;
        
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
            
            if (Input.GetKeyDown(dashKey) && !isDashing)
            {
                StartCoroutine(Dash());
            }
            
            if (isSprinting)
            {
                sprintSpeed = walkSpeed * sprintMultiplier;
                runtimeData.Stamina -= staminaDrainRate * Time.deltaTime;
                timeSinceStoppedSprinting = 0;
            }

            sprintTrail.enabled = Input.GetKey(sprintKey);

            animator.SetBool(Walking, isWalking);
            animator.SetBool(Sprinting, isSprinting);
            
            runtimeData.Stamina += timeSinceStoppedSprinting > staminaRegenDelay ? staminaRegenRate * Time.deltaTime : 0;
            
            timeSinceStoppedSprinting += Time.deltaTime;
        }

        private void FixedUpdate()
        {
            if (isDashing) return;
            var normalizedInput = input.normalized;
            rb.velocity = new Vector2(normalizedInput.x, normalizedInput.y) *
                          (!Input.GetKey(sprintKey) ? walkSpeed : sprintSpeed) * Time.fixedDeltaTime;
            runtimeData.CurrentDirection = input;
            graphicsTransform.localScale = new Vector3(input.x < 0 ? -1 : 1, 1, 1);
        }

        private IEnumerator Dash()
        {
            isDashing = true;
            float dashStartTime = Time.time;
            foreach (var dt in dashTrails)
            {
                dt.gameObject.SetActive(true);
            }

            while (Time.time - dashStartTime < dashDuration)
            {
                rb.velocity = input.normalized * dashSpeed * Time.fixedDeltaTime;
                yield return null;
            }

            rb.velocity = Vector2.zero;
            isDashing = false;
            if (dashTrails.Count > 0)
            {
                yield return new WaitForSeconds(dashTrails[0].time);
                foreach (var dt in dashTrails)
                {
                    dt.gameObject.SetActive(false);
                }
            }
        }
    }
}