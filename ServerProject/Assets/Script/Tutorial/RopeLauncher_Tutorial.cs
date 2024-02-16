using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeLauncher_Tutorial : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public DistanceJoint2D distanceJoint;
    public Movement_Tutorial movementTutorial;
    public bool step4= false;
    
    private Camera mainCamera;
    private int temp = 0;


    private void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("카메라 찾을 수 없음");
        }
        else
        {
            distanceJoint.enabled = false;
        }

        step4 = false;
    }

    void Update()
    {
        if (mainCamera == null)
            return;

        // 로프 발사
        if (Input.GetKeyDown(KeyCode.Mouse0) && movementTutorial.step3)
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            LaunchRope(mousePos);
            temp++;
        }
        // 로프 비활성화
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            DisableRope();
            if(temp>=2)
            step4 = true; // 튜토리얼 4단계 완료
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
