using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterHealthManager : MonoBehaviour
{
    public GameObject dedicatedDrop; //Allows players to assign specific drops
    private SpawnPoints spawn;
    [HideInInspector]
    public Transform RespawnPoint; //Sets respawn point
    [HideInInspector]
    public int respawnTimer; //Time until enemy respawns

    public MonsterHealthBars Healthbar;
    public int maxHealth; // Monsters Maximum Health
    private int currentHealth; // Monsters Current Health while game is playing
    
    [SerializeField] private float immunityFrames = 0.5f; // How long the Monster is unable to be damaged between taking damage (In seconds)
    private float setRed = 0.5f; // Indicator to show Monster is damaged
    private bool takenDamage;
    
    private SpriteRenderer monsterSprite; //Reference to the Monsters sprite
    private UnityEngine.Object enemyRef; //Reference to Monster for respawn function 

    private UnityEngine.Object lootDrop; //Loads in item that is to be dropped on a successful loot drop
    private int lootChance; //Number to determine loot drop/chance
    [SerializeField]
    private bool finalBoss = false;

    void Start()
    {
        currentHealth = maxHealth; //Sets current health the max value when game is launched
        enemyRef = gameObject; //Stores reference for respawn script
        monsterSprite = GetComponent<SpriteRenderer>(); // Assigns sprite
        Healthbar.SetHealth(currentHealth, maxHealth); //Loads and configures Monsters Health bar
    }

    void Update()
    {
        if(takenDamage) //Checks if the monster has been damaged by the player. If true, then toggle indicator
        {
            monsterSprite.material.color = new Color(1f, monsterSprite.color.g, monsterSprite.color.b, 1f);
            if(setRed <= 0) //When timer expires, damage indicator is toggled off
            {
                takenDamage = false;
            }
        }

        else //Resets enemy sprite to normal colours
        {
            monsterSprite.material.color = Color.white;
        }

        setRed -= Time.deltaTime;
    }

    public void hurtMonster(int damageDealt)
    {
        if(!takenDamage) //If the Monster's immunity delay has elapsed, take damage
        {
            currentHealth -= damageDealt;
            
            takenDamage = true;
            setRed = immunityFrames;
        }
        Healthbar.SetHealth(currentHealth, maxHealth);

        
        if(currentHealth <= 0) //If Monsters heal depletes, check if they drop loot and remove them to begin respawn script
        {
            if(finalBoss)
            {
                SceneManager.LoadScene("VictoryScreen");
            }
            else
            {
                gameObject.SetActive(false); //Hides object while it gets ready for respawn
                dropLoot(); //Loot drop script
                //Reset values on death
                takenDamage = false;
                setRed = 0f;
                monsterSprite.material.color = Color.white;
                currentHealth = maxHealth;
                
                Invoke("Respawn", respawnTimer); //Start respawn
            }
        }
        
    }

    void Respawn()
    {
        GameObject enemyClone = (GameObject)Instantiate(enemyRef); //Creates clone of Monster
        enemyClone.transform.position = gameObject.GetComponent<MonsterBehaviour>().homePos.position; //Sets clone's position
        
        enemyClone.SetActive(true); //Sets respawned Monster to active
        Destroy(gameObject); //Deletes original object from hierarchy
    }

    public void dropLoot()
    {
        //Checks if the enemy killed is a boss
        if(this.tag == "Boss")
        {
            //Checks if boss has been assigned a specific drop
            if(dedicatedDrop != null)
            {
                lootDrop = dedicatedDrop;
                GameObject drop = (GameObject)Instantiate(lootDrop);
                drop.transform.position = transform.position;
                drop.SetActive(true);
            }
            
            //Has boss drop a health item so players can heal up afterwards
            lootDrop = Resources.Load("Cake");
            GameObject bossHeal = (GameObject)Instantiate(lootDrop);
            //Vector2 to have it instantiate below so it does not have any overlap issues
            Vector2 dropPos = transform.position;
            dropPos[1] -= 1f;
            bossHeal.transform.position = dropPos;
            bossHeal.SetActive(true);
        }
        else
        {
            lootChance = Random.Range(0,101); //Generate Random number to determine loot drop/chance

            if(lootChance > 96) //Double Damage Potion
            {
                lootDrop = Resources.Load("DamagePot"); //Loads prefab from resources
                GameObject drop = (GameObject)Instantiate(lootDrop); //Instantiates the object
                drop.transform.position = transform.position; //Sets the item on the enemies death point
                drop.SetActive(true); //Makes it visible and interactable
            }

            else if(lootChance > 91 && lootChance < 97) //Max Heal Potion
            {
                lootDrop = Resources.Load("MaxPot");
                GameObject drop = (GameObject)Instantiate(lootDrop);
                drop.transform.position = transform.position;
                drop.SetActive(true);
            }

            else if(lootChance > 85 && lootChance < 92) //Speed Potion
            {
                lootDrop = Resources.Load("SpeedPot");
                GameObject drop = (GameObject)Instantiate(lootDrop);
                drop.transform.position = transform.position;
                drop.SetActive(true);
            }

            else if(lootChance > 70 && lootChance < 86) //Med Food Heal item
            {
                lootDrop = Resources.Load("Cake");
                GameObject drop = (GameObject)Instantiate(lootDrop);
                drop.transform.position = transform.position;
                drop.SetActive(true);
            }

            else if(lootChance > 50 && lootChance < 71) //Low Food Heal item
            {
                lootDrop = Resources.Load("Apple");
                GameObject drop = (GameObject)Instantiate(lootDrop);
                drop.transform.position = transform.position;
                drop.SetActive(true);
            }

            else
            {
                //Drop Nothing
            }
        }  
    }
}
