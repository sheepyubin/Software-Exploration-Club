using UnityEngine;

public class Key2S3 : MonoBehaviour
{
    // KeyDataS3 ��ũ���ͺ� ������Ʈ
    public KeyDataS3 keyData;

    // �÷��̾�� �浹
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Get Key #2");
            keyData.Key2 = true; // Ű ȹ��
            Destroy(gameObject);
        }
    }
}
