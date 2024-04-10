using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS3 : MonoBehaviourPunCallbacks
{
    public PlayerContainer container; // �÷��̾� �����̳� ��ũ���ͺ� ������Ʈ
    public KeyDataS3 keyData; // �������� 1�� KeyData ��ũ���ͺ� ������Ʈ
    public string nextStage;
    public GameObject Key1;
    public GameObject Key2;
    public GameObject scoreBoard;
    public ScoreBoard scoreBoardScrpit;
    public float delayTIme = 3f;

    private string playerID;

    private void Start()
    {
        playerID = PhotonNetwork.LocalPlayer.UserId;

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
            // Ű�� ��� ������ �ִٸ�
            if (keyData != null && keyData.Key1 && keyData.Key2)
            {
                // �������� Ŭ����
                Debug.Log("Success");

                // �� ��ȯ
                Debug.Log(nextStage);

                End();
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
        PhotonNetwork.Disconnect();

        container.ResetContainer(playerID);

        // ���� ������ ��ȯ
        PhotonNetwork.LoadLevel(sceneName);
    }


    public void End()
    {
        scoreBoard.SetActive(true);
        scoreBoardScrpit.Score();

        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        photonView.RPC("SwitchScene", RpcTarget.All, nextStage);
    }
}
