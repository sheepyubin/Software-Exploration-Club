using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyPlatform : MonoBehaviour
{
    [SerializeField]
    float speed = 0.5f;
    [SerializeField]
    float destroyPos = -10.0f;
    void Update()
    {
        transform.Translate(0, -this.speed, 0);
        if (transform.position.y < destroyPos)
        {
            Destroy(gameObject);
        }
    }

}
