using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watching : MonoBehaviourPunCallbacks
{
    public PlayerContainer PlayerContainer;
    public Follow follow;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (photonView.IsMine && PlayerContainer.ReturnisDead(photonView.OwnerActorNr))
        {
            follow.isDead = true;

            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            Vector2 moveVelocity;

            moveVelocity = new Vector2(horizontalInput * 10f, verticalInput * 10f);
            
            rb.velocity = moveVelocity;
        }
    }
}