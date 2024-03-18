using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1S1 : MonoBehaviour
{
    public Stage1Data data;  // Stage1Data 스크립터블 오브젝트

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #1");
            data.Setkey1(true); // 키 획득
            Destroy(gameObject);
        }
    }
}
