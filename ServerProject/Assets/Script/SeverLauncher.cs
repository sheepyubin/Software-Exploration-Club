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
using UnityEngine.UI;

public class SeverLauncher : MonoBehaviourPunCallbacks
{
    [Header("---InputFields UI---")]
    public TMP_InputField joinRoom; // 로비 코드 입력 필드

    [Header("---Server UI---")]
    public Image serverConnector; // 서버 커넥터 이미지
    public TextMeshProUGUI serverConnectortext; // 서버 커넥터 텍스트
    public GameObject serverUI; // 서버 UI 오브젝트
    public GameObject loadingPanel; // 로딩 판넬

    public string lobbyCode; // 로비 코드
    private string lobbyName; // 로비 이름
    private bool Isconnected = false; // 서버에 연결 되었는가?

    void Start()
    {
            PhotonNetwork.ConnectUsingSettings(); // 포톤 서버 연결
    }

    private void Update()
    {
        // 서버 연결 시에만 UI 활성화
        if (Isconnected)
        {
            serverConnector.color = Color.green;
            serverConnectortext.text = "Server connected";
            serverUI.SetActive(true);
        }

        PlayerPrefs.SetString("LobbyCode", lobbyCode);
    }

    // 포톤 서버 연결 시 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
        Isconnected = true;
    }

    // 포톤 서버 로비 입장 시 콜백
    public override void OnJoinedRoom()
    {
        lobbyCode = ExtractNumbersUsingRegex(lobbyName);
        Debug.Log("Lobby entrance: " + lobbyName);
        Debug.Log("Lobby Code: " + lobbyCode);
        PhotonNetwork.LoadLevel("InGame");
    }

    // 로비 생성 시 콜백
    public override void OnCreatedRoom()
    {
        Debug.Log("Create lobby: " + lobbyName);
    }

    // 로비 생성 매서드 (버튼 UI)
    public void CreateAndJoinRoom_BTN()
    {
        loadingPanel.SetActive(true);
        string roomName = "Room " + UnityEngine.Random.Range(1, 1000); // 로비 이름 설정
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // 로비 생성
        lobbyName = roomName;
    }

    // 랜덤 로비 입장 매서드 (버튼 UI)
    public void JoinRandomRoom_BTN()
    {
        PhotonNetwork.JoinRandomRoom();
        loadingPanel.SetActive(true);
    }

    // 지정 로비 입장 매서드 (버트 UI)
    public void JoinDesignatedRoom_BTN()
    {
        if (!string.IsNullOrEmpty(joinRoom.text)) // 입력 텍스트가 NULL이 아닌가?
        {
            if (int.TryParse(joinRoom.text, out int roomNumber)) // 숫자로 이루어진 문자열인가?
            {
                string roomName = "Room " + roomNumber; // 로비 이름 설정
                lobbyName = roomName;
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default, null); // 로비 입장
                // 입력받은 로비로 입장 없다면 생성 후 입장   
            }
            else
            {
                Debug.Log("Please enter only numbers");
                // 숫자로 변환할 수 없는 경우에 대한 처리 로직을 추가할 수 있습니다.
            }
        }
        else
            Debug.Log("Please enter the lobby code");

        loadingPanel.SetActive(true);
    }

    // 참가 할 로비가 존재하지 않을 때 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There is no lobby \r\nPlease create a lobby");
        loadingPanel.SetActive(false);
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
