using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher : MonoBehaviourPunCallbacks, IPunObservable
{
    public LineRenderer lineRenderer; // 라인 랜더러
    public DistanceJoint2D distanceJoint; // 디스턴스 조인트 
    public string targetTag = "Grappleable"; // 타겟 태그
    public PlayerData playerData;

    private Camera mainCamera; // 카메라
    private Vector3 playerPosition; // 플레이어의 위치를 저장하기 위한 변수

    private void Start()
    {
        // 변수 초기화
        distanceJoint.enabled = false;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (photonView != null && photonView.IsMine && !playerData.isDead) // 로컬 플레이어인가?
        {
            playerPosition = transform.position; // 플레이어의 위치를 playerPosition에 저장

            // 로프 발사
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // 마우스의 위치
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity); // 마우스의 위치에 레이 발사

                if (hit.collider != null && hit.collider.CompareTag(targetTag)) // targetTag에 레이가 충돌했는가?
                {
                    photonView.RPC("LaunchRope", RpcTarget.AllBuffered, hit.point);
                }
            }

            // 로프 비활성화
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                photonView.RPC("DisableRope", RpcTarget.AllBuffered);
            }

            // 라인 랜더러의 노드를 동기화
            if (distanceJoint.enabled)
            {
                photonView.RPC("UpdateNode", RpcTarget.AllBuffered, playerPosition);
            }
        }
    }

    // 로프 발사 매서드
    [PunRPC]
    void LaunchRope(Vector2 targetPosition, PhotonMessageInfo info)
    {
        Vector3 node1Pos = targetPosition; // 목표 지점을 node1Pos에 저장

        lineRenderer.SetPosition(0, node1Pos); // node1Pos의 위치를 라인 랜더러의 노드로 저장

        distanceJoint.connectedAnchor = node1Pos; // 디스턴스 조인트의 앵커를 node1Pos으로 설정

        // 라인 랜더러와 디스턴스 조인트를 활성화
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
    }

    // 로프 비활성화 매서드
    [PunRPC]
    void DisableRope()
    {
        // 디스턴스 조인트와 라인 랜더러를 비화성화
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // 노드 갱신 매서드
    [PunRPC]
    void UpdateNode(Vector3 position)
    {
        lineRenderer.SetPosition(1, position); // 2번째 노드 (플레이어 위치의 노드)의 위치를 갱신
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
