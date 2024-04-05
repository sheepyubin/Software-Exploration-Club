using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : MonoBehaviourPunCallbacks, IPunObservable
{
    //public PlayerData playerData; // PlayerData ��ũ��Ʈ ����
    public PlayerContainer playerContainer;

    private string userID; // ���� UI
    private SpriteRenderer spriteRenderer;
    private Color color;

    private void Start()
    {
        userID = PhotonNetwork.LocalPlayer.UserId;
        // �ʱ�ȭ
        //playerData = GetComponent<PlayerData>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // if (playerData.Returnplayer() != null)
        //     color = playerData.player.Returncolor();
        // else
        //     return;

        color = playerContainer.ReturnPlayerColor(userID);
        // ���� �÷��̾ ������ �����մϴ�.
        if (photonView.IsMine)
        {
            spriteRenderer.color = color;
        }
    }


    // ���� ���� �� ����ȭ �޼���
    [PunRPC]
    void ApplyColor(float r, float g, float b, float a)
    {
        spriteRenderer.color = new Color(r, g, b, a);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (photonView.IsMine) // ���� �÷��̾��ΰ�?
            { 
                // ���� ���� ����
                stream.SendNext(spriteRenderer.color.r);
                stream.SendNext(spriteRenderer.color.g);
                stream.SendNext(spriteRenderer.color.b);
                stream.SendNext(spriteRenderer.color.a);
            }
        }
        else // �ٸ� �÷��̾��ΰ�?
        {
            // ���� ���� ����
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
