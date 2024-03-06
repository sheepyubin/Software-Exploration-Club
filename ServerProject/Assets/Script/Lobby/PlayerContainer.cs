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
}