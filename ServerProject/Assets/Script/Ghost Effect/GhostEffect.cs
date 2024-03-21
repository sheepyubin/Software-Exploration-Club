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
    private Vector3 lastSyncedPosition; // 마지막으로 동기화된 ghost의 위치

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
                photonView.RPC("CreateGhost", RpcTarget.All, transform.position); // 위치 정보를 함께 전달하여 RPC 호출
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
        if (stream.IsWriting) // 로컬 플레이어의 데이터를 스트림에 쓰기
        {
            stream.SendNext(transform.position); // 현재 위치를 스트림에 전송
        }
        else // 다른 플레이어의 데이터를 스트림에서 읽기
        {
            lastSyncedPosition = (Vector3)stream.ReceiveNext(); // 다른 플레이어의 위치 정보를 받아옴
        }
    }
}
