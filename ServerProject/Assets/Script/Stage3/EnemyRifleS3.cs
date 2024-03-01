using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class EnemyRifleS3 : MonoBehaviourPun
{
    public float detectionRadius = 8f; // �÷��̾� ���� ����
    public GameObject bulletPrefab; // �Ѿ� ������
    public float fireRate = 1f; // �߻� �ӵ�
    public float bulletSpeed = 10f; // �Ѿ� �ӵ�

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
        GameObject closestPlayer = FindClosestPlayer();

        if (closestPlayer != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, closestPlayer.transform.position);
            if (bulletPrefab != null && distanceToPlayer <= detectionRadius)
            {
                if (Time.time >= nextFireTime)
                {
                    Shoot(SetBulletDirection(closestPlayer.transform));
                    AudioManager.instance.PlaySfx(AudioManager.Sfx.Rifle);
                    nextFireTime = Time.time + fireRate; // ���� �߻������ �ð� ����
                    
                }
            }
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

    void Shoot(Vector3 bulletDirection)
    {
        // Ÿ���� ���ϴ� ȸ���� ���ϰ� -90���� �߰��Ͽ� �Ѿ��� Ÿ���� �ٶ󺸵��� �����մϴ�.
        Quaternion rotationToTarget = Quaternion.LookRotation(Vector3.forward, bulletDirection) * Quaternion.Euler(0f, 0f, 90f);

        // �Ѿ� ���� �� �߻�

        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotationToTarget);

    }

    Vector3 SetBulletDirection(Transform target)
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
