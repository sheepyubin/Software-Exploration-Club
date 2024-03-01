using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

public class EnemyRifleS3 : MonoBehaviourPun
{
    public float detectionRadius = 8f; // 플레이어 감지 범위
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 1f; // 발사 속도
    public float bulletSpeed = 10f; // 총알 속도

    private float nextFireTime; // 다음 발사 시간

    void Start()
    {
        nextFireTime = Time.time + fireRate;

        // 모든 클라이언트에서 이 오브젝트를 볼 수 있도록 설정
        photonView.OwnershipTransfer = OwnershipOption.Takeover;
    }

    void Update()
    {
        // 타겟 찾기
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
                    nextFireTime = Time.time + fireRate; // 다음 발사까지의 시간 설정
                    
                }
            }
        }
    }

    // 가장 가까운 플레이어 찾기
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
        // 타겟을 향하는 회전을 구하고 -90도를 추가하여 총알이 타겟을 바라보도록 설정합니다.
        Quaternion rotationToTarget = Quaternion.LookRotation(Vector3.forward, bulletDirection) * Quaternion.Euler(0f, 0f, 90f);

        // 총알 생성 및 발사

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
            // 타겟이 없으면 정면 방향으로 설정
            direction = transform.right;
        }
        return direction;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경을 기즈모로 표시
    }
}
