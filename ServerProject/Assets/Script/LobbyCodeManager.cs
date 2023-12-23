using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCodeManager : MonoBehaviour
{
    SeverLauncher serverlaucher; // ���� ��ó ��ũ��Ʈ

    public string lobbyCode; // �κ� �ڵ�

    private int creationCount = 0; // ������Ʈ ���� Ƚ��
    private void Start()
    {
        if (creationCount == 0)
        {
            // DontDestroyOnLoad �ż��尡 �� ���� ȣ��ǰ� ��
            // ������ ȣ�� �� LobbyCodeManager�� �������� �þ�� �κ� �ڵ� ���׳�
            DontDestroy();
            creationCount++;
        }
        serverlaucher = GameObject.Find("SeverLauncher").GetComponent<SeverLauncher>(); // ���� ��ó ��ũ��Ʈ ����
    }

    private void Update()
    {
        lobbyCode = serverlaucher.lobbyCode;
    }

    private void DontDestroy() // DontDestroyOnLoad �ż��� ȣ�� �ż���
    {
        DontDestroyOnLoad(gameObject);
    }
}