using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DestroyPlatform : MonoBehaviour
{
    [SerializeField]
    GameObject platform;
    [SerializeField]
    float lifeTime = 3f;
    void Start()
    {
        Destroy(platform, lifeTime);
    }

}
