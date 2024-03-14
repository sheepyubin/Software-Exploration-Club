using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string ID; // ID
    private bool isDead; // 죽었는가?
    private int score; // 점수
    private Color color; // 색상

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

    // color 프로퍼티
    public Color Color
    {
        get { return color; }
        set { color = value; }
    }

    // 생성자
    public Player(string id, bool dead, int initialScore, Color color)
    {
        ID = id;
        isDead = dead;
        score = initialScore;
        this.color = color;
    }

    // isDead 설정
    public void SetisDead(bool isdead)
    {
        this.isDead = isdead;
    }

    // Score 설정
    public void SetScore(int newScore)
    {
        score += newScore;
    }

    // color 설정
    public void SetColor(Color color)
    {
        this.color += color;
    }

    // ID 반환
    public string ReturnID()
    {
        return ID;
    }

    // isDead 반환
    public bool ReturnisDead()
    {
        return isDead;
    }

    // score 반환
    public int Returnscore()
    {
        return score;
    }

    // color 반환
    public Color Returncolor()
    {
        if (color == null)
        {
            Debug.Log("null");
        }
        return color;
    }
}