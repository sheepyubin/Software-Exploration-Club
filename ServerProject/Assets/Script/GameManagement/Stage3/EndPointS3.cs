using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS3 : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // 플레이어 컨테이너 스크립터블 오브젝트
    public KeyDataS3 keyData; // 스테이지 1의 KeyData 스크립터블 오브젝트
    public string nextStage;
    public GameObject Key1;
    public GameObject Key2;

    private void Start()
    {
        Key1.SetActive(false);
        Key1.SetActive(false);

        keyData.Key1 = false;
        keyData.Key2 = false;
    }

    private void Update()
    {
        if (keyData.Key1)
            Key1.SetActive(true);

        if (keyData.Key2)
            Key2.SetActive(true);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 키를 모두 가지고 있다면
            if (keyData != null && keyData.Key1 && keyData.Key2)
            {
                // 스테이지 클리어
                Debug.Log("Success");

                // 씬 전환
                Debug.Log(nextStage);
                photonView.RPC("SwitchScene", RpcTarget.All, nextStage);
            }
            else
            {
                // 실패
                Debug.Log("Fail");
            }
        }
    }

    // 씬 전환 RPC 메서드
    [PunRPC]
    void SwitchScene(string sceneName)
    {
        PhotonNetwork.Disconnect();

        // 다음 씬으로 전환
        PhotonNetwork.LoadLevel(sceneName);
    }
}
