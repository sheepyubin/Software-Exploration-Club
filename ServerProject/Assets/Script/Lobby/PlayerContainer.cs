using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab;
    public Dictionary<int, GameObject> PlayerData = new Dictionary<int, GameObject>(); // 키(번호), 플레이어 프리팹
    public Dictionary<int, Color> playerColor = new Dictionary<int, Color>(); // 키(번호), RGB
    public Dictionary<int, bool> isDead = new Dictionary<int, bool>(); // 키(번호), 죽었는가?
    public Dictionary<int, int> scoreData = new Dictionary<int, int>(); // 키(번호), 점수
    public Dictionary<int, int> playerScore = new Dictionary<int, int>(); // 키(번호), 점수

    public void AddPlayerData(int playerNumber, GameObject player)
    {
        PlayerData[playerNumber] = player;
    }

    public GameObject ReturnPlayerData(int playerNumber)
    {
        return PlayerData[playerNumber];
    }
    public void AddPlayerColor(int playerNumber, Color color)
    {
        playerColor[playerNumber] = color;
    }

    public Color ReturnPlayerColor(int playerNumber)
    {
        return playerColor[playerNumber];
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

    public void ResetScore(int playerNumber)
    {
        playerScore[playerNumber] = 0;
    }

    public void AddScore(int playerNumber, int score)
    {
        playerScore[playerNumber] += score;
    }
}