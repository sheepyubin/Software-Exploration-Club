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
    public bool isStart;

    void Start()
    {
        isStart = false;
        if (PhotonNetwork.IsMasterClient)
        {
            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
                button.gameObject.SetActive(true);
                gameMode.SetActive(true);
            }
        }
        else
        {
            button.enabled = false;
            button.gameObject.SetActive(false);
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
            isStart = true;
            
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
