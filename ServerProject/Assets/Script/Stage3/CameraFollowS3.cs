using UnityEngine;

public class CameraFollowS3 : MonoBehaviour
{
    public Transform target; // 카메라가 따라다닐 타겟
    public Vector3 offset; // 타겟 오프셋

    private float originalZ;

    void Start()
    {
        originalZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // 카메라 위치 계산
            Vector3 desiredPosition = target.position + offset;

            // 카메라 Z 축 고정
            desiredPosition.z = originalZ;

            // 카메라 위치 업데이트
            transform.position = desiredPosition;
        }
        else
        {
            Debug.LogWarning("Camera target is NULL");
        }
    }
}
