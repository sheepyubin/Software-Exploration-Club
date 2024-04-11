using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoundData", menuName = "ScriptableObjects/RoundData", order = 1)]

public class RoundData : ScriptableObject
{
    private int round = 1;
    private float time ;
    private bool clear = false;
    private bool clearGame = false;

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

    public void AddClear(bool clear)
    {
        this.clear = clear; 
    }
    public bool ReturnClear()
    {
        return clear;
    }


    public void AddClearGame(bool clearGame)
    {
        this.clearGame = clearGame; 
    }

    public bool ReturnClearGame()
    {
        return clearGame;  
    }
}