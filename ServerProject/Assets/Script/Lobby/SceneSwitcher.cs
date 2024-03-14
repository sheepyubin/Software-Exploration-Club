using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviourPunCallbacks
{
    public string nextSceneName; // ¿¸»Ø æ¿ ¿Ã∏ß
    public Button button;

    void Start()
    {

        if (PhotonNetwork.IsMasterClient)
        {
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
                button.enabled = true;
            }
        }
        else
        {
            button.enabled = false;
        }
    }

    void OnButtonClick()
    {
        // æ¿ ¿¸»Ø
        photonView.RPC("SwitchScene", RpcTarget.All, nextSceneName);
    }

    // æ¿ ¿¸»Ø RPC ∏ﬁº≠µÂ
    [PunRPC]
    void SwitchScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
