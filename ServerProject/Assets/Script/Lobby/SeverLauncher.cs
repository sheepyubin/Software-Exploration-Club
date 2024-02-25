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
    public TMP_InputField inputRoomCode; // �κ� �ڵ� �Է� �ʵ�
    //public TMP_InputField userIdText;// �г��� �Է� �ʵ�

    [Header("---Server UI---")]
    //public Image serverConnector; // ���� Ŀ���� �̹���
    // public TextMeshProUGUI serverConnectortext; // ���� Ŀ���� �ؽ�Ʈ
    public GameObject lobbyUI; // Lobby UI ������Ʈ
    public GameObject RoomUI; // Room UI ������Ʈ
    public GameObject JoinUI; // Join UI ������Ʈ
    public GameObject loadingScreen; // �ε� ȭ��
    public Slider loadingSlider; // �ε� �����̴�
    public GameObject titleScreen; // Ÿ��Ʋ ȭ��

   [Header("---Loddy Info---")]
    public string lobbyCode; // �κ� �ڵ�

    private string lobbyName = "lobby"; // �κ� �̸�
    private bool Isconnected = false; // ������ ���� �Ǿ��°�?
    private bool Isloading = true; // �ε����ΰ�?
    private Coroutine sliderCoroutine; // �ε� �����̴� �ڷ�ƾ
    //private string userId = "Hi world";

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // ���� ���� ����

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
        // ���� ���� �ÿ��� UI Ȱ��ȭ
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

    // ���� ���� ���� �� �ݹ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
        Isconnected = true;

        PhotonNetwork.JoinLobby();
    }


    // ���� ���� �κ� ���� �� �ݹ�
    public override void OnJoinedRoom()
    {
        Debug.Log("Lobby entrance: " + lobbyName);
        Debug.Log("Lobby Code: " + lobbyCode);
        PhotonNetwork.LoadLevel("InGame");
    }

    // �κ� ���� �� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Create lobby: " + lobbyName);
    }

    // �κ� ���� �ż��� (��ư UI)
    public void CreateAndJoinRoom_BTN()
    {
        loadingScreen.SetActive(true);
        string roomName = "Room " + UnityEngine.Random.Range(1, 100); // �κ� �̸� ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default); // �κ� ����
        lobbyName = roomName;
    }

    // ���� �κ� ���� �ż��� (��ư UI)
    public void JoinRandomRoom_BTN()
    {
        PhotonNetwork.JoinRandomRoom();
        loadingScreen.SetActive(true);
    }

    // ���� �κ� ���� �ż��� (��Ʈ UI)
    public void JoinDesignatedRoom_BTN()
    {
        if (!string.IsNullOrEmpty(inputRoomCode.text)) // �Է� �ؽ�Ʈ�� NULL�� �ƴѰ�?
        {
            if (int.TryParse(inputRoomCode.text, out int roomNumber)) // ���ڷ� �̷���� ���ڿ��ΰ�?
            {
                string roomName = "Room " + roomNumber; // �κ� �̸� ����
                lobbyName = roomName;
                PhotonNetwork.JoinRoom(roomName); // �κ� ����
                // �Է¹��� �κ�� ���� ���ٸ� ���� �� ����   
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

    // ���� �� �κ� �������� ���� �� �ݹ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinRandomFailed: " + message + " (Code: " + returnCode + ")");
        loadingScreen.SetActive(false);

        CreateAndJoinRoom_BTN();
    }

    // ���� �� �κ� �������� ���� �� �ݹ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room: " + message);
        loadingScreen.SetActive(false);
    }
    // ���� ����Ʈ ����
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {

        // ���� �����ϴ� �κ��� ��� �κ��� �̸� ���
        foreach (RoomInfo roomInfo in roomList)
        {
            Debug.Log("Room Name: " + roomInfo.Name);
        }
    }


    // ���ڿ����� ���ڸ� �����ϴ� �ż��� (�κ� �ڵ�)
    static string ExtractNumbersUsingRegex(string input)
    {
        // ���� ���� ���Խ�
        string pattern = @"\d+";

        MatchCollection matches = Regex.Matches(input, pattern);

        // ���ڿ� ��ȯ
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

    // �ε� �����̴��� ���� ������Ű�� �ڷ�ƾ
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
