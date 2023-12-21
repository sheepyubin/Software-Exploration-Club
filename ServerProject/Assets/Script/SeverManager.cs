using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;

public class SeverManager : MonoBehaviourPunCallbacks
{
    private string currentLobbyName;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // 포톤 서버 연결 시 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결");

        // 포톤 서버 로비 입장
        PhotonNetwork.JoinLobby();
    }

    // 포톤 서버 로비 입장 시 콜백
    public override void OnJoinedLobby()
    {
        Debug.Log("로비 입장");
    }

    // 로비 생성 매서드 (버튼 UI)
    public void CreateAndJoinRoom()
    {
        Debug.Log("CreateAndJoinRoom");
        int randomNum = UnityEngine.Random.Range(1, 1000);
        string roomName = "Room " + randomNum; // 방 이름: Room 숫자
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // 최대 접속 플레이어 4명
        currentLobbyName = roomName; // 현재 생성된 로비 이름 저장
    }

    // 로비 생성 시 갱신 콜백
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        // 로비 생성 갱신
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("로비 이름: " + room.Name + ", 플레이어 수: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

    // 방 입장 매서드 (버튼 UI)
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
