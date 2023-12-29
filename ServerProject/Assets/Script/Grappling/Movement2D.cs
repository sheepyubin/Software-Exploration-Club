using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    public float speed = 5.0f; // 이동 속도
    public float jumpPower;
    public int moveSize = 0;
    public float grappleSpeed = 10f;  // 훅으로 이동하는 속도
    public float maxDistance = 10f;   // 훅의 최대 거리
    public float rotationSpeed = 5f; // 플레이어 회전 속도
    public Rigidbody2D rigid2D;
    private void Awake()
    {
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(float x)
    {
        // x축 이동은 x * speed로, y축 이동은 기존 속력 값(현재는 중력)

        rigid2D.velocity = new Vector2(x * speed, rigid2D.velocity.y);
    }
}
