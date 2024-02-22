using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher : MonoBehaviourPunCallbacks, IPunObservable
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    private Camera mainCamera;
    private Vector3 playerPosition; // 플레이어의 위치를 저장하기 위한 변수

    private void Start()
    {
        distanceJoint.enabled = false;

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("카메라를 찾을 수 없습니다.");
        }
        else
        {
            distanceJoint.enabled = false;
        }
    }

    void Update()
    {
        if (photonView != null && photonView.IsMine) // 자신의 캐릭터만 조종
        {
            playerPosition = transform.position;
            if (mainCamera == null)
                return;

            // 로프 발사
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                photonView.RPC("LaunchRope", RpcTarget.AllBuffered, mousePos);
            }
            // 로프 비활성화
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                photonView.RPC("DisableRope", RpcTarget.AllBuffered);
            }

            if(distanceJoint.enabled)
            {
                photonView.RPC("UpdateNode", RpcTarget.AllBuffered, playerPosition);
            }
        }
    }

    [PunRPC]
    void LaunchRope(Vector2 mousePos, PhotonMessageInfo info)
    {
        Vector3 node1Pos = mousePos;
        Vector3 node2Pos = playerPosition; // 플레이어의 위치로 업데이트
        lineRenderer.SetPosition(0, node1Pos);
        //lineRenderer.SetPosition(1, node2Pos);
        distanceJoint.connectedAnchor = node1Pos;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
    }

    [PunRPC]
    void DisableRope()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    [PunRPC]
    void UpdateNode(Vector3 position)
    {
        lineRenderer.SetPosition(1, position);
    }

    // IPunObservable 인터페이스 구현
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // 사용하지 않음
    }
}
