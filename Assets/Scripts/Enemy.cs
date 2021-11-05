using Pathfinding;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public GameManager gm;

    private void Update()
    {
        gameObject.GetComponent<AIPath>().enabled = !(Vector2.Distance(transform.position, GetComponent<AIDestinationSetter>().target.position) <= 1.4f);
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
