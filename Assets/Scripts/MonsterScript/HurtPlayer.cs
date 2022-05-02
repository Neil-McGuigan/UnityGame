using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    //Script to damage the player. Tutorial referenced by Hundred Fire Games (Topdown 2D RPG In Unity - Hurt Player)
    //https://www.youtube.com/watch?v=xlHPlSp3s04
    private HealthManager manageHealth;
    public float hurtDelay;
    public bool isTouching;
    [SerializeField]
    private int damageValue = 10; //Damage to inflict to player
    
    // Start is called before the first frame update
    void Start()
    {
        manageHealth = FindObjectOfType<HealthManager>();
        isTouching = false;
        hurtDelay = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isTouching) //Checks if the monsters collider is in contact with the players
        {
            hurtDelay -= Time.deltaTime; //Counts down timer to determine if it can damage again
            if(hurtDelay <= 0) //When timer depletes, damage player again if it is still in contact
            {
                manageHealth.HurtPlayer(damageValue);
                hurtDelay = 1.5f; //Reset damage timer
            }
        }
    }
    //Damage player on contact
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {         
            other.gameObject.GetComponent<HealthManager>().HurtPlayer(damageValue);
        }
    }
    //Toggle contact boolean if player stays in contact
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            isTouching = true;
        }
        
        
    }
    //When player leave collision, reset contact boolean and reset damage timer
    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.collider.tag == "Player")
        {
            isTouching = false;
            hurtDelay = 1.5f;
        }
    } 
}
