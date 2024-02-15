using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher_Tutorial : MonoBehaviour
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
        if (mainCamera == null)
            return;

        // ���� �߻�
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            LaunchRope(mousePos);
        }
        // ���� ��Ȱ��ȭ
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            DisableRope();
        }
    }

    void LaunchRope(Vector2 mousePos)
    {
        lineRenderer.SetPosition(0, mousePos);
        lineRenderer.SetPosition(1, transform.position);
        distanceJoint.connectedAnchor = mousePos;
        distanceJoint.enabled = true;
        lineRenderer.enabled = true;
    }

    void DisableRope()
    {
        distanceJoint.enabled = false;
        lineRenderer.enabled = false;
    }
}
