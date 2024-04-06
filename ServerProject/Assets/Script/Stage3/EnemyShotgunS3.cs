using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;

public class EnemyShotgunS3 : MonoBehaviourPun
{

    AudioSource audioSource;
    public float detectionRadius = 8f; // 플레이어 감지 반경
    public GameObject bulletPrefab; // 총알 프리팹
    public float fireRate = 2f; // 발사 속도
    public int ammo = 5; // 총알 개수

    private Transform player; // 플레이어 위치
    private float nextFireTime; // 다음 발사 시간

    void Start()
    {
        nextFireTime = Time.time + fireRate;

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.mute = false;

        photonView.OwnershipTransfer = OwnershipOption.Takeover; // 소유권 설정
    }

    void Update()
    {
        player = FindClosestPlayer().transform;

        if (bulletPrefab != null)
        {
            if (Time.time >= nextFireTime)
            {
                // 플레이어가 감지 범위 내에 있을 때만 발사
                if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
                {
                    photonView.RPC("ShootRPC", RpcTarget.All); // RPC 호출
                    nextFireTime = Time.time + fireRate; // 다음 발사 시간 설정
                }
            }
        }
        else
        {
            Debug.LogWarning("Enemy bulletPrefab is NULL");
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

    [PunRPC] // RPC 메서드
    void ShootRPC()
    {
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySfx(AudioManager.Sfx.ShotGun);

            if (player != null)
            {
                Vector3 targetDirection = player.position - transform.position; // 플레이어 방향
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg; // 플레이어와의 각도 (라디안 -> 각도)

                // 총알을 5번 발사
                for (int i = 0; i < ammo; i++)
                {
                    // 총알의 각도 설정
                    float bulletAngle = angle - 30f + i * 15f;

                    // 총알의 회전값
                    Quaternion rotation = Quaternion.Euler(0f, 0f, bulletAngle);

                    // 네트워크를 통해 총알 생성
                    GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, transform.position, rotation);
                    if (bullet != null)
                    {
                        bullet.SetActive(true); // 활성화
                    }
                    else
                    {
                        Debug.LogError("Failed to instantiate bullet prefab.");
                    }
                }
            }
            else
            {
                Debug.LogError("Player is not assigned.");
            }
        }
        else
        {
            Debug.LogError("AudioManager instance is not assigned.");
        }
    }

    // 디버깅을 위한 가시화
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius); // 감지 반경 표시
    }
}
