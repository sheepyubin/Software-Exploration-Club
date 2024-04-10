using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]

public class RoundData : ScriptableObject
{
    private int round = 1;
    private float time = 0.00f;

    private int aliveMan = 0;
    private int deadMan = 0;

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


    public void AddLive(int aliveMan)
    {
        this.aliveMan = aliveMan;
    }
    public int ReturnLive()
    {
        return aliveMan;
        this.aliveMan = 0;
    }


    public void AddDead(int deadMan)
    {
        this.deadMan = deadMan;
    }
    public int ReturnDead()
    {
        return deadMan;
        this.deadMan = 0;
    }

}