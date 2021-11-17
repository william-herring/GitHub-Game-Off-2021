using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; //Stores attached RigidBody2D
    [SerializeField] private float moveSpeed; //Stores player speed

    [SerializeField] private Animator anim;
    [SerializeField] private Gun playerGun;

    private readonly Dictionary<Vector2, int> walkingStates = new Dictionary<Vector2, int>(); //Maps vector inputs to the z axis rotation

    private void Awake()
    {
        //Setting dictionary items
        walkingStates.Add(Vector2.up, 0);
        walkingStates.Add(Vector2.down, 180);
        walkingStates.Add(Vector2.right, -90);
        walkingStates.Add(Vector2.left, 90);
        walkingStates.Add(new Vector2(1, 1), -45);
        walkingStates.Add(new Vector2(-1, 1), 45);
        walkingStates.Add(new Vector2(1, -1), 225);
        walkingStates.Add(new Vector2(-1, -1), 135);
    }

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical) * (moveSpeed * Time.deltaTime);
        rb.velocity = direction;

        anim.SetBool("Walking", direction != Vector2.zero);

        if (direction != Vector2.zero)
        {
            transform.rotation = Quaternion.Euler(0, 0, walkingStates[new Vector2(horizontal, vertical)]);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("BulletCollectable")) return; //Reduces nesting
        
        playerGun.ammunition += 1;
        Destroy(other.gameObject);
    }
}
