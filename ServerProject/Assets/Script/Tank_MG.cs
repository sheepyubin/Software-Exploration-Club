using UnityEngine;

public class Tank_MG : MonoBehaviour
{
    public Transform target; // ������ ��� (��: �ٸ� ���� ������Ʈ�� Transform)

    void Update()
    {

        // ����� ���ϴ� ���� ���
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.Normalize();

        // ����� ���ϴ� ���� ���
        float targetRotationAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // ���� ������ ��ǥ ������ ���� ���
        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.z, targetRotationAngle);

        // ȸ�� �ӵ�
        float rotationSpeed = 0.06f;

        // ���� ȸ�� ���� ���
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationSpeed, rotationSpeed);

        // ȸ�� ����
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}
