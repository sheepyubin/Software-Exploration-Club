using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using TMPro;
using System.Text;

public class SeverLauncher : MonoBehaviourPunCallbacks
{
    [Header("---UI InputFields---")]
    public TMP_InputField joinRoom;

    private string LobbyName;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // 포톤 서버 접속
    }

    // 포톤 서버 연결 시 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
    }

    // 포톤 서버 로비 입장 시 콜백
    public override void OnJoinedRoom()
    {
        Debug.Log("Lobby entrance: " + LobbyName);
        Debug.Log("Lobby Code: " + ExtractNumbersUsingRegex(LobbyName));
        PhotonNetwork.LoadLevel("InGame");
    }

    // 로비 생성 시 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("Create lobby: " + LobbyName);
    }

    // 로비 생성 매서드 (버튼 UI)
    public void CreateAndJoinRoom_BTN()
    {
        string roomName = "Room " + UnityEngine.Random.Range(1, 1000); // 로비 이름 설정
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // 로비 생성

        LobbyName = roomName;
    }

    // 랜덤 로비 입장 매서드 (버튼 UI)
    public void JoinRandomRoom_BTN()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // 지정 로비 입장 매서드 (버트 UI)
    public void JoinDesignatedRoom_BTN()
    {
        if (!string.IsNullOrEmpty(joinRoom.text)) // 입력 텍스트가 NULL이 아니라면
        {
            string roomName = "Room " + joinRoom.text; // 로비 이름 설정
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default, null); // 로비 입장
            // 입력받은 로비로 입장 없다면 생성 후 입장   

            LobbyName = roomName;
        }
        else
            Debug.Log("Please enter the lobby code");
    }

    // 참가 할 로비가 존재하지 않을 때 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There is no lobby \r\nPlease create a lobby");
    }

    // 문자열에서 숫자를 추출하는 매서드 (로비 코드)
    static string ExtractNumbersUsingRegex(string input)
    {
        // 숫자 추출 정규식
        string pattern = @"\d+";

        MatchCollection matches = Regex.Matches(input, pattern);

        // 문자열 반환
        return string.Join("", matches);
    }
}
