using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItemHolder : MonoBehaviour
{

    //[HideInInspector]
    public List<KeyItems.ItemAccessLevel> itemsHeld;
    //public Text messagePrompt;
    public PromptHandler message;

    private void Awake()
    {
        //container = transform.Find("KeyItems");
        //itemsHeld = new List<KeyItems.ItemAccessLevel>();
    }


    public List<KeyItems.ItemAccessLevel> getItemsHeld()
    {
        return itemsHeld;
    }


    public void AddItem(KeyItems.ItemAccessLevel item)
    {
        Debug.Log("Added a " + item + " to your UI");
        itemsHeld.Add(item);
    }
    

    public bool CheckHeld(KeyItems.ItemAccessLevel item)
    {
        return itemsHeld.Contains(item);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        KeyItems item = other.GetComponent<KeyItems>();
        if(item != null)
        {
            AddItem(item.getItemInfo());
            Debug.Log("Picked up: " + item.getItemInfo() + " key!");
            Destroy(item.gameObject);
        }

        Doorways door = other.GetComponent<Doorways>();
        if(door != null)
        {
            if(CheckHeld(door.getItemInfo()))
            {
                Debug.Log("Opening: " + door.getItemInfo() + " door");
                door.openDoor();
                
            }
            else
            {
                message.showMessage(door.getPrompt());
            }
        }


    }
}
