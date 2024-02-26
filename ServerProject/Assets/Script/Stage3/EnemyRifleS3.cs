using UnityEngine;

public class EnemyRifleS3 : MonoBehaviour
{
    public float detectionRadius = 8f; // �÷��̾� ���� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireRate = 1f; // �߻� �ӵ�

    private Transform target; // Ÿ��
    private float nextFireTime; // ���� �߻� �ð�

    void Start()
    {
        nextFireTime = Time.time + fireRate;
    }

    void Update()
    {
        // Ÿ�� ã��
        bool isTargetInRange = FindTarget();

        if (bulletPrefab != null && isTargetInRange)
        {
            if (Time.time >= nextFireTime)
            {
                // �Ѿ� �߻�
                Shoot();
                nextFireTime = Time.time + fireRate; // ���� �߻������ �ð� ����
            }
        }
    }

    // Ÿ�� ã��
    bool FindTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToPlayer <= detectionRadius)
            {
                return true; // �÷��̾���� �Ÿ��� ���� ���� ���� ������ true ��ȯ
            }
        }

        return false; // �÷��̾���� �Ÿ��� ���� ���� ���� ������ false ��ȯ
    }

    void Shoot()
    {
        // Ÿ���� ���ϴ� ���� ���� ���ϱ�
        Vector3 bulletDirection = SetBulletDirection();

        // Ÿ���� ���ϴ� ȸ���� ���ϰ� -90���� �߰��Ͽ� �Ѿ��� Ÿ���� �ٶ󺸵��� �����մϴ�.
        Quaternion rotationToTarget = Quaternion.LookRotation(Vector3.forward, bulletDirection) * Quaternion.Euler(0f, 0f, 90f);

        // �Ѿ� ���� �� �߻�
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
            // Ÿ���� ������ ���� �������� ����
            direction = transform.right;
        }
        return direction;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ���� �ݰ��� ������ ǥ��
    }
}
