using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BGS.Task
{
    [RequireComponent(typeof(CanvasGroup))]
    public class UIFadeCanvasGroupTween : MonoBehaviour
    {
        [SerializeField][Range(0,1)] private float fadeTarget = 1f;
        [SerializeField][Range(0,1)] private float fadeDuration = 0.5f;
        
        [SerializeField] private Ease fadeEase = Ease.Linear;
        [SerializeField] private bool startVisible;
        
        [SerializeField] private UnityEvent OnShow;
        [SerializeField] private UnityEvent OnHide;
        
        private CanvasGroup cg;
        private Tween tween;

        private void Start()
        {
            cg = GetComponent<CanvasGroup>();
            Show(startVisible, startVisible);
        }

        public void Show(bool value, bool immediate = false)
        {
            if (!immediate)
            {
                tween?.Kill();
                tween = cg.DOFade(value ? 1 : 0, fadeDuration).SetEase(fadeEase);
            }
            else
            {
                cg.alpha = value ? fadeTarget : 0;
            }
            
            cg.interactable = value;
            cg.blocksRaycasts = value;
            
            if(value) OnShow?.Invoke();
            else OnHide?.Invoke();
        }
    }
}