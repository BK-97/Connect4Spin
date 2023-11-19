using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ConnectController : MonoBehaviour
{
    const int SPIN_PER_BASE=1;
    const float SPIN_PER_TIME = 0.4f;
    [SerializeField]
    private AnimationCurve OutBounce;
    private void OnEnable()
    {
        InputManager.OnSwipe.AddListener(Spin);
    }
    private void OnDisable()
    {
        InputManager.OnSwipe.RemoveListener(Spin);
    }
    private void Spin(float spinPower)
    {
        int spinCount = SPIN_PER_BASE * ((int)spinPower);
        float spinTime = Mathf.Abs(spinPower)* SPIN_PER_TIME;
        transform.DORotate(new Vector3(180f * spinCount,0 , 0), spinTime, RotateMode.FastBeyond360)
            .SetEase(OutBounce)
            .OnComplete(() => Debug.Log("Dönüþ tamamlandý"));
    }
}
