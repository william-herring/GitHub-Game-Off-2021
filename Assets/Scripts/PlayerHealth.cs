using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float health = 4f;

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
    
    //TODO: Player death
}
