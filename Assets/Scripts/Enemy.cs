using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameManager gm;

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
