using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class EscPopup : MonoBehaviourPunCallbacks
{
    public GameObject popupObject; // 활성화할 게임 오브젝트 변수
    public Slider volumeSlider; // 슬라이더를 가리키는 레퍼런스
    public Image volumeImageLow; // 볼륨이 낮을 때
    public Image volumeImageHigh; // 볼륨이 높을 때


    private int count = 0;

    private void Start()
    {
        popupObject.SetActive(false);

        volumeSlider.value = 0.5f;
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

        if (AudioListener.volume > 0.5)
        {
            volumeImageLow.gameObject.SetActive(false);
            volumeImageHigh.gameObject.SetActive(true);
        }
        else
        {
            volumeImageLow.gameObject.SetActive(true);
            volumeImageHigh.gameObject.SetActive(false);
        }
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

