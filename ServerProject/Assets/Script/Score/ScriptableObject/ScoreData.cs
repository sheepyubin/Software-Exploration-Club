using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScoreData", menuName = "ScriptableObjects/ScoreData", order = 1)]

public class ScoreData : ScriptableObject
{
    // 점수 리스트
    private List<int> scoreList = new List<int>();
    public int index = 0;
    public int[] indexArray;
    public int[] scoreArray;

    // 인덱스 지정
    public int Getindex()
    {
        return index++;
    }
    
    public void SortIndex(List<int> index)
    {
        indexArray = index.ToArray();

        Array.Sort(indexArray);

        PairIndexScore();
    }

    public void PairIndexScore()
    {
        for(int i=0; i<indexArray.Length; i++)
        {
            scoreArray[i] = scoreList[indexArray[i]];
        }
    }

    // 점수 추가
    public void AddScore(int index, int score)
    {
        // 리스트의 크기가 해당 인덱스보다 작으면 리스트를 확장하여 초기화합니다.
        while (scoreList.Count <= index)
        {
            scoreList.Add(0);
        }

        // 해당 인덱스의 값이 0이 아닌 경우 이미 값이 존재
        if (scoreList[index] != 0)
        {
            ModifyScoreAtIndex(index,score);
            Debug.Log("Score " + score + " added at index " + index);            
        }
        else
        {
            // 값이 없으면 해당 인덱스에 값을 할당
            scoreList[index] = score;
            Debug.Log("Score " + score + " added at index " + index);
        }

    }

    // 점수 반환
    public int GetScoreAtIndex(int index)
    {
        // 유효한 인덱스라면
        if (index >= 0 && index < scoreList.Count)
        {
            // 반환
            return scoreList[index];
        }
        else // 유효한 인덱스가 아니라면
        {
            // -1 반환
            return -1;
        }
    }

    // 점수 수정
    public void ModifyScoreAtIndex(int index, int newScore)
    {
        // 유효한 인덱스라면
        if (index >= 0 && index < scoreList.Count)
        {
            // 점수 수정
            scoreList[index] = newScore;
        }
        else
        {
            // 유효하지 않은 인덱스일 경우 예외 처리 또는 무시
            Debug.LogWarning("Invalid index!");
        }
    }

    // 리스트 출력
    public void PrintScoreList()
    {
        Debug.Log("Score List:");
        foreach (int score in scoreList)
        {
            Debug.Log(score);
        }
    }

    // 리스트 초기화
    public void ClearScoreList()
    {
        scoreList.Clear();
    }

    public void ResetData()
    {
        index = 0;

        for (int i = 0; i < indexArray.Length; i++)
        {
            indexArray[i] = 0;
        }

        for (int i = 0; i < scoreArray.Length; i++)
        {
            scoreArray[i] = 0;
        }
    }
}
