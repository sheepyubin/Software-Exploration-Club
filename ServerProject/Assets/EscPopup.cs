using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class EscPopup : MonoBehaviourPunCallbacks
{
    public GameObject popupObject; // Ȱ��ȭ�� ���� ������Ʈ ����

    private int count = 0;

    private void Start()
    {
        popupObject.SetActive(false);
    }

    void Update()
    {
        // Check if the ESC key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            popupObject.SetActive(true); // Ȱ��ȭ ���� ����
            count++;
        }

        if (count % 2 == 0)
            Disabled();
    }

    public void DisabledButton()
    {
        count--;

        popupObject.SetActive(false);
    }

    void Disabled()
    {
        popupObject.SetActive(false);
    }
    public void ToLobby()
    {
        PhotonNetwork.Disconnect();
        PhotonNetwork.LoadLevel("Lobby");
    }
}

