using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentHealth;
    public List<KeyItems.ItemAccessLevel> keyItems;
    public string checkpoint;
    public int levelNum;

    //Collects players current data when called and stores them in variables to then send off into a datastream later on
    public PlayerData(HealthManager playerHealth, KeyItemHolder playerItems, PlayerController controller)
    {
        currentHealth = playerHealth.currentHealth;
        keyItems = playerItems.itemsHeld;
        checkpoint = controller.checkpoint;
        levelNum = controller.levelNum;
    }

}
