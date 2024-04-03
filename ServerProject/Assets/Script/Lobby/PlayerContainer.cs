using UnityEngine;
using System.Collections.Generic;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "PlayerContainer", menuName = "ScriptableObjects/PlayerContainer", order = 1)]

public class PlayerContainer : ScriptableObject
{
    public GameObject playerPrefab; // �÷��̾� ������(����)
    public Dictionary<string, GameObject> playerObject = new Dictionary<string, GameObject>(); // playerID, �÷��̾� ������
    public Dictionary<string, Color> playerColor = new Dictionary<string, Color>(); // playerID, ��
    public Dictionary<string, int> playerScore = new Dictionary<string, int>(); // playerID, ����
    public Dictionary<string, bool> playerisDead = new Dictionary<string, bool>(); // playerID, �׾��°�?


    // �÷��̾� ������ �߰� �ż���
    public void AddPlayerData(string playerID, GameObject player) // (playerID, �÷��̾� ������)
    {
        playerObject[playerID] = player;
    }

    // �÷��̾� ������ ��ȯ �ż���
    public GameObject ReturnPlayerData(string playerID) // (playerID)
    {
        return playerObject[playerID];
    }

    // �÷��̾� ���� �߰� �ż���
    public void AddPlayerColor(string playerID, Color color) // (playerID, �÷��̾� ����)
    {
        playerColor[playerID] = color;
    }

    // �÷��̾� ���� ��ȯ �ż���
    public Color ReturnPlayerColor(string playerID) // (playerID)
    {
        if (playerColor.ContainsKey(playerID)) // playerColor ��ųʸ��� playerID�� Ű�� ���� ���� �ִٸ�
            return playerColor[playerID];
        else // playerColor ��ųʸ��� playerID�� Ű�� ���� ���� ���ٸ�
            return Color.white;
    }

    // �÷��̾� ���� �迭 ��ȯ �ż���
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

    // �÷��̾� ���� �߰� �ż���
    public void AddPlayerScore(string playerID, int score) // (playerID, ����)
    {
        playerScore[playerID] = score;
    }

    // �÷��̾� ���� ��ȯ �ż���
    public int ReturnPlayerScore(string playerID) // (playerID)
    {
        if (playerScore.ContainsKey(playerID)) // playerScore ��ųʸ��� playerID�� Ű�� ���� ���� �ִٸ�
            return playerScore[playerID];
        else // playerScore ��ųʸ��� playerID�� Ű�� ���� ���� ���ٸ�
            return -1;
    }

    // �÷��̾� ���� �迭 ��ȯ �ż���
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

    // �÷��̾� ��� �߰� �ż���
    public void AddPlayerisDead(string playerID, bool isDead) // (playerID, ���)
    {
        playerisDead[playerID] = isDead;
    }

    // �÷��̾� ��� ��ȯ �ż���
    public bool ReturnPlayerisDead(string playerID) // (playerID)
    {
        return playerisDead[playerID];
    }

    // �÷��̾� ��� ��ȯ �ż��� (���)
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
    
    // �����̳� ���� �ż���
    public void ResetContainer()
    {
        playerObject.Clear();
        playerColor.Clear();
        playerScore.Clear();
        playerisDead.Clear();
    }
}