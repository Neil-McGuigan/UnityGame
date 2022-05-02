using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    private string checkpointName;

    void Start()
    {
        checkpointName = this.name;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //On collision, will update the players current checkpoint data
        if(collider.tag == "Player")
        {
            collider.GetComponent<PlayerController>().checkpoint = checkpointName;
        }
    }
}
