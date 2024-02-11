using UnityEngine;
using System.Collections;

public class RopeLauncher : MonoBehaviour
{
    public GameObject ropePrefab; // ���� ������
    public float ropeSpeed = 5f; // ���� �þ�� �ӵ�
    public float maxRopeLength = 10f; // ���� �ִ� ����

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // ���� ��ǥ ��ȯ
            Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            // ���� �߻�
            LaunchRope(mousePosition);
        }
    }

    void LaunchRope(Vector2 targetPosition)
    {
        // �÷��̾� ��ġ���� �߻�
        Vector2 launchPosition = transform.position;
        // �߻� ���� ���
        Vector2 direction = (targetPosition - launchPosition).normalized;

        // ���� ����
        GameObject newRope = Instantiate(ropePrefab, launchPosition, Quaternion.identity);
        // ���� ���� ����
        newRope.transform.up = direction;
        // ���� ���� ����
        float initialRopeLength = 0f;
        newRope.transform.localScale = new Vector3(0.5f, initialRopeLength, 1f);

        // ���� ��ũ��Ʈ ��������
        Rope ropeScript = newRope.GetComponent<Rope>();
        if (ropeScript != null)
        {
            // ���� �ڵ忡 �ִ� ExtendRope �ڷ�ƾ ȣ��
            StartCoroutine(ropeScript.ExtendRope(direction, launchPosition, ropeSpeed, maxRopeLength));
        }
        else
        {
            Debug.LogWarning("Rope component not found on the rope object.");
        }
    }
}
