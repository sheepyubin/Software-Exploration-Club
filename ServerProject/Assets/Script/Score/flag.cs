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
        Debug.Log("���� �����ϴ� �÷��̾� ��: " + playerCount);
        GetScore = gameObject.GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void callScore(int actorNumber)
    {
        Debug.Log("���ھ�ȣ��");
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ű�� ��� ������ �ִٸ�

                // �������� Ŭ����
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
