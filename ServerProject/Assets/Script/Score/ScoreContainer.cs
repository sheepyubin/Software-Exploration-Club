using UnityEngine;
using System;

[CreateAssetMenu(fileName = "ScoreContainer", menuName = "ScriptableObjects/ScoreContainer", order = 1)]
public class ScoreContainer : ScriptableObject
{
    private int stageScore;

    // 점수 변경 이벤트를 위한 델리게이트 선언
    public event Action<int> OnScoreChanged;

    public void ResetScore()
    {
        stageScore = 0;
        NotifyScoreChanged(); // 점수가 초기화되었음을 옵저버에게 통지
    }

    public void AddScore(int score)
    {
        stageScore += score;
        NotifyScoreChanged(); // 점수가 변경되었음을 옵저버에게 통지
    }

    public int ReturnScore()
    {
        return stageScore;
    }

    // 점수 변경을 옵저버에게 통지하는 메서드
    private void NotifyScoreChanged()
    {
        OnScoreChanged?.Invoke(stageScore);
    }
}
