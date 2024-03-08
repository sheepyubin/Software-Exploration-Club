using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS2 : MonoBehaviourPunCallbacks
{
    public KeyDataS2 keyData; // �������� 1�� KeyData ��ũ���ͺ� ������Ʈ
    public string nextStage;
    public GameObject Key1;
    public GameObject Key2;

    private void Start()
    {
        Key1.SetActive(false);
        Key1.SetActive(false);
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
            // Ű�� ��� ������ �ִٸ�
            if (keyData != null && keyData.Key1 && keyData.Key2)
            {
                // �������� Ŭ����
                Debug.Log("Success");

                // �� ��ȯ
                Debug.Log(nextStage);
                photonView.RPC("SwitchScene", RpcTarget.All, nextStage);
            }
            else
            {
                // ����
                Debug.Log("Fail");
            }
        }
    }

    // �� ��ȯ RPC �޼���
    [PunRPC]
    void SwitchScene(string sceneName)
    {
        // ���� ������ ��ȯ
        PhotonNetwork.LoadLevel(sceneName);
    }
}
