using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandemPlatformSpawner : MonoBehaviour
{
    public GameObject prefab; // ������ ������
    public Vector2 minPosition; // �ּ� ��ġ
    public Vector2 maxPosition; // �ִ� ��ġ

    // ���� ��ǥ ���� �� ������ ���� �Լ�
    private void SpawnRandomPrefab()
    {
        // ������ ��ǥ ����
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector2 randomPosition = new Vector2(randomX, randomY);

        // ������ ����
        Instantiate(prefab, randomPosition, Quaternion.identity);
    }

    // ���÷� �� �����Ӹ��� ���� �������� �����ϴ� Update �Լ�
    void Update()
    {
        if (Input.GetMouseButton(0)) // �����̽� Ű�� ������
        {
            SpawnRandomPrefab(); // ���� ������ ����
        }
    }
}
