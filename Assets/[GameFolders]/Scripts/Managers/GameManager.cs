using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class GameManager : Singleton<GameManager>
{
    public enum GameStates { greenTokenSession,greenSpinSession, redTokenSession, redSpinSession }
    [SerializeField]
    private GameStates currentState;
    public bool isLevelFinished{ get; private set; }
    public bool isLevelStarted{ get; private set; }
    public bool isGameStarted{ get; private set; }
    #region Events
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnGameEnd = new UnityEvent();
    public static UnityEvent OnGameplaySceneLoaded = new UnityEvent();

    public static UnityEvent OnLevelStart = new UnityEvent();
    public static TokenTypeEvent OnLevelFinished = new TokenTypeEvent();

    public static UnityEvent OnGreenTokenSessionStart = new UnityEvent();
    public static UnityEvent OnRedTokenSessionStart = new UnityEvent();
    public static UnityEvent OnGreenSpinSessionStart = new UnityEvent();
    public static UnityEvent OnRedSpinSessionStart = new UnityEvent();
    public static UnityEvent OnCheckStatus = new UnityEvent();
    public static TokenTypeEvent OnFeedback = new TokenTypeEvent();
    public static GameStatusEvent OnGameStatusChange = new GameStatusEvent();

    #endregion
    private void OnEnable()
    {
        OnLevelStart.AddListener(LevelStarted);
        OnGameStart.AddListener(() => isGameStarted = true);
        OnGameStatusChange.AddListener(ChangeState);
        OnLevelFinished.AddListener(HandleGameEnd);
        OnGameEnd.AddListener(()=>StartCoroutine(StartNewGame()));

    }
    private void OnDisable()
    {
        OnLevelStart.RemoveListener(LevelStarted);
        OnGameStart.RemoveListener(() => isGameStarted = true);
        OnGameStatusChange.RemoveListener(ChangeState);
        OnLevelFinished.RemoveListener(HandleGameEnd);
        OnGameEnd.RemoveListener(() => StartCoroutine(StartNewGame()));
    }
    public void StatusChecked()
    {
        if (currentState == GameStates.greenSpinSession)
            ChangeState(GameStates.redTokenSession);
        else if(currentState == GameStates.redSpinSession)
            ChangeState(GameStates.greenTokenSession);
    }
    private void HandleGameEnd(TokenType winnerType)
    {
        OnFeedback.Invoke(winnerType);
        isLevelFinished = true;
    }
    IEnumerator StartNewGame()
    {
        yield return SceneManager.UnloadSceneAsync("Gameplay");
        isLevelStarted = false;
        yield return new WaitForSeconds(0.5f);
        yield return SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
        OnGameplaySceneLoaded.Invoke();
    }
    private void LevelStarted()
    {
        isLevelStarted = true;
        ChangeState(GameStates.greenTokenSession);
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
public class TokenTypeEvent : UnityEvent<TokenType> { }