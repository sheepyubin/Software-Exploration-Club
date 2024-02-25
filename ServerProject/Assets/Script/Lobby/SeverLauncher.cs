using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SeverLauncher : MonoBehaviourPunCallbacks
{
    [Header("---InputFields UI---")]
    public TMP_InputField inputRoomCode; // 로비 코드 입력 필드
    //public TMP_InputField userIdText;// 닉네임 입력 필드

    [Header("---Server UI---")]
    //public Image serverConnector; // 서버 커넥터 이미지
    // public TextMeshProUGUI serverConnectortext; // 서버 커넥터 텍스트
    public GameObject lobbyUI; // Lobby UI 오브젝트
    public GameObject RoomUI; // Room UI 오브젝트
    public GameObject JoinUI; // Join UI 오브젝트
    public GameObject loadingScreen; // 로딩 화면
    public Slider loadingSlider; // 로딩 슬라이더
    public GameObject titleScreen; // 타이틀 화면

   [Header("---Loddy Info---")]
    public string lobbyCode; // 로비 코드

    private string lobbyName = "lobby"; // 로비 이름
    private bool Isconnected = false; // 서버에 연결 되었는가?
    private bool Isloading = true; // 로딩중인가?
    private Coroutine sliderCoroutine; // 로딩 슬라이더 코루틴
    //private string userId = "Hi world";

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // 포톤 서버 연결

        titleScreen.SetActive(false);
        lobbyUI.SetActive(false);
        RoomUI.SetActive(false);
        JoinUI.SetActive(false);

        loadingScreen.SetActive(true);
        loadingSlider.gameObject.SetActive(true);

        Loading();
    }

    private void Update()
    {
        // 서버 연결 시에만 UI 활성화
        if (Isconnected && !Isloading)
        {
            titleScreen.SetActive(true);

            loadingScreen.SetActive(false);
            loadingSlider.gameObject.SetActive(false);

            lobbyCode = ExtractNumbersUsingRegex(lobbyName);
            // serverConnector.color = Color.green;
            // serverConnectortext.text = "Server connected";
            lobbyUI.SetActive(true);
        }
        else
        {
            titleScreen.SetActive(false);
            lobbyUI.SetActive(false);
            RoomUI.SetActive(false);
            JoinUI.SetActive(false);
        }
        //PlayerPrefs.SetString("USER_ID", userId);
        PlayerPrefs.SetString("LobbyCode", lobbyCode);
        //PhotonNetwork.NickName = userIdText.text;
    }

    // 포톤 서버 연결 시 콜백
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
        Isconnected = true;

        PhotonNetwork.JoinLobby();
    }


    // 포톤 서버 로비 입장 시 콜백
    public override void OnJoinedRoom()
    {
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
        loadingScreen.SetActive(true);
        string roomName = "Room " + UnityEngine.Random.Range(1, 100); // 로비 이름 설정
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default); // 로비 생성
        lobbyName = roomName;
    }

    // 랜덤 로비 입장 매서드 (버튼 UI)
    public void JoinRandomRoom_BTN()
    {
        PhotonNetwork.JoinRandomRoom();
        loadingScreen.SetActive(true);
    }

    // 지정 로비 입장 매서드 (버트 UI)
    public void JoinDesignatedRoom_BTN()
    {
        if (!string.IsNullOrEmpty(inputRoomCode.text)) // 입력 텍스트가 NULL이 아닌가?
        {
            if (int.TryParse(inputRoomCode.text, out int roomNumber)) // 숫자로 이루어진 문자열인가?
            {
                string roomName = "Room " + roomNumber; // 로비 이름 설정
                lobbyName = roomName;
                PhotonNetwork.JoinRoom(roomName); // 로비 입장
                // 입력받은 로비로 입장 없다면 생성 후 입장   
                loadingScreen.SetActive(true);
            }
            else
            {
                Debug.Log("Please enter only numbers");
            }
        }
        else
            Debug.Log("Please enter the lobby code");

    }

    // 참가 할 로비가 존재하지 않을 때 콜백
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinRandomFailed: " + message + " (Code: " + returnCode + ")");
        loadingScreen.SetActive(false);

        CreateAndJoinRoom_BTN();
    }

    // 지정 한 로비가 존재하지 않을 때 콜백
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room: " + message);
        loadingScreen.SetActive(false);
    }
    // 서버 리스트 갱신
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        // 현재 존재하는 로비의 모든 로비의 이름 출력
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log("Room Name: " + roomInfo.Name);
        }
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

    public void Loading()
    {
        loadingScreen.SetActive(true);
        loadingSlider.gameObject.SetActive(true);

        sliderCoroutine = StartCoroutine(IncreaseLoadingSliderValue());
    }

    public void Tutorial()
    {
        Isloading = true;

        Loading();

        SceneManager.LoadScene("Tutorial");
    }

    // 로딩 슬라이더의 값을 증가시키는 코루틴
    IEnumerator IncreaseLoadingSliderValue()
    {
        loadingSlider.value = 0;

        while (true)
        {
            float increaseAmount = Random.Range(0.01f, 0.2f);

            yield return new WaitForSeconds(0.05f);
            loadingSlider.value += increaseAmount;

            if (loadingSlider.value >= loadingSlider.maxValue)
            {
                yield return new WaitForSeconds(0.5f);

                Isloading = false;
                StopCoroutine(sliderCoroutine);
            }
        }
    }
}
