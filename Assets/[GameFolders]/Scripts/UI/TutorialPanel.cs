using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TutorialPanel : PanelBase
{
    public Transform textObject;
    Tween scaleUpTween;
    private void OnEnable()
    {
        GameManager.OnGameStart.AddListener(ShowPanel);
        GameManager.OnLevelStart.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        GameManager.OnGameStart.RemoveListener(ShowPanel);
        GameManager.OnLevelStart.RemoveListener(HidePanel);
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        scaleUpTween = textObject.DOScale(Vector3.one * 1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }
    public override void HidePanel()
    {
        base.HidePanel();
        scaleUpTween.Kill();
    }
}
