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

        // Ǯ �ʱ�ȭ
        void InitializeBulletPool()
    {
        bulletPool = new List<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            // �Ѿ� ���� �� �ʱ�ȭ
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform); // �Ѿ��� ���� �ڽ� ������Ʈ
            bullet.SetActive(false); // ��Ȱ��ȭ

            // ���� �ʱ�ȭ
            SetBulletDirection(bullet);

            bulletPool.Add(bullet);
        }
    }

    void SetBulletDirection(GameObject bullet)
    {
        // ���� �������� ����
        bullet.transform.rotation = transform.rotation;
    }

    void Shoot()
    {
        // ��Ȱ��ȭ�� �Ѿ� ���
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (!bulletPool[i].activeInHierarchy)
            {
                // �Ѿ� �߻� �� ���� ����
                SetBulletDirection(bulletPool[i]);

                bulletPool[i].transform.position = transform.position;
                bulletPool[i].SetActive(true); // Ȱ��ȭ

                return;
            }
        }
    }
}