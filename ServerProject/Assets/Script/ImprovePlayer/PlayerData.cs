using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player; // Player Ŭ����
    public PlayerContainer playerContainer; // PlayerContainer ����

    string userID; // ���� UI
    bool isDead; // �׾��°�?
    int score; // ����
    Color color; // ����
    int newScore; // �߰� �� ����

    private void Start()
    {
        // ���� �ʱ�ȭ
        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;

        if (playerContainer.ReturnPlayerScore(userID) == -1) // playerScore�� �ƹ� ���� ���°�?
            score = 0;
        else // ���� �̹� �ִ°�?
            score = playerContainer.ReturnPlayerScore(userID);

        if (playerContainer.ReturnPlayerColor(userID) == Color.white ) // playerColor�� �ƹ� ���� ���°�?
        {
            color = SetRandomColor(); // ���� ���� ����
            playerContainer.AddPlayerColor(userID, color); // �� �߰�
        }
        else // ���� �̹� �ִ°�?
            color = playerContainer.ReturnPlayerColor(userID); // ���� �ִ� �� ����

        if (photonView.IsMine) // ���� �÷��̾��ΰ�?
        {
            player = new Player(userID, isDead, score, color); // ���� ���̵�, false, �ʱ� ����(0), ����

            Debug.Log("userID: " + userID + " " + "isDead: " + isDead + " " + "score: " + score.ToString());
        }
    }

    // ���� ���� ���� ��ȯ�ϴ� �ż���
    Color SetRandomColor()
    {
        // ���� RGB �� ����
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = 1f; // Alpha

        Color color = new Color(r, g, b, a);

        return color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Dead")) // Dead �±׿� ��Ҵ°�?
        {
            // ���
            isDead = true;
            player.SetisDead(isDead);

            // ��� ����(0) �߰�
            newScore = 0;
            player.SetScore(newScore);

            Debug.Log(player.Returnscore().ToString());
        }
        if (other.CompareTag("EndPoint")) // EndPoint �±׿� ��Ҵ°�?
        {
            // ���� ����(100) �߰�
            newScore = 100;
            player.SetScore(newScore);

            playerContainer.AddPlayerScore(userID,player.Returnscore());

            Debug.Log(playerContainer.ReturnPlayerScore(userID));
        }
    }
}
