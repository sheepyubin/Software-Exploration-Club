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
        PhotonNetwork.ConnectUsingSettings(); // ���� ���� ����
    }

    // ���� ���� ���� �� �ݹ�
    public override void OnConnectedToMaster()
    {
        Debug.Log("Server connection");
    }

    // ���� ���� �κ� ���� �� �ݹ�
    public override void OnJoinedRoom()
    {
        Debug.Log("Lobby entrance: " + LobbyName);
        Debug.Log("Lobby Code: " + ExtractNumbersUsingRegex(LobbyName));
        PhotonNetwork.LoadLevel("InGame");
    }

    // �κ� ���� �� �ݹ�
    public override void OnCreatedRoom()
    {
        Debug.Log("Create lobby: " + LobbyName);
    }

    // �κ� ���� �ż��� (��ư UI)
    public void CreateAndJoinRoom_BTN()
    {
        string roomName = "Room " + UnityEngine.Random.Range(1, 1000); // �κ� �̸� ����
        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }); // �κ� ����

        LobbyName = roomName;
    }

    // ���� �κ� ���� �ż��� (��ư UI)
    public void JoinRandomRoom_BTN()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    // ���� �κ� ���� �ż��� (��Ʈ UI)
    public void JoinDesignatedRoom_BTN()
    {
        if (!string.IsNullOrEmpty(joinRoom.text)) // �Է� �ؽ�Ʈ�� NULL�� �ƴ϶��
        {
            string roomName = "Room " + joinRoom.text; // �κ� �̸� ����
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default, null); // �κ� ����
            // �Է¹��� �κ�� ���� ���ٸ� ���� �� ����   

            LobbyName = roomName;
        }
        else
            Debug.Log("Please enter the lobby code");
    }

    // ���� �� �κ� �������� ���� �� �ݹ�
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("There is no lobby \r\nPlease create a lobby");
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
