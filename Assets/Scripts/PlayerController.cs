using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; //Stores attached RigidBody2D
    [SerializeField] private float moveSpeed, turnSpeed; //Stores player speeds

    [SerializeField] private Animator anim;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical) * (moveSpeed * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            anim.SetBool("Walking", true);
        }
        else
        {
            anim.SetBool("Walking", false);
        }

        rb.velocity = direction;
    }
}
