using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandemPlatformSpawner : MonoBehaviour
{
    public GameObject prefab; // 생성할 프리팹
    public Vector2 minPosition; // 최소 위치
    public Vector2 maxPosition; // 최대 위치

    // 랜덤 좌표 생성 및 프리팹 생성 함수
    private void SpawnRandomPrefab()
    {
        // 랜덤한 좌표 생성
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // 프리팹 생성
        Instantiate(prefab, randomPosition, Quaternion.identity);
    }

    // 예시로 매 프레임마다 랜덤 프리팹을 생성하는 Update 함수
    void Update()
    {
        if (Input.GetMouseButton(0)) // 스페이스 키를 누르면
        {
            SpawnRandomPrefab(); // 랜덤 프리팹 생성
        }
    }
}
