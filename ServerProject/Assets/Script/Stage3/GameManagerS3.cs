using UnityEngine;

public class GameManagerS3 : MonoBehaviour
{
    // KeyDataS3 스크립터블 오브젝트
    public KeyDataS3 keyData;
    void Start()
    {
        // 스크립터블 오브젝트 초기화
        if (keyData != null)
        {
            // 초기값 설정
            keyData.Key1 = false;
            keyData.Key2 = false;
        }
    }
}
