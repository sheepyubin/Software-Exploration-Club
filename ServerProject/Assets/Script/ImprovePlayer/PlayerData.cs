using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviourPunCallbacks
{
    public PlayerContainer playerContainer;
    public isDeadContainer isDeadContainer;
    public GameObject deadBody;
    public RopeLauncher ropeLauncher;

    public string userID;
    public bool isDead;
    public bool isClear;

    Color color;
    int newScore;

    private void Awake()
    {
        playerContainer.ResetScore();
        playerContainer.ResetisDead(PhotonNetwork.CurrentRoom.PlayerCount);

        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;
        isClear = false;

        if (playerContainer.ReturnPlayerColor(userID) == Color.white )
            color = SetRandomColor();
        else
            color = playerContainer.ReturnPlayerColor(userID);

        if (photonView.IsMine)
        {
            photonView.RPC("SyncPlayerColor", RpcTarget.AllBuffered, userID, color.r, color.g, color.b);
            photonView.RPC("SyncPlayerIsDead", RpcTarget.AllBuffered, userID, isDead);
        }
    }

    void Update()
    {
        if(isDead)
        {
            ropeLauncher.photonView.RPC("DisableRope", RpcTarget.AllBuffered);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
            if (other.CompareTag("Dead"))
            {
                isDead = true;

                isDeadContainer.AddisDead();
                
                photonView.RPC("SyncPlayerIsDead", RpcTarget.AllBuffered, userID, isDead);

                PhotonNetwork.Instantiate(deadBody.name, transform.position, Quaternion.identity);
            }

            if (other.CompareTag("EndPoint") && !isClear)
            {
                newScore = 100;

                playerContainer.AddScore(newScore);

                isClear = true;     
            }

            if (other.CompareTag("Coin"))
            {
                newScore = 10;

                playerContainer.AddScore(newScore);  
            }

            
            if (other.CompareTag("Key"))
            {
                newScore = 50;

                playerContainer.AddScore(newScore);  
            }

            //if (other.CompareTag("SafeMine"))
            //{
            //    int temp = 0;

            //    newScore = 100;
            //    playerContainer.AddScore(newScore);

            ////player.SetScore(newScore);
            //photonView.RPC("SyncPlayerScore", RpcTarget.AllBuffered, userID, newScore);
            //}
    }
    
    Color SetRandomColor()
    {
        float r = Random.Range(0f, 1f);
        float g = Random.Range(0f, 1f);
        float b = Random.Range(0f, 1f);
        float a = 1f; // Alpha

        Color color = new Color(r, g, b, a);

        return color;
    }
}
