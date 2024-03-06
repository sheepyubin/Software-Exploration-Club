using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreContainer", menuName = "ScriptableObjects/ScoreContainer", order = 1)]
public class ScoreContainer : ScriptableObject
{
    public Dictionary<int, float> playerScore = new Dictionary<int, float>(); // 키(번호), 점수

    // 점수 추가
    public void AddScore(int id, float score)
    {
        playerScore[id] = score;
    }

    // 점수 반환
    public float ReturnScore(int id)
    {
        return playerScore[id];
    }
}
