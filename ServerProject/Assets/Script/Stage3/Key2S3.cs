using UnityEngine;

public class Key2S3 : MonoBehaviour
{
    // KeyDataS3 스크립터블 오브젝트
    public KeyDataS3 keyData;

    // 플레이어와 충돌
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #2");
            keyData.Key2 = true; // 키 획득
            Destroy(gameObject);
        }
    }
}
