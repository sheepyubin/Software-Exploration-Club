using UnityEngine;
using System.Collections.Generic;

public class EnemyRifleS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // �÷��̾� ���� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireRate = 1f; // �߻� �ӵ�
    public int poolSize = 3; // Ǯ ũ��

    private Transform player; // �÷��̾� ��ġ
    private float nextFireTime; // ���� �߻� �ð�
    private List<GameObject> bulletPool; // �Ѿ� Ǯ

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Player�� Ʈ������

        // Ǯ �ʱ�ȭ
        InitializeBulletPool();

        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        if (bulletPrefab != null)
        {
            if (Time.time >= nextFireTime)
            {
                // �÷��̾� ���� �� �Ѿ� �߻�
                if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
                {
                    Shoot(); // �Ѿ� �߻�
                    nextFireTime = Time.time + fireRate; // ���� �߻������ �ð� ����
                }
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
        // �÷��̾ ���ϵ��� ���� ����
        Vector3 targetDirection = player.position - transform.position; // ���� ����
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // ���� ��� (���� -> ����) 
        bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);
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



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ���� �ݰ��� ������ ǥ��
    }
}
