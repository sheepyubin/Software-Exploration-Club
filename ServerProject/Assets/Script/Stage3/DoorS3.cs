using UnityEngine;

public class DoorS3 : MonoBehaviour
{
    // KeyDataS3 스크립터블 오브젝트
    public KeyDataS3 keyData;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 키를 모두 가지고 있다면
            if (keyData != null && keyData.Key1 && keyData.Key2)
            {
                Debug.Log("Success");
            }
            else
            {
                Debug.Log("Fail");
            }
        }
    }
}
