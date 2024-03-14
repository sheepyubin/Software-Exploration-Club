using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player; // Player 클래스
    public PlayerContainer playerContainer; // PlayerContainer 참조

    string userID; // 유저 UI
    bool isDead; // 죽었는가?
    int score; // 점수
    Color color; // 색상
    int newScore; // 추가 할 점수

    private void Start()
    {
        // 변수 초기화
        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;

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
            player = new Player(userID, isDead, score, color); // 유저 아이디, false, 초기 점수(0), 색상

            Debug.Log("userID: " + userID + " " + "isDead: " + isDead + " " + "score: " + score.ToString());
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
    }
}
