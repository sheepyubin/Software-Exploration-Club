using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public PlayerContainer playerContainer;
    public GameObject platformPrefab; // 생성할 프리팹

    public GameObject gearObject;
    public Animator animator;
    
    public Vector2 minPosition; // 최소 위치
    public Vector2 maxPosition; // 최대 위치
    
    float delta = 0.0f;
    public float span = 1.0f;
    public float speed = 1.0f;

    private void Start()
    {
    }

    // 랜덤 좌표 생성 및 프리팹 생성 함수
    private void SpawnRandomPrefab()
    {
        // 랜덤한 좌표 생성
        float randomX = Random.Range(minPosition.x, maxPosition.x);
        float randomY = Random.Range(minPosition.y, maxPosition.y);
        Vector3 randomPosition = new Vector3(randomX, randomY);

        // 프리팹 생성
        Instantiate(platformPrefab, transform.position + randomPosition, Quaternion.identity);
    }

    // 예시로 매 프레임마다 랜덤 프리팹을 생성하는 Update 함수
    void Update()
    {
        this.delta += Time.deltaTime;
        this.speed += Time.deltaTime / 2;
        if (delta > span)
        {
            delta = 0.0f;
            SpawnRandomPrefab(); // 랜덤 프리팹 생성
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
