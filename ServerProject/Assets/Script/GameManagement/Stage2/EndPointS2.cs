using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS2 : MonoBehaviourPunCallbacks
{
    public Stage2Data data; // Stage1Data ��ũ���ͺ� ������Ʈ
    public string nextStage;
    public GameObject Key1;
    public GameObject Key2;
    public float delayTIme= 3f;
    public PlayerContainer playerContainer;
    public isDeadContainer isDeadContainer;
    public int targetScore = 0;

    private bool isClear = false;

    private void Awake()
    {
        Key1.SetActive(false);
        Key1.SetActive(false);
    }

    private void Update()
    {
        if (data.Returnkey1())
            Key1.SetActive(true);
        
        if (data.Returnkey2())
            Key2.SetActive(true);

        if (data.ReturnisClear())
            End(nextStage);
            
        if(isDeadContainer.ReturnisAllDead())
            End("Lobby");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // �������� Ŭ����
            data.SetisClear(true);

            Debug.Log(nextStage);
        }
    }

    // �� ��ȯ RPC �޼���
    [PunRPC]
    void SwitchScene(string nextStage)
    {
        if(nextStage == "Lobby")
            PhotonNetwork.Disconnect();

        PhotonNetwork.LoadLevel(nextStage);
    }

    public void AllDead()
    {
        Debug.Log("실패(전멸)");
        StartCoroutine(DelayedFunction(delayTIme, "Lobby"));
    }

    public void End(string next)
    {
        if(playerContainer.ReturnScore() < targetScore)
        {
            Debug.Log("실패");
            StartCoroutine(DelayedFunction(delayTIme, "Lobby"));
        }
        else
        {
            Debug.Log("성공");
            StartCoroutine(DelayedFunction(delayTIme, next));
        }
    }

    IEnumerator DelayedFunction(float delayTime, string nextStage)
    {
        yield return new WaitForSeconds(delayTime);

        if(!isClear)
        photonView.RPC("SwitchScene", RpcTarget.All, nextStage);

        isClear = true;
    }
}
