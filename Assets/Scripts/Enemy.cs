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
    public GameObject bulletPrefab;
    
    [SerializeField] private Transform bulletSpawnTransform;
    public GameObject bulletCollectable;

    private Collider2D _targetCollider;
    private float cooldown;
    private float cooldownTime = 1.3f;
    
    private void Awake()
    {
        cooldown = cooldownTime;
        
        bulletSpawnTransform = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0); //Really inefficient but Unity offers no better alternative
    }
    
    private void Update()
    {
        cooldownTime -= Time.deltaTime; //Decreasing the timer by the elapsed time since last frame

        if (_targetCollider == null)
        {
            GameObject[] all = GameObject.FindGameObjectsWithTag("Team");
            Transform target = all[Random.Range(0, all.Length)].transform;
            GetComponent<AIDestinationSetter>().target = target;
        }
    }

    private void Start()
    {
        StartCoroutine(PickTarget());
    }

    private void FixedUpdate()
    {
        if (IsTargetInRange())
        {
            GetComponent<AIPath>().maxSpeed = 0;

            if (cooldownTime <= 0)
            {
                Shoot();
            }
        }
        else
        {
            GetComponent<AIPath>().maxSpeed = 2;
        }
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
    
    private void Shoot()
    {
        GameObject obj = Instantiate(bulletPrefab, bulletSpawnTransform.position, Quaternion.identity);
        
        obj.GetComponent<Rigidbody2D>().velocity = transform.up * 12;
        obj.GetComponent<Bullet>().damage = 1.1f;
        
        cooldownTime = cooldown;
    }

    public void TakeDamage(float amount, bool isPlayerBullet)
    {
        health -= amount;

        if (!(health <= 0)) return; //Returns to reduce nesting
        
        gm.enemies -= 1;

        if (isPlayerBullet)
        {
            Instantiate(bulletCollectable, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
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
