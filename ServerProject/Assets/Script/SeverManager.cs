using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using Photon.Pun.Demo.PunBasics;

public class SeverManager : MonoBehaviourPunCallbacks
{
    private string currentLobbyName;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // ���� ���� ���� �� �ݹ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ����");
    }

    // ���� ���� �κ� ���� �� �ݹ�
    public override void OnJoinedLobby()
    {
        Debug.Log("�κ� ����");
    }

    // �κ� ���� �� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("�κ� ����: " + currentLobbyName);
    }

    // �κ� ���� �ż��� (��ư UI)
    public void CreateAndJoinRoom()
    {
        Debug.Log("CreateAndJoinRoom...");
        int randomNum = UnityEngine.Random.Range(1, 1000);
        string roomName = "Room " + randomNum; // �� �̸�: Room ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // �ִ� ���� �÷��̾� 4��
        currentLobbyName = roomName; // ���� ������ �κ� �̸� ����
    }

    // �κ� ���� �� ���� �ݹ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("�� ����� ���ŵǾ����ϴ�.");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("�κ� �̸�: " + room.Name + ", �÷��̾� ��: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }


    // �� ���� �ż��� (��ư UI)
    public void JoinCurrentRoom()
    {
        if (!string.IsNullOrEmpty(currentLobbyName))
        {
            Debug.Log("Joining Room: " + currentLobbyName);
            PhotonNetwork.JoinRoom(currentLobbyName);
        }
        else
        {
            Debug.Log("No lobby available to join.");
        }
    }
}
