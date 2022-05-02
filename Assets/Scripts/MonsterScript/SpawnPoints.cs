using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    public Transform spawnPoint; //Lets Player edit Monster spawn position
    public GameObject monsterPrefab; //Lets Player edit what monster spawns
    public int respawnTimer; //Lets Player edit respawn timers
    // Start is called before the first frame update
    void Start()
    {
        GameObject spawnMonster = (GameObject)Instantiate(monsterPrefab);
        spawnMonster.transform.position = spawnPoint.position;
        
        //Sends information to behavioural scripts to set homePositions and respawn delay
        spawnMonster.GetComponent<MonsterBehaviour>().homePos = spawnPoint;
        spawnMonster.GetComponent<MonsterHealthManager>().respawnTimer = respawnTimer;
    }
}
