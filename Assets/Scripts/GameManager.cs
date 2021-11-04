using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Transform playerTransform;
    
    [SerializeField] private List<GameObject> commonTroops; //This array stores the common troops (60% spawn rate)
    [SerializeField] private List<GameObject> rareTroops; //This array stores the rare troops (30% spawn rate)
    [SerializeField] private List<GameObject> arialTroops; //This array stores the arial troops such as wasps (10% spawn rate)
    [SerializeField] private int armySize;
    public int enemies;

    private void Update()
    {
        //Spawning enemy troops
        if (enemies < armySize)
        {
            int enemiesRequired = armySize - enemies;

            for (int i = 0; i < armySize; i++)
            {
                int c = Random.Range(0, 11);

                if (c <= 6)
                {
                    //Spawn common troop
                    GameObject troop = Instantiate(commonTroops[Random.Range(0, commonTroops.Count)], transform.position, Quaternion.identity); //TODO: Spawn at colony position
                    troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                    troop.GetComponent<Enemy>().gm = this;
                    enemies += 1;
                    
                    Debug.Log("Spawned common enemy troop");
                } 
                else if (c > 6)
                {
                    if (c < 90)
                    {
                        //Spawn arial troop
                        GameObject troop = Instantiate(arialTroops[Random.Range(0, commonTroops.Count)], transform.position, Quaternion.identity);
                        troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                        troop.GetComponent<Enemy>().gm = this;
                        enemies += 1;
                        
                        Debug.Log("Spawned arial enemy troop");
                    }
                    else
                    {
                        //Spawn rare troop
                        GameObject troop = Instantiate(rareTroops[Random.Range(0, commonTroops.Count)], transform.position, Quaternion.identity);
                        troop.GetComponent<AIDestinationSetter>().target = playerTransform;
                        troop.GetComponent<Enemy>().gm = this;
                        enemies += 1;
                        
                        Debug.Log("Spawned rare enemy troop");
                    }
                }
            }
        }
    }
}
