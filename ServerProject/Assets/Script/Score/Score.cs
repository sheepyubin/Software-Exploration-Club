using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using Photon.Pun;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]

public class Score : ScriptableObject
{
    public PlayerContainer playerContainer;

    private const int deadScore = 0;
    private const int liveScore = 50;
    private const int SuccessScore = 100;
    public void AddDeadScore(string playerID)
    {
        playerContainer.AddScore(playerID, deadScore);

        Debug.Log("In AddDeadScore '+ 0'  Score - " + playerID + ": " + playerContainer.playerScore[playerID]);
    }

    public void AddLiveScore(string playerID)
    {
        playerContainer.AddScore(playerID, liveScore);

        Debug.Log("In AddLiveScore '+ 50'  Score - " + playerID + ": " + playerContainer.playerScore[playerID]);
    }

    public void AddSuccessScore(string playerID)
    {
        playerContainer.AddScore(playerID, SuccessScore);

        Debug.Log("In AddSuccessScore '+ 100' Score - " + playerID + ": " + playerContainer.playerScore[playerID]);
    }
}
