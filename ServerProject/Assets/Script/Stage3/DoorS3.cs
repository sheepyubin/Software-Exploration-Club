using UnityEngine;

public class DoorS3 : MonoBehaviour
{
    // KeyDataS3 ��ũ���ͺ� ������Ʈ
    public KeyDataS3 keyData;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Ű�� ��� ������ �ִٸ�
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
