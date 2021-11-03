using System;
using Pathfinding.Ionic.Zip;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb; //Stores attached RigidBody2D
    [SerializeField] private float moveSpeed, turnSpeed; //Stores player speeds

    private void Update()
    {
        PointToMouse();
    }
    
    private void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector2 direction = new Vector2(horizontal, vertical) * (moveSpeed * Time.deltaTime);

        rb.velocity = direction;
    }
    
    private void PointToMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 5.23f;
        
        Debug.Log(mousePosition);
        
        Vector3 objPosition = Camera.main.WorldToScreenPoint(transform.position);
        mousePosition.x = mousePosition.x - objPosition.x;
        mousePosition.y = mousePosition.y - objPosition.y;
 
        float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * turnSpeed * Mathf.Rad2Deg; //Don't ask me how this actually works
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
