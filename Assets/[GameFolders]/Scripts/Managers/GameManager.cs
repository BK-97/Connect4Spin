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
    public bool isGameEnded { get; private set; }
    #region Events
    public static UnityEvent OnGameStart = new UnityEvent();
    public static UnityEvent OnGreenTokenSessionStart = new UnityEvent();
    public static UnityEvent OnRedTokenSessionStart = new UnityEvent();
    public static UnityEvent OnGreenSpinSessionStart = new UnityEvent();
    public static UnityEvent OnRedSpinSessionStart = new UnityEvent();
    public static UnityEvent OnCheckStatus = new UnityEvent();
    public static TokenTypeEvent OnGameEnd = new TokenTypeEvent();
    public static GameStatusEvent OnGameStatusChange = new GameStatusEvent();
    #endregion
    public bool isGameStarted { get; private set; }
    private void OnEnable()
    {
        OnGameStart.AddListener(GameStart);
        OnGameStatusChange.AddListener(ChangeState);
        OnGameEnd.AddListener(HandleGameEnd);
    }
    private void OnDisable()
    {
        OnGameStart.RemoveListener(GameStart);
        OnGameStatusChange.RemoveListener(ChangeState);
        OnGameEnd.RemoveListener(HandleGameEnd);
    }
    public void StatusChecked()
    {
        Debug.Log("Status Checked");

        if (currentState == GameStates.greenSpinSession)
            ChangeState(GameStates.redTokenSession);
        else if(currentState == GameStates.redSpinSession)
            ChangeState(GameStates.greenTokenSession);
    }
    private void HandleGameEnd(TokenType winnerType)
    {
        isGameEnded = true;
        isGameStarted = false;
        SceneManager.UnloadSceneAsync("Gameplay");
        StartCoroutine(WaitForNewGame());
    }
    IEnumerator WaitForNewGame()
    {
        yield return new WaitForSeconds(0.5f);
        yield return SceneManager.LoadSceneAsync("Gameplay", LoadSceneMode.Additive);
    }
    private void GameStart()
    {
        isGameEnded = false;
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
public class TokenTypeEvent : UnityEvent<TokenType> { }