using System;
using System.Collections.Generic;
using Pathfinding;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Dictionary<string, int> troopCosts = new Dictionary<string, int>();
    
    public float spawnRadius = 5f;
    public int enemies;
    public int team = 1; //Equal to 1 to account for player
    public int credits = 0;

    [SerializeField] private Transform enemySpawnLocation;
    [SerializeField] private Transform teamSpawnLocation;
    [SerializeField] private List<GameObject> commonTroops; //This array stores the common troops (60% spawn rate)
    [SerializeField] private List<GameObject> rareTroops; //This array stores the rare troops (30% spawn rate)
    [SerializeField] private List<GameObject> aerialTroops; //This array stores the arial troops such as wasps (10% spawn rate)
    [SerializeField] private int armySize;
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private GameObject bulletCollectable;

    [SerializeField] private GameObject bulletPrefab;
    public GameObject deathScreen;

    private void Awake()
    {
        troopCosts.Add("Ant", 0);
        troopCosts.Add("Bull Ant", 15);
        troopCosts.Add("Ladybug", 25);
        troopCosts.Add("Bee", 50);
        troopCosts.Add("Wasp", 100);
    }

    private void Update()
    {
        creditText.text = $"{credits} C";
        
        //Spawning enemy troops
        if (enemies < armySize)
        {
            Debug.Log(enemies);
            int enemiesRequired = armySize - enemies;

            for (int i = 0; i < enemiesRequired; i++)
            {
                int c = Random.Range(0, 11);
                Vector3 spawnLocation = enemySpawnLocation.position + Random.onUnitSphere * spawnRadius; //Picks a random spawn position inside a circle at the colony

                if (c <= 6)
                {
                    //Spawn common troop
                    GameObject troop = Instantiate(commonTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                    
                    troop.tag = "Enemy";
                    troop.AddComponent<Enemy>().health = 4f;
                    troop.GetComponent<Enemy>().gm = this;
                    troop.GetComponent<Enemy>().bulletPrefab = bulletPrefab;
                    troop.GetComponent<Enemy>().bulletCollectable = bulletCollectable;
                    enemies += 1;
                } 
                else if (c > 6)
                {
                    if (c < 90)
                    {
                        //Spawn arial troop
                        GameObject troop = Instantiate(aerialTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                        
                        troop.tag = "Enemy";
                        troop.AddComponent<Enemy>().health = 2f;
                        troop.GetComponent<Enemy>().gm = this;
                        troop.GetComponent<Enemy>().bulletPrefab = bulletPrefab;
                        troop.GetComponent<Enemy>().bulletCollectable = bulletCollectable;
                        enemies += 1;
                    }
                    else
                    {
                        //Spawn rare troop
                        GameObject troop = Instantiate(rareTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);

                        troop.tag = "Enemy";
                        troop.AddComponent<Enemy>().health = 5f;
                        troop.GetComponent<Enemy>().gm = this;
                        troop.GetComponent<Enemy>().bulletPrefab = bulletPrefab;
                        troop.GetComponent<Enemy>().bulletCollectable = bulletCollectable;
                        enemies += 1;
                    }
                }
            }
        }
        
        //Spawning team troops
        if (team < armySize)
        {
            Debug.Log(team);
            int teamRequired = armySize - team;

            for (int i = 0; i < teamRequired; i++)
            {
                int c = Random.Range(0, 11);
                Vector3 spawnLocation = teamSpawnLocation.position + Random.onUnitSphere * spawnRadius; //Picks a random spawn position inside a circle at the colony

                GameObject[] all = GameObject.FindGameObjectsWithTag("Enemy");
                Transform target = all[Random.Range(0, all.Length)].transform;

                if (c <= 6)
                {
                    //Spawn common troop
                    GameObject troop = Instantiate(commonTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                    
                    troop.tag = "Team";
                    troop.AddComponent<Team>().health = 4f;
                    troop.GetComponent<AIDestinationSetter>().target = target;
                    troop.GetComponent<Team>().gm = this;
                    troop.GetComponent<Team>().bulletPrefab = bulletPrefab;
                    team += 1;
                } else if (c > 6)
                {
                    if (c < 90)
                    {
                        //Spawn arial troop
                        GameObject troop = Instantiate(aerialTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                    
                        troop.tag = "Team";
                        troop.AddComponent<Team>().health = 4f;
                        troop.GetComponent<AIDestinationSetter>().target = target;
                        troop.GetComponent<Team>().gm = this;
                        troop.GetComponent<Team>().bulletPrefab = bulletPrefab;
                        team += 1;
                    }
                    else
                    {
                        //Spawn rare troop
                        GameObject troop = Instantiate(rareTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                    
                        troop.tag = "Team";
                        troop.AddComponent<Team>().health = 4f;
                        troop.GetComponent<AIDestinationSetter>().target = target;
                        troop.GetComponent<Team>().gm = this;
                        troop.GetComponent<Team>().bulletPrefab = bulletPrefab;
                        team += 1;
                    }
                }
            }
        }
    }

    public void Respawn(string troopType)
    {
        if (credits >= troopCosts[troopType])
        {
            credits -= troopCosts[troopType];
        }
    }
}
