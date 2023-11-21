using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class CutOutPanel : PanelBase
{
    [SerializeField]
    private float CutOutDelay = 2;
    public Image cutOutMaskTransform;
    private void OnEnable()
    {
        GameManager.OnLevelFinished.AddListener((TokenType winner) =>StartCoroutine(WaitForCutOut(winner)));
        GameManager.OnGameplaySceneLoaded.AddListener(ShowPanel);
    }
    private void OnDisable()
    {
        GameManager.OnLevelFinished.RemoveListener((TokenType winner) => StartCoroutine(WaitForCutOut(winner)));
        GameManager.OnGameplaySceneLoaded.RemoveListener(ShowPanel);
    }
    IEnumerator WaitForCutOut(TokenType winner)
    {
        yield return new WaitForSeconds(CutOutDelay);
        HidePanel();
    }
    public override void ShowPanel()
    {
        base.ShowPanel();
        cutOutMaskTransform.rectTransform.sizeDelta = new Vector2(0f, 0f);

        cutOutMaskTransform.rectTransform.DOSizeDelta(new Vector2(2000, 2000), 1)
            .SetEase(Ease.Linear).OnComplete(()=>GameManager.OnGameStart.Invoke());
    }
    public override void HidePanel()
    {
        cutOutMaskTransform.rectTransform.sizeDelta = new Vector2(2000, 2000f);

        cutOutMaskTransform.rectTransform.DOSizeDelta(new Vector2(0, 0), 1)
            .SetEase(Ease.Linear).OnComplete(()=>  GameManager.OnGameEnd.Invoke());
    }
}
