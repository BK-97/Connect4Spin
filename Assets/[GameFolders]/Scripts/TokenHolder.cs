using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TokenHolder : MonoBehaviour
{
    public TokenType holderTokensType;
    public List<ConnecToken> tokens;
    private void Start()
    {
        for (int i = 0; i < tokens.Count; i++)
        {
            tokens[i].Initalize(holderTokensType);
        }
    }

    public void UseToken(Transform targetTransform,float moveTime)
    {
        tokens[tokens.Count - 1].Use(targetTransform, moveTime);
        tokens.RemoveAt(tokens.Count - 1);
    }
}
