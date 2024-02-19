using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviourPunCallbacks
{
    public string nextSceneName; // 전환 씬 이름
    public Button button;

    private int playerNumber; // 플레이어 번호

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

        // 씬 전환
        photonView.RPC("SwitchScene", RpcTarget.All, nextSceneName);
    }

    // 씬 전환 RPC 메서드
    [PunRPC]
    void SwitchScene(string sceneName)
    {
        // 다음 씬으로 전환
        PhotonNetwork.LoadLevel(sceneName);
    }
}
