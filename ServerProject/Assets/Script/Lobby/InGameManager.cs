using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Text.RegularExpressions;

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // 로비 코드 텍스트

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;

    private string lobbyCode;

    private void Start()
    {
        lobbyCode = ExtractNumbersUsingRegex(PhotonNetwork.CurrentRoom.Name);
        lobbyCodetext.text = lobbyCode;

        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (playerUiPrefab != null)
            {
                Instantiate(playerUiPrefab);
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

    // 문자열에서 숫자를 추출하는 메서드 (로비 코드)
    private string ExtractNumbersUsingRegex(string input)
    {
        // 숫자 추출 정규식
        string pattern = @"\d+";
        MatchCollection matches = Regex.Matches(input, pattern);
        // 문자열 반환
        return string.Join("", matches);
    }
}
