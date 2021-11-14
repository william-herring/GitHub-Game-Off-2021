using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 4f;
    public GameManager gm;

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
