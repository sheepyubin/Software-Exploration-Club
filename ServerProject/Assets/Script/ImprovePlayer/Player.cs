using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string ID; // ID
    private bool isDead; // �׾��°�?
    private int score; // ����

    // ID ������Ƽ
    public string PlayerID
    {
        get { return ID; }
        set { ID = value; }
    }

    // isDead ������Ƽ
    public bool IsDead
    {
        get { return isDead; }
        set { isDead = value; }
    }

    // score ������Ƽ
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    // ������
    public Player(string id, bool dead, int initialScore)
    {
        ID = id;
        isDead = dead;
        score = initialScore;
    }

    // isDead ����
    public void SetisDead(bool isdead)
    {
        this.isDead = isdead;
    }

    // Score ����
    public void SetScore(int newScore)
    {
        this.score += newScore;
    }

    public string ReturnID()
    {
        return ID;
    }
    public bool ReturnisDead()
    {
        return isDead;
    }
    public int Returnscore()
    {
        return score;
    }
}