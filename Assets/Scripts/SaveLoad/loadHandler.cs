using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadHandler : MonoBehaviour
{
    void Awake()
    {
        //Checks if player selected to load a game by checking the PlayerPref. If they have, load in player stats
        if(PlayerPrefs.GetInt("loadGame") == 1)
        {
            LoadPlayerHP();
            PlayerPrefs.SetInt("loadGame", 0);
        }
    }
    
    //Saves the players stats to an encrypted binary file whenc called
    public void SavePlayerHP()
    {
        SaveFile.savePlayer(gameObject.GetComponent<HealthManager>(), gameObject.GetComponent<KeyItemHolder>(), gameObject.GetComponent<PlayerController>());
    }

    //Loads to players data in from their save file and assigns it to the players stats
    public void LoadPlayerHP()
    {
        PlayerData data = SaveFile.loadPlayer();
        
        gameObject.GetComponent<HealthManager>().currentHealth = data.currentHealth;
        gameObject.GetComponent<KeyItemHolder>().itemsHeld = data.keyItems;
        gameObject.GetComponent<PlayerController>().checkpoint = data.checkpoint;
        gameObject.GetComponent<PlayerController>().levelNum = data.levelNum;
        //Sets the players position to their last visited checkpoint

        
        gameObject.transform.position = GameObject.Find(gameObject.GetComponent<PlayerController>().checkpoint).transform.position;
    }
}
