using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS1 : MonoBehaviourPunCallbacks
{
    public Stage1Data data; // Stage1Data ��ũ���ͺ� ������Ʈ
    public string nextStage;
    public GameObject Key1;
    public GameObject Key2;
    public GameObject scoreBoard;
    public ScoreBoard scoreBoardScrpit;
    public float delayTIme= 3f;
    public PlayerContainer playerContainer;

    bool isMethodCalled;

    private void Awake()
    {
        isMethodCalled = false;

        Key1.SetActive(false);
        Key1.SetActive(false);
    }

    private void Update()
    {
        int temp = 0;

        if (data.Returnkey1())
            Key1.SetActive(true);
        
        if (data.Returnkey2())
            Key2.SetActive(true);

        if (data.ReturnisClear())
            End();

        if(playerContainer.ReturnPlayerisDeadAll() && temp==0)
        {
            Debug.Log(playerContainer.ReturnPlayerisDeadAll());
            temp++;
            End();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ű�� ��� ������ �ִٸ�
            if (data.Returnkey1() && data.Returnkey2())
            {

                // �������� Ŭ����
                data.SetisClear(true);

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
        if(!isMethodCalled)
        PhotonNetwork.LoadLevel(sceneName);

        isMethodCalled = true;
    }

    public void End()
    {
        playerContainer.ResetisCreated(PhotonNetwork.LocalPlayer.UserId);
        
        scoreBoardScrpit.Score();

        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);

        photonView.RPC("SwitchScene", RpcTarget.All, nextStage);
    }
}
