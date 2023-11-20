using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
    public enum GameStates { greenTokenSession,greenSpinSession, redTokenSession, redSpinSession }
    [SerializeField]
    private GameStates currentState;
    #region Events
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnGreenTokenSessionStart = new UnityEvent();
    public static UnityEvent OnRedTokenSessionStart = new UnityEvent();
    public static UnityEvent OnGreenSpinSessionStart = new UnityEvent();
    public static UnityEvent OnRedSpinSessionStart = new UnityEvent();
    public static UnityEvent OnCheckStatus = new UnityEvent();
    public static GameStatusEvent OnGameStatusChange = new GameStatusEvent();
    #endregion
    public bool isGameStarted { get; private set; }
    private void OnEnable()
    {
        OnGameStart.AddListener(GameStart);
        OnGameStatusChange.AddListener(ChangeState);
        OnCheckStatus.AddListener(CheckStatus);
    }
    private void OnDisable()
    {
        OnGameStart.RemoveListener(GameStart);
        OnGameStatusChange.RemoveListener(ChangeState);
        OnCheckStatus.RemoveListener(CheckStatus);
    }
    private void CheckStatus()
    {
        if (currentState == GameStates.greenSpinSession)
            ChangeState(GameStates.redTokenSession);
        else if(currentState == GameStates.redSpinSession)
            ChangeState(GameStates.greenTokenSession);
    }
    private void GameStart()
    {
        isGameStarted = true;
        ChangeState(GameStates.greenTokenSession);
        Debug.Log("gameStarted!");

    }
    private void ChangeState(GameStates newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameStates.greenTokenSession:
                OnGreenTokenSessionStart.Invoke();
                break;
            case GameStates.greenSpinSession:
                OnGreenSpinSessionStart.Invoke();
                break;
            case GameStates.redTokenSession:
                OnRedTokenSessionStart.Invoke();
                break;
            case GameStates.redSpinSession:
                OnRedSpinSessionStart.Invoke();
                break;
            default:
                break;
        }
    }
    public GameStates GetCurrentState()
    {
        return currentState;
    }
}
public class GameStatusEvent : UnityEvent<GameManager.GameStates> { }