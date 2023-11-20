using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectCircle : MonoBehaviour
{
    private bool hasObject=false;
    private ConnecToken currentToken;

    public void TakeToken(ConnecToken takenToken)
    {
        currentToken = takenToken;
        hasObject = true;
    }
    public bool CheckIsAvailable()
    {
        Debug.Log(!hasObject);
        return !hasObject;
    }
}
