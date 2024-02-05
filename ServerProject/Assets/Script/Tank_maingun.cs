using UnityEngine;
using System.Collections.Generic;

public class Tank_maingun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 3;

    private float nextFireTime;
    private List<GameObject> bulletPool;
        void Start()
        {

            InitializeBulletPool();

            nextFireTime = Time.time + 7f;
        }

        void Update()
        {
            if (bulletPrefab != null)
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 7f;
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
        // 현재 방향으로 설정
        bullet.transform.rotation = transform.rotation;
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
}