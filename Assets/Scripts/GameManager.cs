using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform playerTransform;
    public float spawnRadius = 5f;
    public int enemies;

    [SerializeField] private Transform enemySpawnLocation;
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
                Vector3 spawnLocation = enemySpawnLocation.position + Random.onUnitSphere * spawnRadius;

                if (c <= 6)
                {
                    //Spawn common troop
                    GameObject troop = Instantiate(commonTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                    troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                    troop.GetComponent<Enemy>().gm = this;
                    enemies += 1;
                } 
                else if (c > 6)
                {
                    if (c < 90)
                    {
                        //Spawn arial troop
                        GameObject troop = Instantiate(arialTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                        troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                        troop.GetComponent<Enemy>().gm = this;
                        enemies += 1;
                    }
                    else
                    {
                        //Spawn rare troop
                        GameObject troop = Instantiate(rareTroops[Random.Range(0, commonTroops.Count)], spawnLocation, Quaternion.identity);
                        troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                        troop.GetComponent<Enemy>().gm = this;
                        enemies += 1;
                    }
                }
            }
        }
    }
}
