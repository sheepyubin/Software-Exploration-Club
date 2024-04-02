using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player; // Player 클래스
    public PlayerContainer playerContainer; // PlayerContainer 참조
    public bool isCreate = false; // 플레이어 객체가 생성 되었는가?
    public GameObject deadBody;

    string userID; // 유저 UI
    public bool isDead; // 죽었는가?
    public bool isClear; // 스테이지를 클리어 했는가?
    int score; // 점수
    Color color; // 색상
    int newScore; // 추가 할 점수


    private void Start()
    {
        // 변수 초기화
        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;
        isClear = false;

        if (playerContainer.ReturnPlayerScore(userID) == -1) // playerScore에 아무 값도 없는가?
            score = 0;
        else // 값이 이미 있는가?
            score = playerContainer.ReturnPlayerScore(userID);

        if (playerContainer.ReturnPlayerColor(userID) == Color.white ) // playerColor에 아무 색도 없는가?
        {
            color = SetRandomColor(); // 랜덤 색상 생성
            playerContainer.AddPlayerColor(userID, color); // 색 추가
        }
        else // 색이 이미 있는가?
            color = playerContainer.ReturnPlayerColor(userID); // 원래 있던 색 적용

        if (photonView.IsMine) // 로컬 플레이어인가?
        {
            if (!isCreate)
            {
                player = new Player(userID, isDead, score, color); // 유저 아이디, false, 초기 점수(0), 색상

                playerContainer.AddPlayerisDead(userID, isDead);
                isCreate = true;    

                Debug.Log("userID: " + userID + " " + "isDead: " + isDead + " " + "score: " + score.ToString());
            }
        }
    }

    // 랜덤 색상 값을 반환하는 매서드
    Color SetRandomColor()
    {
        // 랜덤 RGB 값 생성
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = 1f; // Alpha

        Color color = new Color(r, g, b, a);

        return color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead")) // Dead 태그에 닿았는가?
        {
            // 사망
            isDead = true;
            player.SetisDead(isDead);

            playerContainer.AddPlayerisDead(userID, isDead);

            PhotonNetwork.Instantiate(deadBody.name, transform.position, Quaternion.identity);

            // 사망 점수(0) 추가
            newScore = 0;
            player.SetScore(newScore);

            Debug.Log(player.Returnscore().ToString());
        }
        if (other.CompareTag("EndPoint")) // EndPoint 태그에 닿았는가?
        {
            // 성공 점수(100) 추가
            newScore = 100;
            player.SetScore(newScore);

            playerContainer.AddPlayerScore(userID,player.Returnscore());

            Debug.Log(playerContainer.ReturnPlayerScore(userID));
        }
        if (other.CompareTag("SafeMine"))
        {
            int temp = 0;

            newScore = 100;

            if (temp == 0)
            {
                player.SetScore(newScore);
                playerContainer.AddPlayerScore(userID,player.Returnscore());
                temp++;
                Debug.Log(playerContainer.ReturnPlayerScore(userID));
            }
        }
    }

    public Player Returnplayer()
    {
        if (isCreate)
            return player;
        else
            return null;
    }
}
