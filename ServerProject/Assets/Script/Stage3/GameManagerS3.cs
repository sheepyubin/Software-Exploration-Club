using UnityEngine;

public class GameManagerS3 : MonoBehaviour
{
    // KeyDataS3 ��ũ���ͺ� ������Ʈ
    public KeyDataS3 keyData;
    void Start()
    {
        // ��ũ���ͺ� ������Ʈ �ʱ�ȭ
        if (keyData != null)
        {
            // �ʱⰪ ����
            keyData.Key1 = false;
            keyData.Key2 = false;
        }
    }
}
