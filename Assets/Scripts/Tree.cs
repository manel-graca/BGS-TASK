using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
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

        [SerializeField] private Image hitInfoButton;
        
        private bool alive;
        private Tween shakeTween;
        private Tween hitInfoTween;
        private bool playerNear;

        private void Start()
        {
            alive = true;
            hitInfoButton.transform.DOScale(Vector3.zero, 0);

            aliveGraphics.SetActive(true);
            deadGraphics.SetActive(false);
        }

        public bool CanInteract()
        {
            return alive && playerNear;
        }

        public int Hit()
        {
            if(!alive) return 0;
            
            hitInfoTween?.Kill();
            hitInfoTween = hitInfoButton.DOColor(Color.gray, 0.05f).SetLoops(2, LoopType.Yoyo);
            
            shakeTween?.Kill();
            
            shakeTween = transform.DOShakeRotation(hitShakeDuration,hitShakeStrength);
            var hitAmount = Mathf.RoundToInt(Random.Range(woodPerHitRange.x, woodPerHitRange.y + 1));
            woodAmount -= Mathf.RoundToInt(hitAmount);
            woodAmount = Mathf.Clamp(woodAmount,0,woodAmount);
            
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            
            if (woodAmount <= 0)
            {
                Die();
            }
            
            return hitAmount;
        }

        public void Die()
        {
            alive = false;
            
            aliveGraphics.SetActive(false);
            deadGraphics.SetActive(true);
            
            hitInfoTween?.Kill();
            hitInfoTween = hitInfoButton.DOColor(Color.clear, 0.05f);
            Destroy(hitInfoButton.transform.GetChild(0).gameObject);
            
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        public void StartInteract()
        {
            hitInfoButton.transform.DOScale(Vector3.one, 0.1f);
        }

        public void StopInteract()
        {
            hitInfoButton.transform.DOScale(Vector3.zero, 0.1f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag("Player")) return;
            playerNear = true;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(!other.CompareTag("Player")) return;
            playerNear = false;
        }
    }
}
