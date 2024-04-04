using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]

public class RoundData : ScriptableObject
{
    private int round = 1;

    public void AddRound(int round)
    {
        this.round = round;
    }

    public int ReturnRound()
    {
        return round;
    }
}