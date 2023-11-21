using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ConnectToken : MonoBehaviour
{
    private TokenType currentType=TokenType.None;
    public MeshRenderer renderer;
    public void Initalize(TokenType newType)
    {
        currentType = newType;
        switch (currentType)
        {
            case TokenType.Green:
                renderer.material.SetColor("_Color", Color.green);
                break;
            case TokenType.Red:
                renderer.material.SetColor("_Color", Color.red);
                break;
            default:
                break;
        }
    }
    public void Use(Transform targetTransform,float moveTime)
    {
        transform.SetParent(targetTransform);
        targetTransform.gameObject.GetComponent<ConnectCircle>().TakeToken(this);
        transform.DOLocalMove(Vector3.zero, moveTime).SetEase(Ease.Linear);
        transform.DOLocalRotate(new Vector3(360, 0f, 0f), moveTime, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear);
    }
    public void WinnerAnimation()
    {
        transform.DOLocalRotate(new Vector3(360f, 0f, 0f), 1f, RotateMode.FastBeyond360)
           .SetEase(Ease.Linear);
    }
    public TokenType GetInfo()
    {
        return currentType;
    }
}
