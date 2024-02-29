using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Text.RegularExpressions;

public class InGameManager : MonoBehaviourPunCallbacks
{
    [Header("---Lobbycode UI---")]
    public TextMeshProUGUI lobbyCodetext; // �κ� �ڵ� �ؽ�Ʈ

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
    // ���ڿ����� ���ڸ� �����ϴ� �޼��� (�κ� �ڵ�)
    private string ExtractNumbersUsingRegex(string input)
    {
        // ���� ���� ���Խ�
        string pattern = @"\d+";
        MatchCollection matches = Regex.Matches(input, pattern);
        // ���ڿ� ��ȯ
        return string.Join("", matches);
    }
}
