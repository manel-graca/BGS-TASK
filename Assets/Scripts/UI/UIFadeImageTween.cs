using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BGS.Task
{
    [RequireComponent(typeof(Image))]
    public class UIFadeImageTween : MonoBehaviour
    {
        [SerializeField][Range(0,1)] private float fadeTarget = 1f;
        [SerializeField][Range(0,1)] private float fadeDuration = 0.5f;
        [SerializeField] private Ease fadeEase = Ease.Linear;
        
        
        private Image image;
        private Tween tween;

        private void Start()
        {
            image = GetComponent<Image>();
            Show(false);
        }

        public void Show(bool value, bool immediate = false)
        {
            if (!immediate)
            {
                tween?.Kill();
                tween = image.DOFade(value ? 1 : 0, fadeDuration).SetEase(fadeEase);
            }
            else
            {
                image.color = new Color(image.color.r, image.color.g, image.color.b, value ? fadeTarget : 0);
            }
        }
    }
}
