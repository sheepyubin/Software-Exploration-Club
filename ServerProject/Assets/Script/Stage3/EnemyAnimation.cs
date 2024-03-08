using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    private Animator ani;
    private SpriteRenderer sp;
    float currentX;

    private void Start()
    {
        ani = GetComponent<Animator>();
        sp = GetComponent<SpriteRenderer>();

        currentX = transform.position.x;
    }

    private void Update()
    {
        float newX = transform.position.x;

        if (newX != currentX)
            ani.SetBool("isWalk", true);
        else
            ani.SetBool("isWalk", false);

        if (newX < currentX)
            sp.flipX= true;
        else
            sp.flipX= false;

        currentX = newX;
    }
}
