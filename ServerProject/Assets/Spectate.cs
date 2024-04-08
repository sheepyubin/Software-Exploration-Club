using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectate : MonoBehaviour
{
    public PlayerData playerData;
    private bool isDead;

    private SpriteRenderer sp;
    private Collider2D co;
    private Rigidbody2D rb;

    private void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        co = sp.GetComponent<Collider2D>();
        rb = sp.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isDead = playerData.isDead;

        if (isDead)
        {
            sp.enabled = false;
            co.enabled = false;
            rb.gravityScale = 0f;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 moveVelocity = new Vector2(horizontalInput * 10f, verticalInput * 10f);

            rb.position += moveVelocity * Time.deltaTime;
        }
    }
}
