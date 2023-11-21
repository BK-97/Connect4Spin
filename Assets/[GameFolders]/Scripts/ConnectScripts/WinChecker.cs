using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WinChecker : MonoBehaviour
{
    public RaycastChecker[] lineCheckers;
    [SerializeField]
    private List<ConnectCircle> row1 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> row2 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> row3 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> row4 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> diagonal1 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> diagonal2 = new List<ConnectCircle>();
    private void OnEnable()
    {
        GameManager.OnCheckStatus.AddListener(Calculate);
    }
    private void OnDisable()
    {
        GameManager.OnCheckStatus.RemoveListener(Calculate);
    }
    private void Calculate()
    {
        Debug.Log("CheckStatus");
        for (int i = 0; i < lineCheckers.Length; i++)
        {
            lineCheckers[i].Raycast();
        }
        CheckRowLines();
        CheckDiagonalLines();
    }
    private void CheckRowLines()
    {
        row1.Clear();
        row2.Clear();
        row3.Clear();
        row4.Clear();
        for (int i = 0; i < lineCheckers.Length; i++)
        {
            row1.Add(lineCheckers[i].connectCirclesList[0]);
            row2.Add(lineCheckers[i].connectCirclesList[1]);
            row3.Add(lineCheckers[i].connectCirclesList[2]);
            row4.Add(lineCheckers[i].connectCirclesList[3]);
        }
        CheckCircles(row1);
        CheckCircles(row2);
        CheckCircles(row3);
        CheckCircles(row4);
        CheckCircles(diagonal1);
        CheckCircles(diagonal2);
        StartCoroutine(WaitForChecks());
    }
    IEnumerator WaitForChecks()
    {
        yield return new WaitForSeconds(0.2f);
        if (!GameManager.Instance.isGameEnded)
            GameManager.Instance.StatusChecked();
    }
    private void CheckDiagonalLines()
    {
        diagonal1.Clear();
        diagonal2.Clear();
        for (int i = 0; i < lineCheckers.Length; i++)
        {
            diagonal1.Add(lineCheckers[i].connectCirclesList[i]);
            diagonal2.Add(lineCheckers[(lineCheckers.Length-1)-i].connectCirclesList[i]);
        }

        CheckCircles(diagonal1);
        CheckCircles(diagonal2);
    }
    private void CheckCircles(List<ConnectCircle> currentCircles)
    {
        List<ConnectToken> tokenList = new List<ConnectToken>();
        for (int i = 0; i < currentCircles.Count; i++)
        {
            if (currentCircles[i].GetToken() != null)
                tokenList.Add(currentCircles[i].GetToken().GetComponent<ConnectToken>());
        }
        if (tokenList.Count == 4)
            CheckTokens(tokenList);
    }
    private void CheckTokens(List<ConnectToken> tokenList)
    {
        TokenType _cachedTokenType = tokenList[0].GetInfo();
        bool status = true;
        for (int i = 0; i < tokenList.Count; i++)
        {
            if (tokenList[i].GetInfo() != _cachedTokenType)
                status = false;
        }
        if (status)
        {
            GameManager.OnGameEnd.Invoke(_cachedTokenType);
            for (int i = 0; i < tokenList.Count; i++)
            {
                tokenList[i].WinnerAnimation();
            }
            Debug.Log("Winner= " + _cachedTokenType);

        }
    }
}
