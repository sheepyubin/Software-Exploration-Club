using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher : MonoBehaviourPunCallbacks, IPunObservable
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;

    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("ī�޶� ã�� �� ����");
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
        }
    }

    [PunRPC]
    void LaunchRope(Vector2 mousePos)
    {
        lineRenderer.SetPosition(0, mousePos);
        lineRenderer.SetPosition(1, transform.position);
        distanceJoint.connectedAnchor = mousePos;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
    }

    [PunRPC]
    void DisableRope()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        // ������� ����
    }
}
