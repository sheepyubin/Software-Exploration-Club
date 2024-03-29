using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandemPlatformSpawner : MonoBehaviour
{
    public GameObject prefab; // ������ ������
    public Vector2 minPosition; // �ּ� ��ġ
    public Vector2 maxPosition; // �ִ� ��ġ
    float delta = 0.0f;
    public float span = 1.0f;
    // ���� ��ǥ ���� �� ������ ���� �Լ�
    private void SpawnRandomPrefab()
    {
        // ������ ��ǥ ����
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector2 randomPosition = new Vector3(randomX, randomY, 0);

        // ������ ����
        GameObject gear = Instantiate(prefab);
        gear.transform.position = new UnityEngine.Vector3(transform.position.x + randomX, transform.position.y + randomY, -1);
        gear.transform.rotation = Quaternion.identity;
    }

    // ���÷� �� �����Ӹ��� ���� �������� �����ϴ� Update �Լ�
    void Update()
    {
        this.delta += Time.deltaTime;
        if (delta > span)
        {
            delta = 0.0f;
            SpawnRandomPrefab(); // ���� ������ ����
        }
    }
}
