using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    
    [SerializeField] private float despawnTime = 5f;
    private float despawn;

    private void Start()
    {
        despawn = despawnTime;
    }

    private void Update()
    {
        despawnTime -= Time.deltaTime; //Decreasing the timer by the elapsed time since last frame

        if (despawnTime <= 0)
        {
            Destroy(gameObject); //Destroys this bullet instance
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }

        if (other.gameObject.CompareTag("Team"))
        {
            other.gameObject.GetComponent<Team>().TakeDamage(damage);
        }

        if (other.collider.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
        
        Destroy(gameObject);
    }
}
