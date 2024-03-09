using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flag : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject endflag;
    public GameObject scoreBoard;
    public ScoreBoard scoreBoardScrpit;
    public PlayerContainer playerContainer;
    private static Score GetScore;
    int playerCount;
    void Start()
    {
        playerCount = PhotonNetwork.PlayerList.Length;
        Debug.Log("씬에 존재하는 플레이어 수: " + playerCount);
        GetScore = gameObject.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callScore(int actorNumber)
    {
        Debug.Log("스코어호출");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 키를 모두 가지고 있다면

                // 스테이지 클리어
                Debug.Log("Success");


                End();

        }
    }

    public void End()
    {
        scoreBoard.SetActive(true);
        scoreBoardScrpit.Score();

    }
}
