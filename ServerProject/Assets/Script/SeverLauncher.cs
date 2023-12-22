using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.SceneManagement;

public class SeverLauncher : MonoBehaviourPunCallbacks
{
    private string LobbyName;

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
    public override void OnJoinedRoom()
    {
        Debug.Log("�κ� ����");

        PhotonNetwork.LoadLevel("InGame");
    }

    // �κ� ���� �� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("�κ� ����: " + LobbyName);
    }

    // �κ� ���� �ż��� (��ư UI)
    public void CreateAndJoinRoom()
    {
        Debug.Log("���� �� ����");
        int randomNum = UnityEngine.Random.Range(1, 1000);
        string roomName = "Room " + randomNum; // �� �̸�: Room ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // �ִ� ���� �÷��̾� 4��
        LobbyName = roomName; // ���� ������ �κ� �̸� ����
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
    public void JoinRoom()
    {
        Debug.Log("����" + LobbyName);
        PhotonNetwork.JoinRandomRoom();
    }

    // �κ� �������� ���� �� �ݹ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("�� ����");
    }
}
