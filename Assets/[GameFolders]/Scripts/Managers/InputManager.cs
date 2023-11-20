using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputManager : Singleton<InputManager>
{
    #region Params
    public LayerMask connectCircleLayer;
    public LayerMask connectsLayer;

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
    public static ConnectsEvent OnClickedConnects = new ConnectsEvent();
    public static CircleEvent OnClickedCircle = new CircleEvent();
    #endregion
    private void Update()
    {
        if (!GameManager.Instance.isGameStarted)
        {
            if (Input.touchCount > 0)
                GameManager.OnGameStart.Invoke();
            return;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
            CheckRaycast(startTouchPos);
            startTime = Time.time;

        }
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPos = Input.GetTouch(0).position;
            endTime = Time.time;
            CalcualteSwipe();
        }
    }
    private void CheckRaycast(Vector3 touchPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity,connectsLayer))
        {
            Collider hitCollider= hit.collider;
            if (hitCollider != null)
            {
                ConnectController connectController = hitCollider.gameObject.GetComponent<ConnectController>();
                if (connectController != null)
                {
                    OnClickedConnects.Invoke(connectController);
                }
            }
        }
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, connectCircleLayer))
        {
            Collider hitCollider = hit.collider;
            if (hitCollider != null)
            {
                ConnectCircle connectCircle = hitCollider.gameObject.GetComponent<ConnectCircle>();
                if (connectCircle != null)
                {
                    OnClickedCircle.Invoke(connectCircle);
                }
            }
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
        OnSwipe.Invoke(newPower);
    }
}
public class FloatEvent : UnityEvent<float> { }
public class CircleEvent : UnityEvent<ConnectCircle> { }
public class ConnectsEvent : UnityEvent<ConnectController> { }