using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string ID; // ID
    private bool isDead; // �׾��°�?
    private int score; // ����
    private Color color; // ����

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

    // color ������Ƽ
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    // ������
    public Player(string id, bool dead, int initialScore, Color color)
    {
        ID = id;
        isDead = dead;
        score = initialScore;
        this.color = color;
    }

    // isDead ����
    public void SetisDead(bool isdead)
    {
        this.isDead = isdead;
    }

    // Score ����
    public void SetScore(int newScore)
    {
        score += newScore;
    }

    // color ����
    public void SetColor(Color color)
    {
        this.color += color;
    }

    // ID ��ȯ
    public string ReturnID()
    {
        return ID;
    }

    // isDead ��ȯ
    public bool ReturnisDead()
    {
        return isDead;
    }

    // score ��ȯ
    public int Returnscore()
    {
        return score;
    }

    // color ��ȯ
    public Color Returncolor()
    {
        if (color == null)
        {
            Debug.Log("null");
        }
        return color;
    }
}