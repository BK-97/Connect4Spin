using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCircle : MonoBehaviour
{
    private bool hasObject=false;
    private ConnectToken currentToken;

    public void TakeToken(ConnectToken takenToken)
    {
        currentToken = takenToken;
        hasObject = true;
    }
    public bool CheckIsAvailable()
    {
        return !hasObject;
    }
    public ConnectToken GetToken()
    {
        return currentToken;
    }
}
