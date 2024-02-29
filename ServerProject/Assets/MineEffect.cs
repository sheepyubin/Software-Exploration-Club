using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEffect : MonoBehaviour
{
    public Animator animator;

    private void Update()
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime >= 1f)
        {
            GameObject.Destroy(gameObject);
        }
    }
}
