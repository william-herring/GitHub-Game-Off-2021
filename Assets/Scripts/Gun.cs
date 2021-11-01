using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab; //Stores the bullet object
    [SerializeField] private Transform bulletSpawnTransform; //Stores the spawn location of the bullet
    [SerializeField] private float cooldownTime = 1.3f;

    public float ammunition = 30f;
    public float bulletSpeed = 12f;

    private float cooldown;

    private void Awake()
    {
        cooldown = cooldownTime;
    }

    private void Update()
    {
        cooldownTime -= Time.deltaTime;

        if (cooldownTime <= 0)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (ammunition < 1)
        {
            return;
        }
        
        GameObject obj = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity);
        
        obj.GetComponent<Rigidbody2D>().velocity = transform.right * bulletSpeed;
        Debug.Log(obj.GetComponent<Rigidbody2D>().velocity);

        ammunition -= 1;
        cooldownTime = cooldown;
    }
}
