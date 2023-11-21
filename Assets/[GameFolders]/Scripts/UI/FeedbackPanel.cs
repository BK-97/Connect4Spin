using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
public class FeedbackPanel : PanelBase
{
    public TextMeshProUGUI feedbackText;
    Tween punchTween;
    private void Awake()
    {
        HidePanel();
    }
    private void OnEnable()
    {
        GameManager.OnFeedback.AddListener(WinPanel);
        GameManager.OnGameEnd.AddListener(HidePanel);
    }
    private void OnDisable()
    {
        GameManager.OnFeedback.RemoveListener(WinPanel);
        GameManager.OnGameEnd.RemoveListener(HidePanel);
    }
    private void WinPanel(TokenType winnerType)
    {
        switch (winnerType)
        {
            case TokenType.Green:
                feedbackText.text = "Green" + " is the Winner!";
                feedbackText.color = Color.green;
                break;
            case TokenType.Red:
                feedbackText.text = "Red" + " is the Winner!";
                feedbackText.color = Color.red;
                break;
            case TokenType.None:
                feedbackText.text = "Draw!";
                feedbackText.color = Color.white;
                break;
            default:
                break;
        }
        ShowPanel();
        punchTween = feedbackText.gameObject.transform.DOScale(Vector3.one * 1.1f, 0.5f).SetEase(Ease.Linear).SetLoops(-1,LoopType.Yoyo);
    }
}
