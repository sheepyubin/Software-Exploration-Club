using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher : MonoBehaviourPunCallbacks, IPunObservable
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    private Camera mainCamera;
    private Vector3 playerPosition; // �÷��̾��� ��ġ�� �����ϱ� ���� ����

    private void Start()
    {
        distanceJoint.enabled = false;

        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("ī�޶� ã�� �� �����ϴ�.");
        }
        else
        {
            distanceJoint.enabled = false;
        }
    }

    void Update()
    {
        if (photonView != null && photonView.IsMine) // �ڽ��� ĳ���͸� ����
        {
            playerPosition = transform.position;
            if (mainCamera == null)
                return;

            // ���� �߻�
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
                photonView.RPC("LaunchRope", RpcTarget.AllBuffered, mousePos);
            }
            // ���� ��Ȱ��ȭ
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
        Vector3 node2Pos = playerPosition; // �÷��̾��� ��ġ�� ������Ʈ
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

    // IPunObservable �������̽� ����
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ������� ����
    }
}
