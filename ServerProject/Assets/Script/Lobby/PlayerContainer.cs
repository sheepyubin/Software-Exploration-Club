using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject[] playerPrefabs;
    public Dictionary<int, int> playerData = new Dictionary<int, int>(); // ÇÁ¸®ÆÕ ¹è¿­ ÀÎµ¦½º, ¹øÈ£
    public int count = 0;

    public int GetIndex()
    {
        count++;
        return count-1;
    }

    public void ResetIndex()
    {
        count = 0;
    }

    public void RestoreNum(int index, int num)
    {
        playerData[num] = index;
    }

    public int GetNum(int num)
    {
        return playerData[num];
    }
}
