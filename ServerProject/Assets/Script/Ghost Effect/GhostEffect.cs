using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GhostEffect : MonoBehaviourPunCallbacks, IPunObservable
{
    public float ghostDelay;
    private float ghostDelayTime;
    public GameObject ghost;
    public bool makeGhost = false;
    public bool isFlip;

    private SpriteRenderer sp;
    private Vector3 lastSyncedPosition; // ���������� ����ȭ�� ghost�� ��ġ

    void Start()
    {
        ghostDelayTime = ghostDelay;
        sp = ghost.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (makeGhost && photonView.IsMine)
        {
            if (isFlip)
                sp.flipX = true;
            else
                sp.flipX = false;

            if (ghostDelayTime > 0)
            {
                ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                photonView.RPC("CreateGhost", RpcTarget.All, transform.position); // ��ġ ������ �Բ� �����Ͽ� RPC ȣ��
                ghostDelayTime = ghostDelay;
            }
        }
    }

    [PunRPC]
    void CreateGhost(Vector3 position)
    {
        GameObject currentGhost = Instantiate(ghost, position, transform.rotation);
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        currentGhost.transform.localScale = transform.localScale;
        currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
        Destroy(currentGhost, 0.2f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting) // ���� �÷��̾��� �����͸� ��Ʈ���� ����
        {
            stream.SendNext(transform.position); // ���� ��ġ�� ��Ʈ���� ����
        }
        else // �ٸ� �÷��̾��� �����͸� ��Ʈ������ �б�
        {
            lastSyncedPosition = (Vector3)stream.ReceiveNext(); // �ٸ� �÷��̾��� ��ġ ������ �޾ƿ�
        }
    }
}
