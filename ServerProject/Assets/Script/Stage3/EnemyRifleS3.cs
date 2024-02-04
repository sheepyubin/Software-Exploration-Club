using UnityEngine;
using System.Collections.Generic;

public class EnemyRifleS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // 플레이어 감지 범위
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 1f; // 발사 속도
    public int poolSize = 3; // 풀 크기

    private Transform player; // 플레이어 위치
    private float nextFireTime; // 다음 발사 시간
    private List<GameObject> bulletPool; // 총알 풀

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Player의 트랜스폼

        // 풀 초기화
        InitializeBulletPool();

        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
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

    // 풀 초기화
    void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            // 총알 생성 및 초기화
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform); // 총알은 적의 자식 오브젝트
            bullet.SetActive(false); // 비활성화

            // 방향 초기화
            SetBulletDirection(bullet);

            bulletPool.Add(bullet);
        }
    }

    void SetBulletDirection(GameObject bullet)
    {
        // 플레이어를 향하도록 방향 설정
        Vector3 targetDirection = player.position - transform.position; // 방향 백터
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 각도 계산 (라디안 -> 각도) 
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Shoot()
    {
        // 비활성화된 총알 사용
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                // 총알 발사 시 방향 설정
                SetBulletDirection(bulletPool[i]);

                bulletPool[i].transform.position = transform.position;
                bulletPool[i].SetActive(true); // 활성화

                return;
            }
        }
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경을 기즈모로 표시
    }
}
