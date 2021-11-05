using System;
using Pathfinding;
using System.Linq;
using UnityEngine;

public class Team : MonoBehaviour
{
    public float health;
    public GameManager gm;

    private Collider2D _targetCollider;

    private void Start()
    {
        _targetCollider = GetComponent<AIDestinationSetter>().target.GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        GetComponent<AIPath>().maxSpeed = IsTargetInRange() ? 0 : 2;
    }
    
    private bool IsTargetInRange()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 4.5f);

        if (hit.Contains(_targetCollider))
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
            gm.team -= 1;
            Destroy(gameObject);
        }
    }
}