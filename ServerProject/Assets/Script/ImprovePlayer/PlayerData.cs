using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player;

    Test test;
    string info;

    private void Start()
    {
        test = new Test();
        if (photonView.IsMine) // 현재 플레이어가 로컬 플레이어인 경우에만 실행
        {
            player = new Player(PhotonNetwork.LocalPlayer.UserId, false, 0); // 유저 아이디, false, 초기 점수(0)
        }
    }

    private void Update()
    {
        string ID = "ID: " + PhotonNetwork.LocalPlayer.UserId;
        string isDead = "isDead: " + player.ReturnisDead().ToString();
        string Score = "Score: " + player.Returnscore().ToString();

        test.Info(ID, isDead, Score);

        //Debug.Log(ID);
        //Debug.Log(isDead);
        //Debug.Log(Score);
    }

    public void TestScore()
    {
        player.SetScore(10);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("test"))
        {
            TestScore();
        }
    }
}
