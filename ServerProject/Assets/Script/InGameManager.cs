using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("---PlayerPrefab Gameobject---")]
    public GameObject PlayerPrefab; // �÷��̾� ������

    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // �κ� �ڵ� �ؽ�Ʈ

    private string lobbyCode;

    void Start()
    {
        lobbyCode = PlayerPrefs.GetString("LobbyCode");
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
}

