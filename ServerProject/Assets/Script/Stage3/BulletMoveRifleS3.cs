using UnityEngine;

public class BulletMoveRifleS3 : MonoBehaviour
{
    public float speed = 10f; // ź��
    public float damageRadius = 1f; // �ǰ� �ݰ�

    private Transform target; // �÷��̾� ��ġ
    private Vector3 moveDirection; // �̵� ����

    void Start()
    {
        Invoke("Destroy", 3f);
        // Ÿ�� ����
        FindPlayerTarget();

        // Ÿ���� NULL�̶��
        if (target == null)
        {
            Debug.LogWarning("Bullet target is NULL!.");
            return;
        }

        // Ÿ�� �������� ����
        moveDirection = (target.position - transform.position).normalized;


    }

    void Update()
    {
        // �̵�
        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);
    }

    // �÷��̾� Ž��
    void FindPlayerTarget()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }
    }

    // �÷��̾� �ǰ�
    void Damage()
    {
        Debug.Log("Damage(Rifle)");
        Destroy(gameObject);
    }

    // ������Ʈ �ı�
    void Destroy()
    {
        Destroy(gameObject);
    }

    // �浹 ó��
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Damage();
        }
    }
}
