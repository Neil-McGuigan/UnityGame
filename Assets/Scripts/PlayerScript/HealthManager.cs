using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int currentHealth; //Players current health
    public int maxHealth; //Players max health
    [SerializeField]
    private float indicateDamage = 0f;
    private float indicateDamageFlashes = 0f;
    private bool takenDamage; //Bool to determine if player has taken damage
    [HideInInspector] public SpriteRenderer playerModel;

    //Assigns player sprite to a variable to alter later upon damage
    //If the player spawns in with 0 health, update it to be at max
    //This was to combat an issue with the respawn script that would cause them to spawn in immediately dead
    void Start()
    {
        playerModel = GetComponent<SpriteRenderer>();
        
        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;
        }
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player takes damage, it will update their sprites alpha color to toggle visible and invisible in order to show they have taken damage and how long they're immune for
        if(takenDamage)
        {
            if(indicateDamageFlashes > indicateDamage * 0.99f)
            {
                playerModel.color = new Color(playerModel.color.r, playerModel.color.g, playerModel.color.b, 0f);
            }
            else if(indicateDamageFlashes > indicateDamage * 0.75f)
            {
                playerModel.color = new Color(playerModel.color.r, playerModel.color.g, playerModel.color.b, 1f);
            }
            else if(indicateDamageFlashes > indicateDamage * 0.5f)
            {
                playerModel.color = new Color(playerModel.color.r, playerModel.color.g, playerModel.color.b, 0f);
            }
            else if(indicateDamageFlashes > indicateDamage * 0.25f)
            {
                playerModel.color = new Color(playerModel.color.r, playerModel.color.g, playerModel.color.b, 1f);
            }
            else if(indicateDamageFlashes > 0f)
            {
                playerModel.color = new Color(playerModel.color.r, playerModel.color.g, playerModel.color.b, 0f);
            }


            else
            {
                playerModel.color = new Color(playerModel.color.r, playerModel.color.g, playerModel.color.b, 1f);
                takenDamage = false; //Resets boolean to enable player to take damage again
            }

            indicateDamageFlashes -= Time.deltaTime;
        }
    }

    public void HurtPlayer(int damageValue)
    {
        //When boolean is false, player is open to taking damage again
        if(!takenDamage)
        {
            //On damage, take damage and re-enable damaged boolean
            currentHealth -= damageValue;
            takenDamage = true;
            indicateDamageFlashes = indicateDamage;
        }
        //If players health depletes, disable their character
        if(currentHealth <= 0)
        {
            gameObject.SetActive(false);
        }
    }    
}
