using UnityEngine;

public class EnemyRifleS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // 플레이어 감지 범위
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 1f; // 발사 속도

    private Transform target; // 타겟
    private float nextFireTime; // 다음 발사 시간

    void Start()
    {
        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        // 타겟 찾기
        bool isTargetInRange = FindTarget();

        if (bulletPrefab != null && isTargetInRange)
        {
            if (Time.time >= nextFireTime)
            {
                // 총알 발사
                Shoot();
                nextFireTime = Time.time + fireRate; // 다음 발사까지의 시간 설정
            }
        }
    }

    // 타겟 찾기
    bool FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToPlayer <= detectionRadius)
            {
                return true; // 플레이어와의 거리가 감지 범위 내에 있으면 true 반환
            }
        }

        return false; // 플레이어와의 거리가 감지 범위 내에 없으면 false 반환
    }

    void Shoot()
    {
        // 타겟을 향하는 방향 벡터 구하기
        Vector3 bulletDirection = SetBulletDirection();

        // 타겟을 향하는 회전을 구하고 -90도를 추가하여 총알이 타겟을 바라보도록 설정합니다.
        Quaternion rotationToTarget = Quaternion.LookRotation(Vector3.forward, bulletDirection) * Quaternion.Euler(0f, 0f, 90f);

        // 총알 생성 및 발사
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotationToTarget);
    }



    Vector3 SetBulletDirection()
    {
        Vector3 direction;
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
        else
        {
            // 타겟이 없으면 정면 방향으로 설정
            direction = transform.right;
        }
        return direction;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경을 기즈모로 표시
    }
}
