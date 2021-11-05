using System;
using Pathfinding;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameManager gm;

    private Collider2D targetCollider;

    private void Start()
    {
        targetCollider = GetComponent<AIDestinationSetter>().target.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        GetComponent<AIPath>().maxSpeed = IsTargetInRange() ? 0 : 2;
    }
    
    private bool IsTargetInRange()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 4.5f);

        if (hit.Contains(targetCollider))
        {
            return true;
        }

        return false;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            gm.enemies -= 1;
            Destroy(gameObject);
        }
    }
}
