using UnityEngine;

public class CameraFollowS3 : MonoBehaviour
{
    public Transform target; // ī�޶� ����ٴ� Ÿ��
    public Vector3 offset; // Ÿ�� ������

    private float originalZ;

    void Start()
    {
        originalZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // ī�޶� ��ġ ���
            Vector3 desiredPosition = target.position + offset;

            // ī�޶� Z �� ����
            desiredPosition.z = originalZ;

            // ī�޶� ��ġ ������Ʈ
            transform.position = desiredPosition;
        }
        else
        {
            Debug.LogWarning("Camera target is NULL");
        }
    }
}
