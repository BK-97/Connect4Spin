using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WinChecker : MonoBehaviour
{
    public RaycastChecker[] lineCheckers;
    [SerializeField]
    private List<ConnectCircle> column1 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> column2 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> column3 = new List<ConnectCircle>();
    [SerializeField]
    private List<ConnectCircle> column4 = new List<ConnectCircle>();
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
        StartCoroutine(WaitForChecks());
        for (int i = 0; i < lineCheckers.Length; i++)
        {
            lineCheckers[i].Raycast();
        }
        CheckRowLines();
        CheckDiagonalLines();

    }
    private void CheckRowLines()
    {
        column1.Clear();
        column2.Clear();
        column3.Clear();
        column4.Clear();
        for (int i = 0; i < lineCheckers.Length; i++)
        {
            column1.Add(lineCheckers[i].connectCirclesList[0]);
            column2.Add(lineCheckers[i].connectCirclesList[1]);
            column3.Add(lineCheckers[i].connectCirclesList[2]);
            column4.Add(lineCheckers[i].connectCirclesList[3]);
        }
        CheckCircles(column1);
        CheckCircles(column2);
        CheckCircles(column3);
        CheckCircles(column4);
        CheckCircles(diagonal1);
        CheckCircles(diagonal2);
    }
    IEnumerator WaitForChecks()
    {
        yield return new WaitForSeconds(0.2f);
        if (!GameManager.Instance.isLevelFinished)
        {
            Debug.Log("StatusChecked");
            GameManager.Instance.StatusChecked();
        }
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
            GameManager.OnLevelFinished.Invoke(_cachedTokenType);
            for (int i = 0; i < tokenList.Count; i++)
            {
                tokenList[i].WinnerAnimation();
            }
        }
    }
}
