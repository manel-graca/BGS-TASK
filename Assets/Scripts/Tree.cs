using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BGS.Task
{
    public class Tree : ResourceWorldObject, IInteractable
    {
        [SerializeField] private int woodAmount;
        [SerializeField] private Vector2 woodPerHitRange = new Vector2(5,30);
        
        [SerializeField][Range(0,0.5f)] private float hitShakeDuration = 0.1f;
        [SerializeField][Range(2,20)] private float hitShakeStrength = 5f;
        
        [SerializeField] private GameObject aliveGraphics;
        [SerializeField] private GameObject deadGraphics;
        
        [SerializeField] private ParticleSystem hitEffect;
        [SerializeField] private ParticleSystem deathEffect;

        private bool alive;
        private Tween shakeTween;

        private void Start()
        {
            alive = true;
            
            aliveGraphics.SetActive(true);
            deadGraphics.SetActive(false);
        }

        public bool CanInteract()
        {
            return alive;
        }

        public void Hit()
        {
            if(!alive) return;
            shakeTween?.Kill();
            shakeTween = transform.DOShakeRotation(hitShakeDuration,hitShakeStrength);
            woodAmount -= Mathf.RoundToInt(Random.Range(woodPerHitRange.x, woodPerHitRange.y + 1));
            woodAmount = Mathf.Clamp(woodAmount,0,woodAmount);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            if (woodAmount <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            alive = false;
            aliveGraphics.SetActive(false);
            deadGraphics.SetActive(true);
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        public void StartInteract()
        {
            
        }

        public void StopInteract()
        {
            
        }
    }
}
