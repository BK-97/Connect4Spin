using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenController : PlayerBase
{
    private void OnEnable()
    {
        GameManager.OnGreenTokenSessionStart.AddListener(ReadyForTokenPut);
        GameManager.OnGreenSpinSessionStart.AddListener(ReadyForSpin);
        InputManager.OnClickedCircle.AddListener(SetCircle);
        InputManager.OnClickedConnects.AddListener(SetConnects);
        InputManager.OnSwipe.AddListener(SpinConnects);
    }
    private void OnDisable()
    {
        GameManager.OnGreenTokenSessionStart.RemoveListener(ReadyForTokenPut);
        GameManager.OnGreenSpinSessionStart.RemoveListener(ReadyForSpin);
        InputManager.OnClickedCircle.RemoveListener(SetCircle);
        InputManager.OnClickedConnects.RemoveListener(SetConnects);
        InputManager.OnSwipe.RemoveListener(SpinConnects);
    }
    public override void TokenUse()
    {
        base.TokenUse();
        StartCoroutine(WaitForTokenArrive());
    }

    IEnumerator WaitForTokenArrive()
    {
        yield return new WaitForSeconds(TOKEN_MOVE_TIME + 0.1f);
        GameManager.OnGameStatusChange.Invoke(GameManager.GameStates.greenSpinSession);
    }
}
