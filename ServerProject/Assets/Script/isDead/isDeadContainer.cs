using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "isDeadContainer", menuName = "ScriptableObjects/isDeadContainer", order = 1)]

public class isDeadContainer : ScriptableObject
{
    public int playerCount;
    public int deadPlayerCount;

    public void ResetContainer(int playerCount)
    {
        this.playerCount = playerCount;
        deadPlayerCount = 0;
    }

    public void AddisDead()
    {
        deadPlayerCount++;
    }
    
    public bool ReturnisAllDead()
    {
        if(deadPlayerCount >= playerCount)
            return true;
        else
            return false;
    }
}