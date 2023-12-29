using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    public float speed = 5.0f; // �̵� �ӵ�
    public float jumpPower;
    public int moveSize = 0;
    public float grappleSpeed = 10f;  // ������ �̵��ϴ� �ӵ�
    public float maxDistance = 10f;   // ���� �ִ� �Ÿ�
    public float rotationSpeed = 5f; // �÷��̾� ȸ�� �ӵ�
    public Rigidbody2D rigid2D;
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(float x)
    {
        // x�� �̵��� x * speed��, y�� �̵��� ���� �ӷ� ��(����� �߷�)

        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }
}
