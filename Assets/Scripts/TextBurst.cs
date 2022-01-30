using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TextBurst : W2C
{
    [SerializeField] float _appearTime;
    [SerializeField] float _lifetime;
    [SerializeField] float _vanishTime;

    [SerializeField] Ease _easeIn;
    [SerializeField] Text _text;

    public void Init(Vector3 position, string text)
    {
        _trackRect.localScale = Vector3.zero;
        _trackRect.DOScale(1, _appearTime).SetEase(_easeIn);
        _trackRect.localRotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(-25, 25)));

        _text.text = text;
        _text.DOFade(0, _vanishTime).SetDelay(_appearTime + _lifetime).OnComplete(() => Destroy(gameObject));
        SetPosition(position);
    }
}
