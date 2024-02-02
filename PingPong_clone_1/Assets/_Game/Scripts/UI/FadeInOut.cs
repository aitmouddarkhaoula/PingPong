using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] private float _fadeTime = 1f; 
    private Vector3 _textScale;
    void Start()
    {
        _textScale =  GetComponent<RectTransform>().localScale;
        

    // Update is called once per frame
    void Update()
    {
        RectTransform rectTransform =GetComponent<RectTransform>();
        rectTransform.DOScale(_textScale*1.4f, _fadeTime).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
    }
    }
}
