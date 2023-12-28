using Photon.Pun;
using UnityEngine;

public class Player : MonoBehaviourPun
{
    public float speed = 5f;

    void Update()
    {
        if (photonView.IsMine)
        {
            HandleMovementInput();
        }
    }

    void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * speed * Time.deltaTime;
        transform.Translate(movement);

        // �������� �ٸ� �÷��̾�� ����ȭ
        if (photonView.IsMine)
        {
            photonView.RPC("SyncMovement", RpcTarget.Others, transform.position);
        }
    }

    [PunRPC]
    void SyncMovement(Vector3 newPosition)
    {
        // �ٸ� �÷��̾��� ��ġ�� ����ȭ
        transform.position = newPosition;
    }
}
