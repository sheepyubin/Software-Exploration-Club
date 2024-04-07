using Photon.Pun;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    //private Player player; // Player Ŭ����
    public PlayerContainer playerContainer; // PlayerContainer ����
    private bool isCreate = false; // �÷��̾� ��ü�� ���� �Ǿ��°�?
    public GameObject deadBody;

    string userID; // ���� UI
    public bool isDead; // �׾��°�?
    public bool isClear; // ���������� Ŭ���� �ߴ°�?
    public int tmepScore;
    int score; // ����
    Color color; // ����
    int newScore; // �߰� �� ����


    void Update()
    {
        tmepScore = playerContainer.ReturnPlayerScore( PhotonNetwork.LocalPlayer.UserId);
    }
    private void Start()
    {
        if (photonView.IsMine) // ���� �÷��̾��ΰ�?
        {
            userID = PhotonNetwork.LocalPlayer.UserId;

            if (!playerContainer.ReturnisCreated(userID))
            {
                // ���� �ʱ�ȭ
                isDead = false;
                isClear = false;

                if (playerContainer.ReturnPlayerScore(userID) == -1) // playerScore�� �ƹ� ���� ���°�?
                    score = 0;
                else // ���� �̹� �ִ°�?
                    score = playerContainer.ReturnPlayerScore(userID);

                if (playerContainer.ReturnPlayerColor(userID) == Color.white ) // playerColor�� �ƹ� ���� ���°�?
                {
                    color = SetRandomColor(); // ���� ���� ����
                }
                else // ���� �̹� �ִ°�?
                    color = playerContainer.ReturnPlayerColor(userID); // ���� �ִ� �� ����

                //player = new Player(userID, isDead, score, color); // ���� ���̵�, false, �ʱ� ����(0), ����
                photonView.RPC("SyncPlayerColor", RpcTarget.AllBuffered, userID, color.r, color.g, color.b);
                photonView.RPC("SyncPlayerIsDead", RpcTarget.AllBuffered, userID, isDead);
                photonView.RPC("SyncPlayerScore", RpcTarget.AllBuffered, userID, score);

                playerContainer.SetisCreated(userID,true);

                isCreate = true;    

                Debug.Log("userID: " + userID + " " + "isDead: " + playerContainer.ReturnPlayerisDead(userID) + " " + "score: " + playerContainer.ReturnPlayerScore(userID).ToString());
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
        if(!isClear)
        {
            if (other.CompareTag("Dead")) // Dead �±׿� ��Ҵ°�?
            {
                // ���
                isDead = true;

                photonView.RPC("SyncPlayerIsDead", RpcTarget.AllBuffered, userID, isDead);

                PhotonNetwork.Instantiate(deadBody.name, transform.position, Quaternion.identity);

                // ��� ����(0) �߰�
                newScore = 0;

                photonView.RPC("SyncPlayerScore", RpcTarget.AllBuffered, userID, newScore);

                //player.SetScore(newScore);
            }
            if (other.CompareTag("EndPoint")) // EndPoint �±׿� ��Ҵ°�?
            {
                // ���� ����(100) �߰�
                newScore = 100;
                //player.SetScore(newScore);

                photonView.RPC("SyncPlayerScore", RpcTarget.AllBuffered, userID, newScore);


                isClear = true;     
            }
            if (other.CompareTag("SafeMine"))
            {
                int temp = 0;

                newScore = 100;

                if (temp == 0)
                {
                    //player.SetScore(newScore);
                    photonView.RPC("SyncPlayerScore", RpcTarget.AllBuffered, userID, newScore);
                    temp++;
                }
            }
        }
    }
}
