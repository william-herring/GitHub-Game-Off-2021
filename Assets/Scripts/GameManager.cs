using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform playerTransform; //TODO: pick random troop target
    public float spawnRadius = 5f;
    public int enemies;
    public int team = 1; //Equal to 1 to account for player

    [SerializeField] private Transform enemySpawnLocation;
    [SerializeField] private Transform teamSpawnLocation;
    [SerializeField] private List<GameObject> commonTroops; //This array stores the common troops (60% spawn rate)
    [SerializeField] private List<GameObject> rareTroops; //This array stores the rare troops (30% spawn rate)
    [SerializeField] private List<GameObject> arialTroops; //This array stores the arial troops such as wasps (10% spawn rate)
    [SerializeField] private int armySize;

    private void Update()
    {
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
                    //troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                    troop.GetComponent<Enemy>().gm = this;
                    enemies += 1;
                } 
                else if (c > 6)
                {
                    if (c < 90)
                    {
                        //Spawn arial troop
                        GameObject troop = Instantiate(arialTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                        
                        troop.tag = "Enemy";
                        troop.AddComponent<Enemy>().health = 2f;
                        //troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                        troop.GetComponent<Enemy>().gm = this;
                        enemies += 1;
                    }
                    else
                    {
                        //Spawn rare troop
                        GameObject troop = Instantiate(rareTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);

                        troop.tag = "Enemy";
                        troop.AddComponent<Enemy>().health = 5f;
                        //troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                        troop.GetComponent<Enemy>().gm = this;
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
                    team += 1;
                } else if (c > 6)
                {
                    if (c < 90)
                    {
                        //Spawn arial troop
                        GameObject troop = Instantiate(arialTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                    
                        troop.tag = "Team";
                        troop.AddComponent<Team>().health = 4f;
                        troop.GetComponent<AIDestinationSetter>().target = target;
                        troop.GetComponent<Team>().gm = this;
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
                        team += 1;
                    }
                }
            }
        }
    }
}
