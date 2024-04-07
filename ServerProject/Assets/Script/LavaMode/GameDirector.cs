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
    public float speed = 3.0f;

    bool moveMode;
    public float modeChangeTime = 3.0f;

    string userID;
    bool isDead;
    private void Start()
    {
        userID = PhotonNetwork.LocalPlayer.UserId;
        isDead = false;
        moveMode = false;
        maxPosition.y = minPosition.y + 10;
        playerContainer.AddPlayerisDead(userID, isDead);
    }
    private void Awake()
    {
        StartCoroutine("SwichingMode");
    }
    private IEnumerator SwichingMode()
    {
        while (true)
        {
            
            if (moveMode)
            {
                moveMode = false;
                maxPosition.y = minPosition.y + 10;
            }
            else
            {
                moveMode = true;
                maxPosition.y = minPosition.y;
            }
            yield return new WaitForSeconds(modeChangeTime);
        
        }
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
    private void MoveMode(bool mode)
    {
        if (mode)
        {
            Movement(this.speed);
        }
        else
        {
            Movement(0);
        }

    }
    private void Movement(float speed)
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
        gearObject.transform.Translate(0, speed * Time.deltaTime, 0);
        animator.SetFloat("isRotationSpeed", speed);
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
        MoveMode(moveMode);
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