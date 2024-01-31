using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;
using System.Text.RegularExpressions;
using Photon.Pun.Demo.PunBasics;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviourPunCallbacks
{

    [Header("---PlayerPrefab Gameobject---")]
    public GameObject PlayerPrefab; // �÷��̾� ������

    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // �κ� �ڵ� �ؽ�Ʈ

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;


    private string lobbyCode;
    Vector2 spawnPosition = new Vector2(0f, 0f);


    void Start()
    {
        lobbyCode = ExtractNumbersUsingRegex(PhotonNetwork.CurrentRoom.Name);
        lobbyCodetext.text = lobbyCode;
        
        // ���� �÷��̾� ����
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.Instantiate(PlayerPrefab.name, spawnPosition, Quaternion.identity);
            if (playerUiPrefab != null)
            {
                GameObject _uiGo = Instantiate(playerUiPrefab);
                //_uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
            else
            {
                Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
            }
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
