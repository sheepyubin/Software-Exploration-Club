using UnityEngine;
using Photon.Pun;

public class MissileSpawner : MonoBehaviourPun
{
    public GameObject missilePrefab; // 생성할 미사일 프리팹
    public float minY = 0f; // Y 좌표의 최소값
    public float maxY = 0f; // Y 좌표의 최대값
    public float X = 0;
    public float spawnInterval = 1f; // 생성 간격

    void Start()
    {
        // Master 클라이언트만 SpawnMissile 함수를 호출
        if (PhotonNetwork.IsMasterClient)
            InvokeRepeating("SpawnMissile", 0f, spawnInterval);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(X, minY, 1), new Vector3(X, maxY, 1));
    }

    void SpawnMissile()
    {
        // 랜덤한 X와 Z 좌표 선택
        float randomX = Random.Range(-10f, 10f); // X 좌표 범위를 조절하세요
        float Z = 10;

        // 랜덤한 Y 좌표 선택
        float randomY = Random.Range(minY, maxY);

        // 선택된 좌표로 미사일 생성
        Vector3 spawnPosition = new Vector3(X, randomY, Z);
        PhotonNetwork.Instantiate(missilePrefab.name, spawnPosition, Quaternion.identity);
    }
}
