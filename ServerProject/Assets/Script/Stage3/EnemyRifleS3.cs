using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class EnemyRifleS3 : MonoBehaviourPun
{
    public float detectionRadius = 8f; // 플레이어 감지 반경
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 1f; // 발사 속도
    public float bulletSpeed = 10f; // 총알 속도

    private float nextFireTime; // 다음 발사 시간

    void Start()
    {
        nextFireTime = Time.time + fireRate;
        photonView.OwnershipTransfer = OwnershipOption.Takeover; // 소유권 설정
    }

    void Update()
    {
        // 플레이어 감지
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
                    nextFireTime = Time.time + fireRate; // 다음 발사까지 시간 업데이트
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

    // 발사 로직
    void Shoot(Vector3 bulletDirection)
    {
        // 방향 설정
        Quaternion rotationToTarget = Quaternion.LookRotation(Vector3.forward, bulletDirection) * Quaternion.Euler(0f, 0f, 90f);

        // 네트워크를 통해 총알 생성
        PhotonNetwork.Instantiate(bulletPrefab.name, transform.position, rotationToTarget);
    }

    // 총알 방향 설정
    Vector3 SetBulletDirection(Transform target)
    {
        Vector3 direction;
        if (target != null)
        {
            direction = (target.position - transform.position).normalized;
        }
        else
        {
            // 플레이어가 없으면 오른쪽으로 발사
            direction = transform.right;
        }
        return direction;
    }

    // 디버깅을 위한 가시화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경 표시
    }
}
