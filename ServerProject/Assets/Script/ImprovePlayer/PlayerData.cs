using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player;

    private void Start()
    {
        if (photonView.IsMine) // ���� �÷��̾ ���� �÷��̾��� ��쿡�� ����
        {
            player = new Player(PhotonNetwork.LocalPlayer.UserId, false, 0); // ���� ���̵�, false, �ʱ� ����(0)

            Debug.Log(PhotonNetwork.LocalPlayer.UserId);
        }
    }

    public void SetIsDead(bool isDead)
    {
        if (photonView.IsMine) // ���� �÷��̾��� ��쿡�� ����
        {
            player.SetisDead(isDead);
        }
    }

    public void SetScore(int newScore)
    {
        if (photonView.IsMine) // ���� �÷��̾��� ��쿡�� ����
        {
            player.SetScore(newScore);
        }
    }
}
