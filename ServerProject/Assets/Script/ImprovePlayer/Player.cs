using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string ID; // ID
    private bool isDead; // 죽었는가?
    private int score; // 점수

    // ID 프로퍼티
    public string PlayerID
    {
        get { return ID; }
        set { ID = value; }
    }

    // isDead 프로퍼티
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    // score 프로퍼티
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    // 생성자
    public Player(string id, bool dead, int initialScore)
    {
        ID = id;
        isDead = dead;
        score = initialScore;
    }

    // isDead 설정
    public void SetisDead(bool isdead)
    {
        this.isDead = isdead;
    }

    // Score 설정
    public void SetScore(int newScore)
    {
        this.score += newScore;
    }
}