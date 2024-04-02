using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public Player player; // Player Ŭ����
    public PlayerContainer playerContainer; // PlayerContainer ����
    public bool isCreate = false; // �÷��̾� ��ü�� ���� �Ǿ��°�?
    public GameObject deadBody;

    string userID; // ���� UI
    public bool isDead; // �׾��°�?
    public bool isClear; // ���������� Ŭ���� �ߴ°�?
    int score; // ����
    Color color; // ����
    int newScore; // �߰� �� ����


    private void Start()
    {
        // ���� �ʱ�ȭ
        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;
        isClear = false;

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
            if (!isCreate)
            {
                player = new Player(userID, isDead, score, color); // ���� ���̵�, false, �ʱ� ����(0), ����

                playerContainer.AddPlayerisDead(userID, isDead);
                isCreate = true;    

                Debug.Log("userID: " + userID + " " + "isDead: " + isDead + " " + "score: " + score.ToString());
            }
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

            playerContainer.AddPlayerisDead(userID, isDead);

            PhotonNetwork.Instantiate(deadBody.name, transform.position, Quaternion.identity);

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
