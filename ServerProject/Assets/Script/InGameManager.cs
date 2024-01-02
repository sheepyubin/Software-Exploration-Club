using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;
using System.Text.RegularExpressions;

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("---PlayerPrefab Gameobject---")]
    public GameObject PlayerPrefab; // �÷��̾� ������

    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // �κ� �ڵ� �ؽ�Ʈ

    private string lobbyCode;

    void Start()
    {
        lobbyCode = ExtractNumbersUsingRegex(PhotonNetwork.CurrentRoom.Name);
        lobbyCodetext.text = lobbyCode;

        // ���� �÷��̾� ����
        if (PhotonNetwork.IsConnectedAndReady)
        {
            Vector2 spawnPosition = new Vector2(0f, 0f);
            PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPosition, Quaternion.identity);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel("Lobby");
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

