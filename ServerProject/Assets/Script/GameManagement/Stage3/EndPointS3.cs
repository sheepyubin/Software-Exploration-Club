using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS3 : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // �÷��̾� �����̳� ��ũ���ͺ� ������Ʈ
    public KeyDataS3 keyData; // �������� 1�� KeyData ��ũ���ͺ� ������Ʈ
    public string nextStage;
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
        container.ResetContainer();

        PhotonNetwork.Disconnect();

        // ���� ������ ��ȯ
        PhotonNetwork.LoadLevel(sceneName);
    }
}
