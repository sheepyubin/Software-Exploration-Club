using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab;
    public Dictionary<int, Color> playerData = new Dictionary<int, Color>(); // 키(번호), RGB
    public Dictionary<int, bool> isDead = new Dictionary<int, bool>(); // 키(번호), 죽었는가?
    public Dictionary<int, int> scoreData = new Dictionary<int, int>(); // 키(번호), 점수
    public List<int> playerNum = new List<int>(); // 플레이어 번호

    public void AddPlayerData(int playerNumber, Color playerColor)
    {
        playerData[playerNumber] = playerColor;
    }

    public Color ReturnPlayerColor(int playerNumber)
    {
        return playerData[playerNumber];
    }

    public void AddisDead(int playerNumber, bool dead)
    {
        isDead[playerNumber] = dead;
    }

    public bool ReturnisDead(int playerNumber)
    {
        return isDead[playerNumber];
    }

    public bool CheckAllDead()
    {
        foreach (var kvp in isDead)
        {
            if (!kvp.Value)
            {
                return false;
            }
        }

        return true;
    }

    public void AddScoreData(int playerNumber, int score)
    {
        scoreData[playerNumber] = score;
    }

    public int ReturnScoreData(int playerNumber)
    {
        return scoreData[playerNumber];
    }

    public void AddNum()
    {
        foreach (var kvp in playerData)
        {
            playerNum.Add(kvp.Key);
        }
    }
    public void DelNum(int Num)
    {
        playerNum.Remove(Num);
    }
}