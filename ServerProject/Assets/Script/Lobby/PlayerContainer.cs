using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab;
    public Dictionary<int, Color> playerData = new Dictionary<int, Color>(); // Å°(¹øÈ£), RGB
    public int index = 0;

    public void AddPlayerData(int playerNumber, Color playerColor)
    {
        playerData[playerNumber] = playerColor;
    }

    public Color ReturnPlayerColor(int playerNumber)
    {
        return playerData[playerNumber];
    }
}
