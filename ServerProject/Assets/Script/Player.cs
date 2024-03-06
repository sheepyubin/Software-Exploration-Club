using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
   

    public float speed = 5f;
    private int actorNumber;
    private static flag flag;

    private void Start()
    {
        flag=gameObject.GetComponent<flag>();
    }
    void Update()
    {
        if (photonView.IsMine)
        {
            HandleMovementInput();
        }
        if(PhotonNetwork.IsConnected)
        {
            actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (photonView.IsMine)
        {
            if (collision.gameObject.tag == "flag")
            {
                 Debug.Log("깃발과 충돌");
                flag.callScore(actorNumber);
            }
        }
    }
    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);

        // 움직임을 다른 플레이어에게 동기화
        if (photonView.IsMine)
        {
            photonView.RPC("SyncMovement", RpcTarget.Others, transform.position);
        }
    }

    [PunRPC]
    void SyncMovement(Vector3 newPosition)
    {
        // 다른 플레이어의 위치를 동기화
        transform.position = newPosition;
    }
}
