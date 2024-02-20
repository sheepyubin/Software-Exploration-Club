using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1S1 : MonoBehaviour
{
    public KeyDataS1 keyData;  // 스테이지 1의 KeyData 스크립터블 오브젝트

    void OnTriggerEnter2D(Collider2D other)
    {
        // 플레이어와 충돌
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #1");
            keyData.Key1 = true; // 키 획득
            Destroy(gameObject);
        }
    }
}
