using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] Transform[] _disableOnWebGL;
    [SerializeField] Image _backgroundFade;
    [SerializeField] CanvasGroup _popupGroup;
    [SerializeField] RectTransform _popupRect;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            foreach (var tra in _disableOnWebGL)
                tra.gameObject.SetActive(false);
        }

        _backgroundFade.color = new Color(0, 0, 0, 0);
        _popupGroup.alpha = 0;
        _popupRect.anchoredPosition = new Vector2(0, -50);
        gameObject.SetActive(false);
    }

    public void ButtonPressContinue()
    {
        Disappear();
    }

    public void ButtonPressReset()
    {
        TransitionManager.LoadGameScene();
    }

    public void ButtonPressMenu()
    {
        TransitionManager.LoadTitleScene();
    }

    public void ButtonPressQuit()
    {
        if (Application.platform != RuntimePlatform.WebGLPlayer)
            Application.Quit();
    }

    public void Disappear()
    {
        _popupGroup.interactable = _popupGroup.blocksRaycasts = false;
        _backgroundFade.DOFade(0, 0.5f).SetEase(Ease.InSine);
        _popupGroup.DOFade(0, 0.5f).SetEase(Ease.InSine);
        _popupRect.DOAnchorPosY(-50, 0.5f).SetEase(Ease.InSine).OnComplete(OnDisappearComplete);
    }

    void OnDisappearComplete()
    {
        gameObject.SetActive(false);
    }

    public void Appear()
    {
        _popupGroup.interactable = _popupGroup.blocksRaycasts = true;
        gameObject.SetActive(true);
        _backgroundFade.color = new Color(0, 0, 0, 0);
        _popupGroup.alpha = 0;
        _popupRect.anchoredPosition = new Vector2(0, -50);

        _backgroundFade.DOFade(0.9f, 0.8f).SetEase(Ease.OutSine);
        _popupGroup.DOFade(1, 1f).SetEase(Ease.OutExpo).SetDelay(0.2f);
        _popupRect.DOAnchorPosY(0, 1f).SetEase(Ease.OutExpo).SetDelay(0.2f).OnComplete(OnAppearComplete);
    }

    void OnAppearComplete()
    {
        _popupGroup.interactable = _popupGroup.blocksRaycasts = true;
    }
}
