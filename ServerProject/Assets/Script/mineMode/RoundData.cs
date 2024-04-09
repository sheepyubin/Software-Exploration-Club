using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]

public class RoundData : ScriptableObject
{
    private int round = 1;
    private float time = 0f;

    public void AddRound(int round)
    {
        this.round = round;
    }

    public int ReturnRound()
    {
        return round;
    }

    public void AddTime(float time)
    {
        this.time = time;
    } 
    
    public float ReturnTime()
    {
        return time;
    }
}