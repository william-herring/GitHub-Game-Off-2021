using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 4f;
    public GameManager gm;

    private void Update()
    {
        //FOR TESTING PURPOSES ONLY
        if (Input.GetKeyDown(KeyCode.K))
        {
            TakeDamage(200f);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            gm.deathScreen.SetActive(true);
            Destroy(gameObject);
        }
    }
}
