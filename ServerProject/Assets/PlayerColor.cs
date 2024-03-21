using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviourPunCallbacks, IPunObservable
{
    public PlayerData playerData; // PlayerData 스크립트 참조

    private SpriteRenderer spriteRenderer;
    private Color color;

    private void Start()
    {
        // 초기화
        playerData = GetComponent<PlayerData>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (playerData.Returnplayer() != null)
            color = playerData.player.Returncolor();
        else
            return;

        // 로컬 플레이어만 색상을 설정합니다.
        if (photonView.IsMine)
        {
            spriteRenderer.color = color;
        }
    }


    // 색상 적용 및 동기화 메서드
    [PunRPC]
    void ApplyColor(float r, float g, float b, float a)
    {
        spriteRenderer.color = new Color(r, g, b, a);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (photonView.IsMine) // 로컬 플레이어인가?
            { 
                // 색상 정보 전송
                stream.SendNext(spriteRenderer.color.r);
                stream.SendNext(spriteRenderer.color.g);
                stream.SendNext(spriteRenderer.color.b);
                stream.SendNext(spriteRenderer.color.a);
            }
        }
        else // 다른 플레이어인가?
        {
            // 색상 정보 수신
            float r = (float)stream.ReceiveNext();
            float g = (float)stream.ReceiveNext();
            float b = (float)stream.ReceiveNext();
            float a = (float)stream.ReceiveNext();

            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(r, g, b, a);
            }
        }
    }
}
