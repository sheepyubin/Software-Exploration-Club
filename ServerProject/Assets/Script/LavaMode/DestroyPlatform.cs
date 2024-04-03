using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    [SerializeField]
    float destroyTime = 10.0f;
    float delta = 0.0f;

    private Renderer rend; // 오브젝트의 랜더러
    private Color originalColor; // 오브젝트의 초기 색상

    void Start()
    {
        Destroy(gameObject, destroyTime);
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void Update()
    {
        delta += Time.deltaTime;

        if (delta < destroyTime)
        {
            // 시간이 흐름에 따라 색 변화
            float t = delta / destroyTime; // 정규화된 시간 (0에서 1까지)

            // 현재 시간에 해당하는 색상 계산
            Color currentColor = Color.Lerp(originalColor, Color.clear, t);

            // 오브젝트의 색상을 변경
            rend.material.color = currentColor;
        }
    }
}
