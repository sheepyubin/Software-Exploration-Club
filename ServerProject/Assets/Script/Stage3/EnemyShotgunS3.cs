using Photon.Pun;
using UnityEngine;

public class EnemyShotgunS3 : MonoBehaviourPun
{
    public float detectionRadius = 8f; // �÷��̾� ���� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireRate = 2f; // �߻� �ӵ�
    public int ammo = 5; // �Ѿ� ����

    private Transform player; // �÷��̾� ��ġ
    private float nextFireTime; // ���� �߻� �ð�

    void Start()
    {
        nextFireTime = Time.time + fireRate;

        // ��� Ŭ���̾�Ʈ���� �� ������Ʈ�� �� �� �ֵ��� ����
        photonView.OwnershipTransfer = OwnershipOption.Takeover;
    }

    void Update()
    {
        // Ÿ�� ã��
        player = FindClosestPlayer().transform;


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

    // ���� ����� �÷��̾� ã��
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
        Vector3 targetDirection = player.position - transform.position; // ���� ���� ���
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // ���� ��� (���� -> ����) 

        // �Ѿ� ������ 5�� ����
        for (int i = 0; i < ammo; i++)
        {
            // ���� ���
            float bulletAngle = angle - 30f + i * 15f;

            // ���� ����
            Quaternion rotation = Quaternion.Euler(0f, 0f, bulletAngle);

            GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
            bullet.SetActive(true); // Ȱ��ȭ
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // ���� �ݰ��� ������ ǥ��
    }
}
