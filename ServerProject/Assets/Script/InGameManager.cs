using UnityEngine;
using Photon.Pun;
using Unity.VisualScripting;
using TMPro;
using System.Text.RegularExpressions;

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("---PlayerPrefab Gameobject---")]
    public GameObject PlayerPrefab; // 플레이어 프리팹

    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // 로비 코드 텍스트

    private string lobbyCode;

    void Start()
    {
        lobbyCode = ExtractNumbersUsingRegex(PhotonNetwork.CurrentRoom.Name);
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

