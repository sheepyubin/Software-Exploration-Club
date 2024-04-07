using UnityEngine;
using System.Collections.Generic;
using Photon.Pun;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]

public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab; // ?��???? ??????(????)
    public Dictionary<string, GameObject> playerObject = new Dictionary<string, GameObject>(); // playerID, ?��???? ??????
    public Dictionary<string, Color> playerColor = new Dictionary<string, Color>(); // playerID, ??
    public Dictionary<string, int> playerScore = new Dictionary<string, int>(); // playerID, ??
    public Dictionary<string, bool> playerisDead = new Dictionary<string, bool>(); // playerID, ????��??

    // ?��???? ?????? ??? ?????
    public void AddPlayerData(string playerID, GameObject player) // (playerID, ?��???? ??????)
    {
        playerObject[playerID] = player;
    }

    // ?��???? ?????? ??? ?????
    public GameObject ReturnPlayerData(string playerID) // (playerID)
    {
        return playerObject[playerID];
    }

    // ?��???? ???? ??? ?????
    public void AddPlayerColor(string playerID, Color color) // (playerID, ?��???? ????)
    {
        playerColor[playerID] = color;
    }

    // ?��???? ???? ??? ?????
    public Color ReturnPlayerColor(string playerID) // (playerID)
    {
        if (playerColor.ContainsKey(playerID)) // playerColor ??????? playerID?? ??? ???? ???? ????
            return playerColor[playerID];
        else // playerColor ??????? playerID?? ??? ???? ???? ?????
            return Color.white;
    }

    // ?��???? ???? ?�� ??? ?????
    public Color[] ReturnPlayerColorArray() // index
    {
        Color[] colorsArray = new Color[playerColor.Count];
        int index = 0;

        foreach (var color in playerColor.Values)
        {
            colorsArray[index] = color;
            index++;
        }

        return colorsArray;
    }

    // public void AllIndex(string playerID)
    // {
    //     indexList.Add(scoreIndex[playerID]);

    // }

    // public void MergeIndex()
    // {
    //     scoreData.SortIndex(indexList);
    // }

    // public void SetScoreIndex(string playerID)
    // {
    //     int index = scoreData.Getindex();

    //     scoreIndex[playerID] = index;

    //     Debug.Log(scoreIndex[playerID]);
    // }

    // ?��???? ???? ??? ?????
    public void AddPlayerScore(string playerID, int score) // (playerID, ????)
    {
        if (playerScore.ContainsKey(playerID))
            playerScore[playerID] += score;
        else
            playerScore[playerID] = score;

        Debug.Log(playerID + ": " + playerScore[playerID]);
    }

    //?��???? ???? ??? ?????
    public int ReturnPlayerScore(string playerID) // (playerID)
    {
        if (playerScore.ContainsKey(playerID)) // playerScore ��ųʸ��� playerID�� Ű�� ���� ���� �ִٸ�
            return playerScore[playerID];
        else // playerScore ��ųʸ��� playerID�� Ű�� ���� ���� ���ٸ�
            return -1;
    }

    // ?��???? ??? ??? ?????
    public void AddPlayerisDead(string playerID, bool isDead) // (playerID, ???)
    {
        playerisDead[playerID] = isDead;
    }

    // ?��???? ??? ??? ?????
    public bool ReturnPlayerisDead(string playerID) // (playerID)
    {
        return playerisDead[playerID];
    }

    // ?��???? ??? ??? ????? (???)
    public bool ReturnPlayerisDeadAll()
    {
        foreach (var kvp in playerisDead)
        {
            if (!kvp.Value)
            {
                return false;
            }
        }
        return true;
    }
    
    // ??????? ???? ?????
    public void ResetContainer(string playerID)
    {
        // playerObject ???? playerID ??? ?? ????
        if (playerObject.ContainsKey(playerID))
            playerObject.Remove(playerID);
        // playerColor ???? playerID ??? ?? ????
        if (playerColor.ContainsKey(playerID))
            playerColor.Remove(playerID);
        // playerScore ???? playerID ??? ?? ????
         if (playerScore.ContainsKey(playerID))
              playerScore.Remove(playerID);
        // playerisDead ???? playerID ??? ?? ????
        if (playerisDead.ContainsKey(playerID))
            playerisDead.Remove(playerID);
    }

    public void PrintPlayerData()
    {
        foreach (var kvp in playerObject)
        {
            string playerID = kvp.Key;
            int score = playerScore.ContainsKey(playerID) ? playerScore[playerID] : 0;
            bool isDead = playerisDead.ContainsKey(playerID) ? playerisDead[playerID] : false;
            Color color = playerColor.ContainsKey(playerID) ? playerColor[playerID] : Color.white;

            Debug.Log("UserID: " + playerID + ": Score " + score + " isDead " + isDead + " Color " + color + "\n");
        }
    }
}