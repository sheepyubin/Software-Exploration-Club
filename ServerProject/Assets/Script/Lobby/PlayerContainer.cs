using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab;
    public Dictionary<string, GameObject> PlayerData = new Dictionary<string, GameObject>(); // 키(번호), 플레이어 프리팹
    public Dictionary<string, bool> isDead = new Dictionary<string, bool>(); // 키(번호), 죽었는가?
    public Dictionary<string, int> scoreData = new Dictionary<string, int>(); // 키(번호), 점수
    public Dictionary<string, int> playerScore = new Dictionary<string, int>(); // 키(번호), 점수

    public void AddPlayerData(string playerID, GameObject player)
    {
        PlayerData[playerID] = player;
    }

    public GameObject ReturnPlayerData(string playerID)
    {
        return PlayerData[playerID];
    }
    public void AddisDead(string playerID, bool dead)
    {
        isDead[playerID] = dead;
    }

    public bool ReturnisDead(string playerID)
    {
        return isDead[playerID];
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

    public void ResetScore(string playerID)
    {
        playerScore[playerID] = 0;
    }

    public void AddScore(string playerID, int score)
    {
        playerScore[playerID] += score;
    }
}