using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class OnOffUI : MonoBehaviour
{
    [SerializeField] bool _doesStartOn;

    [Header("Turning On")]
    [SerializeField] Vector2 _onPosition;
    [SerializeField] float _onTime;
    [SerializeField] Ease _onEase;

    [Header("Turning Off")]
    [SerializeField] Vector2 _offPosition;
    [SerializeField] float _offTime;
    [SerializeField] Ease _offEase;

    RectTransform _rect;

    // Start is called before the first frame update
    void Start()
    {
        _rect = GetComponent<RectTransform>();

        if (_doesStartOn)
            _rect.anchoredPosition = _onPosition;
        else
            _rect.anchoredPosition = _offPosition;

    }

    public void TurnOn()
    {
        DOTween.Kill(_rect);
        _rect.DOAnchorPos(_onPosition, _onTime).SetEase(_onEase);
    }

    public void TurnOff()
    {
        DOTween.Kill(_rect);
        _rect.DOAnchorPos(_offPosition, _offTime).SetEase(_offEase);
    }
}
