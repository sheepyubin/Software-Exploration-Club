using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public GameObject platformPrefab; // ������ ������

    public GameObject gearObject;
    public Animator animator;
    
    public Vector2 minPosition; // �ּ� ��ġ
    public Vector2 maxPosition; // �ִ� ��ġ
    
    float delta = 0.0f;
    public float span = 1.0f;
    public float speed = 1.0f;

    private void Start()
    {
    }

    // ���� ��ǥ ���� �� ������ ���� �Լ�
    private void SpawnRandomPrefab()
    {
        // ������ ��ǥ ����
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector3 randomPosition = new Vector3(randomX, randomY);

        // ������ ����
        Instantiate(platformPrefab, transform.position + randomPosition, Quaternion.identity);
    }

    // ���÷� �� �����Ӹ��� ���� �������� �����ϴ� Update �Լ�
    void Update()
    {
        this.delta += Time.deltaTime;
        this.speed += Time.deltaTime / 2;
        if (delta > span)
        {
            delta = 0.0f;
            SpawnRandomPrefab(); // ���� ������ ����
        }
        transform.Translate(0, this.speed * Time.deltaTime, 0);

        gearObject.transform.Translate(0, this.speed * Time.deltaTime, 0);
        animator.SetFloat("isRotationSpeed", this.speed / 2);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerContainer.AddPlayerisDead(PhotonNetwork.LocalPlayer.UserId, true);
        }
    }
}
