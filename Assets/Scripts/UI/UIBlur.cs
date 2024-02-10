using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIBlur : MonoBehaviour
{
    [SerializeField]
    private Image blur;
    
    [SerializeField][Range(0,20)]
    private float targetSize = 12.5f;
    
    [SerializeField][Range(0.01f,2f)]
    private float duration;
    
    [SerializeField][Range(0.01f,1f)]
    private float delay;
    
    [SerializeField]
    private Ease ease;

    protected readonly int Size = Shader.PropertyToID("_Size");


    public void Show(bool show)
    {
        var mat = new Material(blur.material);
        blur.material = mat;
        
        var initialSize = show ? 0 : targetSize;
        var f = initialSize;
        DOTween.To(() => f, x => f = x, show ? targetSize : 0, duration)
            .SetEase(ease)
            .SetDelay(delay)
            .OnUpdate(() => { mat.SetFloat(Size, f); });
    }
}
