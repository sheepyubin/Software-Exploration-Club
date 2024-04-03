using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]

public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab; // 플레이어 프리팹(원본)
    public Dictionary<string, GameObject> playerObject = new Dictionary<string, GameObject>(); // playerID, 플레이어 프리팹
    public Dictionary<string, Color> playerColor = new Dictionary<string, Color>(); // playerID, 색
    public Dictionary<string, int> playerScore = new Dictionary<string, int>(); // playerID, 점수
    public Dictionary<string, bool> playerisDead = new Dictionary<string, bool>(); // playerID, 죽었는가?


    // 플레이어 데이터 추가 매서드
    public void AddPlayerData(string playerID, GameObject player) // (playerID, 플레이어 프리팹)
    {
        playerObject[playerID] = player;
    }

    // 플레이어 데이터 반환 매서드
    public GameObject ReturnPlayerData(string playerID) // (playerID)
    {
        return playerObject[playerID];
    }

    // 플레이어 색상 추가 매서드
    public void AddPlayerColor(string playerID, Color color) // (playerID, 플레이어 색상)
    {
        playerColor[playerID] = color;
    }

    // 플레이어 색상 반환 매서드
    public Color ReturnPlayerColor(string playerID) // (playerID)
    {
        if (playerColor.ContainsKey(playerID)) // playerColor 딕셔너리에 playerID를 키로 가진 값이 있다면
            return playerColor[playerID];
        else // playerColor 딕셔너리에 playerID를 키로 가진 값이 없다면
            return Color.white;
    }

    // 플레이어 색상 배열 반환 매서드
    public Color ReturnPlayerColorArray(int i) // index
    {
        Color[] scoresArray = new Color[playerColor.Count];
        int index = 0;

        foreach (var color in playerColor.Values)
        {
            scoresArray[index] = color;
            index++;
        }

        if (i >= 0 && i < scoresArray.Length)
            return scoresArray[i];
        else
            return Color.white;
    }

    // 플레이어 점수 추가 매서드
    public void AddPlayerScore(string playerID, int score) // (playerID, 점수)
    {
        playerScore[playerID] = score;
    }

    // 플레이어 점수 반환 매서드
    public int ReturnPlayerScore(string playerID) // (playerID)
    {
        if (playerScore.ContainsKey(playerID)) // playerScore 딕셔너리에 playerID를 키로 가진 값이 있다면
            return playerScore[playerID];
        else // playerScore 딕셔너리에 playerID를 키로 가진 값이 없다면
            return -1;
    }

    // 플레이어 점수 배열 반환 매서드
    public int ReturnPlayerScoreArray(int i) // index
    {
        int[] scoresArray = new int[playerScore.Count];
        int index = 0;

        foreach (var score in playerScore.Values)
        {
            scoresArray[index] = score;
            index++;
        }

        if (i >= 0 && i < scoresArray.Length)
        {
            return scoresArray[i];
        }
        else
        {
            return 0;
        }
    }

    // 플레이어 사망 추가 매서드
    public void AddPlayerisDead(string playerID, bool isDead) // (playerID, 사망)
    {
        playerisDead[playerID] = isDead;
    }

    // 플레이어 사망 반환 매서드
    public bool ReturnPlayerisDead(string playerID) // (playerID)
    {
        return playerisDead[playerID];
    }

    // 플레이어 사망 반환 매서드 (모두)
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
    
    // 컨테이너 리셋 매서드
    public void ResetContainer()
    {
        playerObject.Clear();
        playerColor.Clear();
        playerScore.Clear();
        playerisDead.Clear();
    }
}