using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{
    #region Params
    private Vector2 startTouchPos;
    private float startTime;
    private Vector2 endTouchPos;
    private float endTime;
    const float LEAST_SWIPE_DISTANCE = 300;
    const float LEAST_SWIPE_TIME = 1;
    const float SWIPE_POWER_STARTER = 1;
    #endregion
    #region Events
    public static FloatEvent OnSwipe = new FloatEvent();
    #endregion
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
            startTime = Time.time;
        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;
            endTime = Time.time;
            CalcualteSwipe();
        }
    }
    private void CalcualteSwipe()
    {
        float distance = startTouchPos.y - endTouchPos.y;
        if (Mathf.Abs(distance)< LEAST_SWIPE_DISTANCE)
        {
            Debug.Log("Swipe too short!");
            return;
        }
        float swipeTime = endTime - startTime;
        if (swipeTime > LEAST_SWIPE_TIME)
        {
            Debug.Log("Swipe too slow!");
            return;
        }
        float newPower = SWIPE_POWER_STARTER / swipeTime;
        if (distance > 0)
            newPower *= -1;
        Debug.Log(newPower);
        OnSwipe.Invoke(newPower);
    }
}
public class FloatEvent : UnityEvent<float> { }