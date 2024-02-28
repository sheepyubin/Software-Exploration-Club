using Photon.Pun;
using UnityEngine;

public class EnemyShotgunS3 : MonoBehaviourPun
{
    public float detectionRadius = 8f; // 플레이어 감지 범위
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 2f; // 발사 속도
    public int ammo = 5; // 총알 갯수

    private Transform player; // 플레이어 위치
    private float nextFireTime; // 다음 발사 시간

    void Start()
    {
        nextFireTime = Time.time + fireRate;

        // 모든 클라이언트에서 이 오브젝트를 볼 수 있도록 설정
        photonView.OwnershipTransfer = OwnershipOption.Takeover;
    }

    void Update()
    {
        // 타겟 찾기
        player = FindClosestPlayer().transform;


        if (bulletPrefab != null)
        {
            if (Time.time >= nextFireTime)
            {
                // 플레이어 감지 후 총알 발사
                if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
                {
                    Shoot(); // 총알 발사
                    nextFireTime = Time.time + fireRate; // 다음 발사까지의 시간 설정
                }
            }
        }
        else
        {
            Debug.LogWarning("Enemy bulletPrefab is NULL");
        }
    }

    // 가장 가까운 플레이어 찾기
    GameObject FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;
        foreach (GameObject player in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < closestDistance)
            {
                closestPlayer = player;
                closestDistance = distanceToPlayer;
            }
        }
        return closestPlayer;
    }

    void Shoot()
    {
        Vector3 targetDirection = player.position - transform.position; // 방향 백터 계산
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 각도 계산 (라디안 -> 각도) 

        // 총알 프리팹 5개 생성
        for (int i = 0; i < ammo; i++)
        {
            // 각도 계산
            float bulletAngle = angle - 30f + i * 15f;

            // 각도 설정
            Quaternion rotation = Quaternion.Euler(0f, 0f, bulletAngle);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.SetActive(true); // 활성화
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경을 기즈모로 표시
    }
}
