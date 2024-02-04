using UnityEngine;

public class EnemyRifleS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // �÷��̾� ���� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireRate = 1f; // �߻� �ӵ�

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
        Instantiate(bulletPrefab, transform.position, Quaternion.identity); // ����
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ���� �ݰ��� ������ ǥ��
    }
}
