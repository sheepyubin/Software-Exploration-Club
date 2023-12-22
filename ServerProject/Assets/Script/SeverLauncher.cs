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
        // ���� ���� ����
        PhotonNetwork.ConnectUsingSettings();
    }

    // ���� ���� ���� �� �ݹ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
    }

    // ���� ���� �κ� ���� �� �ݹ�
    public override void OnJoinedRoom()
    {
        Debug.Log("Lobby entrance");

        PhotonNetwork.LoadLevel("InGame");
    }

    // �κ� ���� �� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Create lobby: " + LobbyName);
    }

    // �κ� ���� �ż��� (��ư UI)
    public void CreateAndJoinRoom()
    {
        Debug.Log("Create and enter");
        int randomNum = UnityEngine.Random.Range(1, 1000);
        string roomName = "Room " + randomNum; // �� �̸�: Room ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // �ִ� ���� �÷��̾� 4��
        LobbyName = roomName; // ���� ������ �κ� �̸� ����
    }

    // �κ� ���� �� ���� �ݹ�
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Update lobby list");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("Lobby name: " + room.Name + ", Number of players: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

    // �κ� ���� �ż��� (��ư UI)
    public void JoinRoom()
    {
        Debug.Log("Entrance" + LobbyName);
        PhotonNetwork.JoinRandomRoom();
    }

    // ���� �� �κ� �������� ���� �� �ݹ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room");
    }
}
