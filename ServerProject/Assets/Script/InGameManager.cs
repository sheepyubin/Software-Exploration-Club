using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("---PlayerPrefab Gameobject---")]
    public GameObject PlayerPrefab; // 플레이어 프리팹

    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // 로비 코드 텍스트

    private string lobbyCode;

    void Start()
    {
        lobbyCode = PlayerPrefs.GetString("LobbyCode");
        lobbyCodetext.text = lobbyCode;

        // 로컬 플레이어 생성
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

