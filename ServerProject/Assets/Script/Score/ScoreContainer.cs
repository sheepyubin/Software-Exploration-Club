using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreContainer", menuName = "ScriptableObjects/ScoreContainer", order = 1)]
public class ScoreContainer : ScriptableObject
{
    public Dictionary<int, float> playerScore = new Dictionary<int, float>(); // Ű(��ȣ), ����

    // ���� �߰�
    public void AddScore(int id, float score)
    {
        playerScore[id] = score;
    }

    // ���� ��ȯ
    public float ReturnScore(int id)
    {
        return playerScore[id];
    }
}
