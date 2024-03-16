using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class SceneSwitcher : MonoBehaviourPunCallbacks
{
    public string nextSceneName = "Stage_1";
    public Button button;
    public GameObject gameMode;
    public GameModeSelect gameModeSelect;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
                button.enabled = true;
                gameMode.SetActive(true);
            }
        }
        else
        {
            button.enabled = false;
            gameMode.SetActive(false);
        }
    }

    private void Update()
    {
        nextSceneName = gameModeSelect.ReturnGameMode();
    }

    void OnButtonClick()
    {
        // �� ��ȯ
        if (nextSceneName != null)
        {
            Debug.Log(nextSceneName);

            photonView.RPC("SwitchScene", RpcTarget.All, nextSceneName);
        }
    }

    // �� ��ȯ RPC �޼���
    [PunRPC]
    void SwitchScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
