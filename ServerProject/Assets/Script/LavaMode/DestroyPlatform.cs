using UnityEngine;

public class DestroyPlatform : MonoBehaviour
{
    [SerializeField]
    float destroyTime = 10.0f;
    float delta = 0.0f;

    private Renderer rend; // ������Ʈ�� ������
    private Color originalColor; // ������Ʈ�� �ʱ� ����

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
            // �ð��� �帧�� ���� �� ��ȭ
            float t = delta / destroyTime; // ����ȭ�� �ð� (0���� 1����)

            // ���� �ð��� �ش��ϴ� ���� ���
            Color currentColor = Color.Lerp(originalColor, Color.clear, t);

            // ������Ʈ�� ������ ����
            rend.material.color = currentColor;
        }
    }
}
