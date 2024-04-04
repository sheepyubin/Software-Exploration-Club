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
    public float span = 2.0f;
    float originalTime;
    public float speed = 2.0f;
    float returnSpeed = 20.0f;

    string userID;
    bool isDead;
    private void Start()
    {
        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;
        playerContainer.AddPlayerisDead(userID, isDead);

        originalTime = span;
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
        if (delta > span)
        {
            delta = 0.0f;
            SpawnRandomPrefab(); // ���� ������ ����
        }
        transform.Translate(0, this.speed * Time.deltaTime, 0);

        gearObject.transform.Translate(0, this.speed * Time.deltaTime, 0);
        animator.SetFloat("isRotationSpeed", this.speed / 2);

        this.speed += Time.deltaTime / 10;
        if (speed > returnSpeed)
        {
            span -= 0.1f;
            returnSpeed += 10.0f;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("dead");
            playerContainer.AddPlayerisDead(userID, isDead);
            playerContainer.ReturnPlayerisDead(userID);
        }
    }
}
