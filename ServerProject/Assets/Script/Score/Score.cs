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
    public void AddDeadScore(int actorNumber)
    {
        playerContainer.AddScore(actorNumber, deadScore);

        Debug.Log("In AddDeadScore '+ 0'  Score - " + actorNumber + ": " + playerContainer.playerScore[actorNumber]);
    }

    public void AddLiveScore(int actorNumber)
    {
        playerContainer.AddScore(actorNumber, liveScore);

        Debug.Log("In AddLiveScore '+ 50'  Score - " + actorNumber + ": " + playerContainer.playerScore[actorNumber]);
    }

    public void AddSuccessScore(int actorNumber)
    {
        playerContainer.AddScore(actorNumber, SuccessScore);

        Debug.Log("In AddSuccessScore '+ 100' Score - " + actorNumber + ": " + playerContainer.playerScore[actorNumber]);
    }
}
