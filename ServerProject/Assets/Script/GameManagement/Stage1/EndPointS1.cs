using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointS1 : MonoBehaviourPunCallbacks
{
    public Stage1Data data; // Stage1Data 스크립터블 오브젝트
    public string nextStage;
    public GameObject Key1;
    public GameObject Key2;
    public GameObject scoreBoard;
    public ScoreBoard scoreBoardScrpit;
    public float delayTIme= 3f;

    private void Start()
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
        {
            End();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 키를 모두 가지고 있다면
            if (data.Returnkey1() && data.Returnkey2())
            {
                // 스테이지 클리어
                data.SetisClear(true);

                Debug.Log("Success");

                End();
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
        // 다음 씬으로 전환
        PhotonNetwork.LoadLevel(sceneName);
    }

    public void End()
    {
        scoreBoard.SetActive(true);
        scoreBoardScrpit.Score();

        // 씬 전환
        Debug.Log(nextStage);
        StartCoroutine(DelayedFunction(delayTIme));
    }

    IEnumerator DelayedFunction(float delayTime)
    {

        yield return new WaitForSeconds(delayTime);

        photonView.RPC("SwitchScene", RpcTarget.All, nextStage);
    }
}
