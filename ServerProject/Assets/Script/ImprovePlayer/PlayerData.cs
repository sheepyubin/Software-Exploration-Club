using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player;

    private void Start()
    {
        if (photonView.IsMine) // 현재 플레이어가 로컬 플레이어인 경우에만 실행
        {
            player = new Player(PhotonNetwork.LocalPlayer.UserId, false, 0); // 유저 아이디, false, 초기 점수(0)

            Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        }
    }

    public void SetIsDead(bool isDead)
    {
        if (photonView.IsMine) // 로컬 플레이어인 경우에만 실행
        {
            player.SetisDead(isDead);
        }
    }

    public void SetScore(int newScore)
    {
        if (photonView.IsMine) // 로컬 플레이어인 경우에만 실행
        {
            player.SetScore(newScore);
        }
    }
}
