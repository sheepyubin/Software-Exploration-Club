using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EscPopup : MonoBehaviourPunCallbacks
{
    public GameObject popupObject; // 활성화할 게임 오브젝트 변수
    public Slider volumeSlider; // 슬라이더를 가리키는 레퍼런스


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
            popupObject.SetActive(true); // 활성화 상태 반전
            count++;
        }

        if (count % 2 == 0)
            Disabled();

        AudioListener.volume = volumeSlider.value;
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

