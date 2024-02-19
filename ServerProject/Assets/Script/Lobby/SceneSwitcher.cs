using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviourPunCallbacks
{
    public string nextSceneName; // ��ȯ �� �̸�
    public Button button;

    private int playerNumber; // �÷��̾� ��ȣ

    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
                button.interactable = true;
            }
        }
        else
        {
            button.interactable = false;
        }
    }

    void OnButtonClick()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            Debug.LogError("Only the master client can change the scene.");
            return;
        }

        // �� ��ȯ
        photonView.RPC("SwitchScene", RpcTarget.All, nextSceneName);
    }

    // �� ��ȯ RPC �޼���
    [PunRPC]
    void SwitchScene(string sceneName)
    {
        // ���� ������ ��ȯ
        PhotonNetwork.LoadLevel(sceneName);
    }
}
