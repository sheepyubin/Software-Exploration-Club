using UnityEngine;
using System.Collections;

public class Rope : MonoBehaviour
{
    private bool isColled = false;

    public IEnumerator ExtendRope(Vector2 direction, Vector2 launchPosition, float ropeSpeed, float maxRopeLength)
    {
        float currentRopeLength = 0f;
        float elapsedTime = 0f;

        while (currentRopeLength < maxRopeLength)
        {
            // �浹 ����
            if (isColled)
            {
                break; // �浹�� �����Ǹ� while ���� ����
            }

            // ���� �ø���
            elapsedTime += Time.deltaTime;
            currentRopeLength = Mathf.Lerp(0f, maxRopeLength, elapsedTime * ropeSpeed);

            // ���� ������ ����
            transform.localScale = new Vector3(0.5f, currentRopeLength, 1f);

            // ������ �� �κ� ��ġ ����
            Vector2 endPoint = launchPosition + direction * currentRopeLength * 0.5f;
            transform.position = endPoint;

            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Ground �±׿� ����� ��
        if (other.CompareTag("Ground"))
        {
            isColled = true;
        }
    }
}
