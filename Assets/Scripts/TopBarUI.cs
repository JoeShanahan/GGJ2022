using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TopBarUI : MonoBehaviour
{
    [SerializeField] Text _titleText;
    [SerializeField] Image[] _images;

    [Space(10)]
    [SerializeField] RectTransform _mainRect;
    [SerializeField] RectTransform _subRect;

    [Space(10)]
    [SerializeField] float _mainActiveY;
    [SerializeField] float _mainHiddenY;

    [Space(10)]
    [SerializeField] float _subActiveY;
    [SerializeField] float _subHiddenY;

    [Space(10)]
    [SerializeField] float _tweenTime;
    [SerializeField] Ease _activateEase;
    [SerializeField] Ease _deactivateEase;
    
    [Header("Sprites")]
    [SerializeField] Sprite _buildSprite;
    [SerializeField] Sprite _destroySprite;

    void Start()
    {
        _mainRect.anchoredPosition = new Vector2(_mainRect.anchoredPosition.x, _mainHiddenY);
        _subRect.anchoredPosition = new Vector2(_subRect.anchoredPosition.x, _subHiddenY);
    }

    public void SetMenuBuild()
    {
        _titleText.text = "BUILD MODE";
        _images[0].sprite = _buildSprite;
        _images[1].sprite = _buildSprite;

        MenuAppear();
    }

    public void SetMenuDestroy()
    {
        _titleText.text = "DESTROY MODE";
        _images[0].sprite = _destroySprite;
        _images[1].sprite = _destroySprite;

        MenuAppear();
    }

    public void BackToPlayMode()
    {
        MenuGoAway();
    }
    
    void MenuAppear()
    {
        DOTween.Kill(_mainRect);
        DOTween.Kill(_subRect);

        _mainRect.DOAnchorPosY(_mainActiveY, _tweenTime).SetEase(_activateEase);
        _subRect.DOAnchorPosY(_subActiveY, _tweenTime).SetEase(_activateEase);
    }

    public void MenuGoAway()
    {
        DOTween.Kill(_mainRect);
        DOTween.Kill(_subRect);

        _mainRect.DOAnchorPosY(_mainHiddenY, _tweenTime).SetEase(_deactivateEase);
        _subRect.DOAnchorPosY(_subHiddenY, _tweenTime).SetEase(_deactivateEase);
    }
}
