using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public bool isPlayerBullet;
    public float damage;
    
    [SerializeField] private float despawnTime = 5f;

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

            if (isPlayerBullet)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().credits += 2;
            }
        }

        if (other.gameObject.CompareTag("Team"))
        {
            if (other.gameObject.name[0] != 'P')
            {
                other.gameObject.GetComponent<Team>().TakeDamage(damage);
            }
            else
            {
                other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
