using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private bool canUseToken;
    private bool canSpinConnects;
    [SerializeField]
    private ConnectCircle currentCircle;
    [SerializeField]
    private ConnectController currentConnects;
    public TokenHolder TokenHolder;
    public const float TOKEN_MOVE_TIME = 0.5f;
    public virtual void ReadyForTokenPut()
    {
        canUseToken = true;
    }
    public virtual void ReadyForSpin()
    {
        canSpinConnects = true;
    }
    public virtual void SetCircle(ConnectCircle clickedCircle)
    {
        if (!canUseToken)
            return;
        currentCircle = clickedCircle;
        TokenUse();
    }
    public virtual void SetConnects(ConnectController clickedConnects)
    {
        if (!canSpinConnects)
            return;
        currentConnects = clickedConnects;
    }
    public virtual void TokenUse()
    {
        if (!canUseToken)
            return;
        if (currentCircle == null)
            return;
        if (!currentCircle.CheckIsAvailable())
            return;
        canUseToken = false;
        TokenHolder.UseToken(currentCircle.gameObject.transform, TOKEN_MOVE_TIME);
        currentCircle = null;
    }

    public virtual void SpinConnects(float spinPower)
    {
        if (!canSpinConnects)
            return;
        if (currentConnects == null)
            return;

        canSpinConnects = false;
        currentConnects.Spin(spinPower);
        currentConnects = null;
    }
}
