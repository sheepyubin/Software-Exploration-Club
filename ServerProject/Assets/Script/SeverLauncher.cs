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

    // 포톤 서버 연결 시 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("서버 연결");
    }

    // 포톤 서버 로비 입장 시 콜백
    public override void OnJoinedRoom()
    {
        Debug.Log("로비 입장");

        PhotonNetwork.LoadLevel("InGame");
    }

    // 로비 생성 시 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("로비 생성: " + LobbyName);
    }

    // 로비 생성 매서드 (버튼 UI)
    public void CreateAndJoinRoom()
    {
        Debug.Log("생성 및 입장");
        int randomNum = UnityEngine.Random.Range(1, 1000);
        string roomName = "Room " + randomNum; // 방 이름: Room 숫자
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // 최대 접속 플레이어 4명
        LobbyName = roomName; // 현재 생성된 로비 이름 저장
    }

    // 로비 생성 시 갱신 콜백
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("방 목록이 갱신되었습니다.");
        foreach (RoomInfo room in roomList)
        {
            Debug.Log("로비 이름: " + room.Name + ", 플레이어 수: " + room.PlayerCount + "/" + room.MaxPlayers);
        }
    }

    // 방 입장 매서드 (버튼 UI)
    public void JoinRoom()
    {
        Debug.Log("입장" + LobbyName);
        PhotonNetwork.JoinRandomRoom();
    }

    // 로비가 존재하지 않을 때 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("방 없음");
    }
}
