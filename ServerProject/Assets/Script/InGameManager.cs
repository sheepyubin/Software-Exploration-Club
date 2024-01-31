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
    public GameObject PlayerPrefab; // 플레이어 프리팹

    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // 로비 코드 텍스트

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;


    private string lobbyCode;
    Vector2 spawnPosition = new Vector2(0f, 0f);


    void Start()
    {
        lobbyCode = ExtractNumbersUsingRegex(PhotonNetwork.CurrentRoom.Name);
        lobbyCodetext.text = lobbyCode;
        
        // 로컬 플레이어 생성
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
