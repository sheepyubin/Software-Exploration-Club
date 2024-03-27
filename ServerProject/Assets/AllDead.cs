using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDead : MonoBehaviour
{
    public PlayerContainer playerContainer;


    private void Update()
    {
        if (playerContainer.ReturnPlayerisDeadAll())
        {

        }
    }
}
