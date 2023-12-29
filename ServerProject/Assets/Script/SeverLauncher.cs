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
    public TMP_InputField joinRoom; // �κ� �ڵ� �Է� �ʵ�

    [Header("---Server UI---")]
    public Image serverConnector; // ���� Ŀ���� �̹���
    public TextMeshProUGUI serverConnectortext; // ���� Ŀ���� �ؽ�Ʈ
    public GameObject serverUI; // ���� UI ������Ʈ
    public GameObject loadingPanel; // �ε� �ǳ�

    public string lobbyCode; // �κ� �ڵ�
    private string lobbyName; // �κ� �̸�
    private bool Isconnected = false; // ������ ���� �Ǿ��°�?

    void Start()
    {
            PhotonNetwork.ConnectUsingSettings(); // ���� ���� ����
    }

    private void Update()
    {
        // ���� ���� �ÿ��� UI Ȱ��ȭ
        if (Isconnected)
        {
            serverConnector.color = Color.green;
            serverConnectortext.text = "Server connected";
            serverUI.SetActive(true);
        }

        PlayerPrefs.SetString("LobbyCode", lobbyCode);
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
        lobbyCode = ExtractNumbersUsingRegex(lobbyName);
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
        loadingPanel.SetActive(true);
        string roomName = "Room " + UnityEngine.Random.Range(1, 100); // �κ� �̸� ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default); // �κ� ����
        lobbyName = roomName;
    }

    // ���� �κ� ���� �ż��� (��ư UI)
    public void JoinRandomRoom_BTN()
    {

        PhotonNetwork.JoinRandomRoom();

        loadingPanel.SetActive(true);
    }

    // ���� �κ� ���� �ż��� (��Ʈ UI)
    public void JoinDesignatedRoom_BTN()
    {
        if (!string.IsNullOrEmpty(joinRoom.text)) // �Է� �ؽ�Ʈ�� NULL�� �ƴѰ�?
        {
            if (int.TryParse(joinRoom.text, out int roomNumber)) // ���ڷ� �̷���� ���ڿ��ΰ�?
            {
                string roomName = "Room " + roomNumber; // �κ� �̸� ����
                lobbyName = roomName;
                PhotonNetwork.JoinRoom(roomName); // �κ� ����
                // �Է¹��� �κ�� ���� ���ٸ� ���� �� ����   
            }
            else
            {
                Debug.Log("Please enter only numbers");
                // ���ڷ� ��ȯ�� �� ���� ��쿡 ���� ó�� ������ �߰��� �� �ֽ��ϴ�.
            }
        }
        else
            Debug.Log("Please enter the lobby code");

        loadingPanel.SetActive(true);
    }

    // ���� �� �κ� �������� ���� �� �ݹ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("JoinRandomFailed: " + message + " (Code: " + returnCode + ")");
        loadingPanel.SetActive(false);

        CreateAndJoinRoom_BTN();
    }

    // ���� �� �κ� �������� ���� �� �ݹ�
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Failed to join room: " + message);
        loadingPanel.SetActive(false);
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
}
