using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]
public class PlayerContainer : ScriptableObject
{
    public GameObject[] playerPrefabs;
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
}
