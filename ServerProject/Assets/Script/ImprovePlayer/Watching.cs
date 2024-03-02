using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watching : MonoBehaviourPunCallbacks
{
    public PlayerContainer PlayerContainer;

    private void Update()
    {
        if (photonView.IsMine && PlayerContainer.ReturnisDead(photonView.OwnerActorNr))
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0f).normalized;

            transform.position += moveDirection * 7 * Time.deltaTime;
        }
    }
}
