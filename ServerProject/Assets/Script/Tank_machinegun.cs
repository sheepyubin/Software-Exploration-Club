using UnityEngine;
using System.Collections.Generic;

public class Tank_machinegun : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int poolSize = 5;

    private float nextFireTime;
    private List<GameObject> bulletPool;
        void Start()
        {

            InitializeBulletPool();

            nextFireTime = Time.time + 0.1f;
        }

        void Update()
        {
            if (bulletPrefab != null)
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot();
                    nextFireTime = Time.time + 0.1f;
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
        // ���� ��ũ�� �������� ����
        bullet.transform.rotation = transform.rotation;

        // �Ѿ��� ��ũ�� �������� ���ư����� ����
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = bullet.transform.up * 30; // yourBulletSpeed���� ���ϴ� �ӵ��� �־��ּ���.
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