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
        // 포톤 서버 접속
        PhotonNetwork.ConnectUsingSettings();
    }

    // 포톤 서버 연결 시 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
    }

    // 포톤 서버 로비 입장 시 콜백
    public override void OnJoinedRoom()
    {
        Debug.Log("Lobby entrance");

        PhotonNetwork.LoadLevel("InGame");
    }

    // 로비 생성 시 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("Create lobby: " + LobbyName);
    }

    // 로비 생성 매서드 (버튼 UI)
    public void CreateAndJoinRoom()
    {
        Debug.Log("Create and enter");
        int randomNum = UnityEngine.Random.Range(1, 1000);
        string roomName = "Room " + randomNum; // 방 이름: Room 숫자
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // 최대 접속 플레이어 4명
        LobbyName = roomName; // 현재 생성된 로비 이름 저장
    }

    // 로비 생성 시 갱신 콜백
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("Update lobby list");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("Lobby name: " + room.Name + ", Number of players: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

    // 로비 입장 매서드 (버튼 UI)
    public void JoinRoom()
    {
        Debug.Log("Entrance" + LobbyName);
        PhotonNetwork.JoinRandomRoom();
    }

    // 참가 할 로비가 존재하지 않을 때 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No room");
    }
}
