using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    HealthManager playerHealth;
    PlayerController playerStats;

    //Depending on the type of item, these stats will be applied to the Player upon pickup
    public int healing; //Value to heal the Player's health

    public float speedMultiplier; //Multiplier to how much faster the Player shall move
    public float speedDuration; //Duration of how long the speed boost lasts

    public int damageMultiplier; //Multiplier to how much the more damage the Player's attacks shall do
    public float damageDuration; //Duration of how long the damage boost lasts

    void Awake()
    {
        playerHealth = FindObjectOfType<HealthManager>(); //Reference to script that controls the Player's health
        playerStats = FindObjectOfType<PlayerController>(); //Reference to Player's stats
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(this.tag == "HealItem") //Checks if it is a healing item
            {
                if(playerHealth.currentHealth == playerHealth.maxHealth) //If they're health is already at max
                {
                        //DO NOT PICKUP
                }
                else if((playerHealth.currentHealth + healing) < playerHealth.maxHealth) //Adds healing value to Player's current health 
                {
                    Destroy(gameObject);
                    playerHealth.currentHealth += healing;
                }

                else if((playerHealth.currentHealth + healing) > playerHealth.maxHealth) //Player's health would go over max on pickup, so reverts it to max health value
                {
                    Destroy(gameObject);
                    playerHealth.currentHealth = playerHealth.maxHealth;
                }
            }

            if(this.tag == "SpeedItem") //Checks if it is a speed boost item
            {
                
                Debug.Log("Speed Boost!");
                if(!playerStats.speedActive) //If Player does not already have a speed boost, apply boost values and change Player colour to indicate
                {
                    playerStats.speed *= speedMultiplier;
                    playerHealth.playerModel.color = new Color(153f/255f, 1f, 153f/255f, 1f);
                    Invoke("restoreSpeed", speedDuration);
                    gameObject.SetActive(false);
                    playerStats.speedActive = true;
                }
                else if(playerStats.speedActive) //If Player already has a speed boost, reset the boosts duration
                {
                    restoreSpeed();
                    playerStats.speed *= speedMultiplier;
                    playerHealth.playerModel.color = new Color(153f/255f, 1f, 153f/255f, 1f);
                    Invoke("restoreSpeed", speedDuration);
                    gameObject.SetActive(false);
                    playerStats.speedActive = true;
                }
            }

            if(this.tag == "DamageItem") //Checks if it is a damage boost item
            {
                if(!playerStats.damageActive) //If Player does not already have a damage boost, apply boost values and change Player colour to indicate
                {
                    playerStats.weaponDamage *= damageMultiplier;
                    playerHealth.playerModel.color = new Color(1f, 178f/255f, 102f/255f, 1f);
                    Invoke("restoreDamage", damageDuration);
                    gameObject.SetActive(false);
                    playerStats.damageActive = true;
                }
                else if(playerStats.damageActive) //If Player already has a damage boost, reset the boosts duration
                {
                    restoreDamage();
                    playerStats.weaponDamage *= damageMultiplier;
                    playerHealth.playerModel.color = new Color(1f, 178f/255f, 102f/255f, 1f);
                    Invoke("restoreDamage", damageDuration);
                    gameObject.SetActive(false);
                    playerStats.damageActive = true;
                }
            }


        }
    }

    //Reverts Player speed and sprite colour back to base value and disables boost
    private void restoreSpeed() 
    {
        playerStats.speed = playerStats.originalSpeed;
        playerHealth.playerModel.color = new Color(1f, 1f, 1f, 1f);
        playerStats.speedActive = false;
        Destroy(gameObject);
    }

    //Reverts Player damage and sprite colour back to base value and disables boost
    private void restoreDamage() 
    {
        playerStats.weaponDamage = playerStats.originalDamage;
        playerHealth.playerModel.color = new Color(1f, 1f, 1f, 1f);
        playerStats.damageActive = false;
        Destroy(gameObject);
    }

}
