using System;
using Pathfinding;
using System.Linq;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameManager gm;

    private Collider2D _targetCollider;

    private void Start()
    {
        StartCoroutine(PickTarget());
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
            gm.enemies -= 1;
            Destroy(gameObject);
        }
    }

    IEnumerator PickTarget()
    {
        yield return new WaitForSeconds(0.2f);
        
        GameObject[] all = GameObject.FindGameObjectsWithTag("Team");
        Transform target = all[Random.Range(0, all.Length)].transform;

        GetComponent<AIDestinationSetter>().target = target;
        
        _targetCollider = GetComponent<AIDestinationSetter>().target.GetComponent<Collider2D>();
    }
}
