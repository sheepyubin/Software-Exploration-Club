using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;
using Photon.Pun;

[CreateAssetMenu(fileName = "Score", menuName = "ScriptableObjects/Score", order = 1)]

public class Score : ScriptableObject
{
    public PlayerContainer playerContainer;

    public void AddDeadScore(int actorNumber)
    {
        playerContainer.AddScoreData(actorNumber, 0);

        Debug.Log(actorNumber + ": " + 0);
    }

    public void AddLiveScore(int actorNumber)
    {
        playerContainer.AddScoreData(actorNumber, 50);

        Debug.Log(actorNumber + ": " + 50);
    }

    public void AddSuccessScore(int actorNumber)
    {
        playerContainer.AddScoreData(actorNumber, 100);

        Debug.Log(actorNumber + ": " + 100);

        foreach (var item in playerContainer.playerNum)
        {
            AddLiveScore(item);
        }
    }
}
