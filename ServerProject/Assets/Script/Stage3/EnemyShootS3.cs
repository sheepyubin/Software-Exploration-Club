using UnityEngine;

public class EnemyShootS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // 플레이어 감지 범위
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 3f; // 발사 속도

    private Transform player; // 플레이어 위치
    private float nextFireTime; // 다음 발사 시간

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Player의 트랜스폼

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
                    nextFireTime = Time.time + fireRate; // 3초 후 다음 발사
                }
            }
        }
        else
        {
            Debug.LogWarning("Enemy bulletPrefab is NULL");
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // 생성
        bullet.SetActive(true); // 활성화
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경을 기즈모로 표시
    }
}
