using UnityEngine;

public class EnemyShootS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // �÷��̾� ���� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireRate = 3f; // �߻� �ӵ�

    private Transform player; // �÷��̾� ��ġ
    private float nextFireTime; // ���� �߻� �ð�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Player�� Ʈ������

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
                    nextFireTime = Time.time + fireRate; // 3�� �� ���� �߻�
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
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity); // ����
        bullet.SetActive(true); // Ȱ��ȭ
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ���� �ݰ��� ������ ǥ��
    }
}
