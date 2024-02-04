using UnityEngine;

public class Tank_MG : MonoBehaviour
{
    public Transform target; // 추적할 대상 (예: 다른 게임 오브젝트의 Transform)

    void Update()
    {

        // 대상을 향하는 벡터 계산
        Vector3 directionToTarget = target.position - transform.position;
        directionToTarget.Normalize();

        // 대상을 향하는 각도 계산
        float targetRotationAngle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;

        // 현재 각도와 목표 각도의 차이 계산
        float angleDifference = Mathf.DeltaAngle(transform.eulerAngles.z, targetRotationAngle);

        // 회전 속도
        float rotationSpeed = 0.06f;

        // 실제 회전 각도 계산
        float rotationAmount = Mathf.Clamp(angleDifference, -rotationSpeed, rotationSpeed);

        // 회전 적용
        transform.Rotate(Vector3.forward, rotationAmount);
    }
}
