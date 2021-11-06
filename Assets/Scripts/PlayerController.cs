using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; //Stores attached RigidBody2D
    [SerializeField] private float moveSpeed, turnSpeed; //Stores player speeds

    [SerializeField] private Animator anim;

    private readonly Dictionary<Vector2, string> walkingStates = new Dictionary<Vector2, string>();

    private void Awake()
    {
        //Setting dictionary items
        walkingStates.Add(Vector2.up, "north");
        walkingStates.Add(Vector2.down, "south");
        walkingStates.Add(Vector2.right, "east");
        walkingStates.Add(Vector2.left, "west");
        walkingStates.Add(new Vector2(1, 1), "northEast");
        walkingStates.Add(new Vector2(-1, 1), "northWest");
        walkingStates.Add(new Vector2(1, -1), "southEast");
        walkingStates.Add(new Vector2(-1, -1), "southWest");
    }
    private Vector2 currentState = Vector2.up;

    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical) * (moveSpeed * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            if (currentState != new Vector2(horizontal, vertical))
            {
                anim.SetBool(walkingStates[currentState], false);
            }
            
            anim.SetBool(walkingStates[new Vector2(horizontal, vertical)], true);

            currentState = new Vector2(horizontal, vertical);
        }
        else
        {
            foreach (var keys in walkingStates.Values)
            {
                anim.SetBool(keys, false);
            }
        }

        rb.velocity = direction;
    }
}
