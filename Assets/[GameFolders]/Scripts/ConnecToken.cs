using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ConnecToken : MonoBehaviour
{
    [HideInInspector]
    public TokenType currentType=TokenType.None;
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
        transform.DOLocalMove(Vector3.zero, moveTime).SetEase(Ease.Linear);
        transform.DOLocalRotate(Vector3.zero, moveTime).SetEase(Ease.Linear);
        //spinAroundAnimation
    }
}
