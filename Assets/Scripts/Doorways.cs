using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorways : MonoBehaviour
{
    private Animator myAnim;
    public int openTime;
    public string failedMessage;

    [SerializeField] private KeyItems.ItemAccessLevel key;

    private void Awake()
    {
        myAnim = GetComponent<Animator>(); 
    }

    public KeyItems.ItemAccessLevel getItemInfo()
    {
        return key;
    }

    public string getPrompt()
    {
        return failedMessage;
    }
    
    public void openDoor()
    {
        myAnim.SetBool("Open", true);
    }
    
    public void closeDoor()
    {
        myAnim.SetBool("Open", false);
    }

    void OnTriggerStay2D()
    {
        // if(Input.GetKeyDown(KeyCode.E))
        // {
        //     openDoor();
        // }

        // if()
        // {
        //     openDoor();
        // }
        
    }

    void OnTriggerExit2D()
    {
        Invoke("closeDoor", openTime);
    }
    
}
