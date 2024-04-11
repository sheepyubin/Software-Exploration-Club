using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher : MonoBehaviourPunCallbacks, IPunObservable
{
    public LineRenderer lineRenderer; // ���� ������
    public DistanceJoint2D distanceJoint; // ���Ͻ� ����Ʈ 
    public string targetTag = "Grappleable"; // Ÿ�� �±�
    public PlayerData playerData;

    private Camera mainCamera; // ī�޶�
    private Vector3 playerPosition; // �÷��̾��� ��ġ�� �����ϱ� ���� ����

    private void Start()
    {
        // ���� �ʱ�ȭ
        distanceJoint.enabled = false;
        mainCamera = Camera.main;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    void Update()
    {
        if (photonView != null && photonView.IsMine && !playerData.isDead) // ���� �÷��̾��ΰ�?
        {
            playerPosition = transform.position; // �÷��̾��� ��ġ�� playerPosition�� ����

            // ���� �߻�
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition); // ���콺�� ��ġ
                RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, Mathf.Infinity); // ���콺�� ��ġ�� ���� �߻�

                if (hit.collider != null && hit.collider.CompareTag(targetTag)) // targetTag�� ���̰� �浹�ߴ°�?
                {
                    photonView.RPC("LaunchRope", RpcTarget.AllBuffered, hit.point);
                }
            }

            // ���� ��Ȱ��ȭ
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                photonView.RPC("DisableRope", RpcTarget.AllBuffered);
            }

            // ���� �������� ��带 ����ȭ
            if (distanceJoint.enabled)
            {
                photonView.RPC("UpdateNode", RpcTarget.AllBuffered, playerPosition);
            }
        }
    }

    // ���� �߻� �ż���
    [PunRPC]
    void LaunchRope(Vector2 targetPosition, PhotonMessageInfo info)
    {
        Vector3 node1Pos = targetPosition; // ��ǥ ������ node1Pos�� ����

        lineRenderer.SetPosition(0, node1Pos); // node1Pos�� ��ġ�� ���� �������� ���� ����

        distanceJoint.connectedAnchor = node1Pos; // ���Ͻ� ����Ʈ�� ��Ŀ�� node1Pos���� ����

        // ���� �������� ���Ͻ� ����Ʈ�� Ȱ��ȭ
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
    }

    // ���� ��Ȱ��ȭ �ż���
    [PunRPC]
    void DisableRope()
    {
        // ���Ͻ� ����Ʈ�� ���� �������� ��ȭ��ȭ
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }

    // ��� ���� �ż���
    [PunRPC]
    void UpdateNode(Vector3 position)
    {
        lineRenderer.SetPosition(1, position); // 2��° ��� (�÷��̾� ��ġ�� ���)�� ��ġ�� ����
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }
}
