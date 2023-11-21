using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastChecker : MonoBehaviour
{
    public List<ConnectCircle> connectCirclesList;
    public void Raycast()
    {
        connectCirclesList.Clear();
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit[] hits = Physics.RaycastAll(ray, 15f);

        System.Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        foreach (var hit in hits)
        {
            ConnectCircle connectCircle = hit.collider.GetComponent<ConnectCircle>();

            if (connectCircle != null && !connectCirclesList.Contains(connectCircle))
            {
                connectCirclesList.Add(connectCircle);
            }
        }
        CheckForConnect();
    }
    private void CheckForConnect()
    {
        List<ConnectToken> tokenList = new List<ConnectToken>();
        for (int i = 0; i < connectCirclesList.Count; i++)
        {
            if (connectCirclesList[i].GetToken() != null)
                tokenList.Add(connectCirclesList[i].GetToken().GetComponent<ConnectToken>());
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
            Debug.Log("Winner= "+ _cachedTokenType);
    }
}
