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

    void Start()
    {
        ghostDelayTime = ghostDelay;
    }

    void Update()
    {
        if (makeGhost && photonView.IsMine) // ������ IsMine�� ����Ͽ� �� ���� �÷��̾ ������Ʈ�� ��Ʈ���ϴ��� Ȯ��
        {
            if (ghostDelayTime > 0)
            {
                ghostDelayTime -= Time.deltaTime;
            }
            else
            {
                photonView.RPC("CreateGhost", RpcTarget.All); // ��� �÷��̾�� RPC ȣ��
                ghostDelayTime = ghostDelay;
            }
        }
    }

    [PunRPC]
    void CreateGhost()
    {
        GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
        Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
        currentGhost.transform.localScale = transform.localScale;
        currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
        Destroy(currentGhost, 0.2f);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}