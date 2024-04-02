using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mineModeManager : MonoBehaviour
{
    public GameObject PlayerContainer;
    public PlayerContainer playerContainer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool isdead = playerContainer.ReturnPlayerisDeadAll();

        if (isdead==true) {
            
        }
    }

}
