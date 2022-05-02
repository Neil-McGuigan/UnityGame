using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator myAnim;
    private Transform target;
    
    //[HideInInspector]
    public Transform homePos;
    public Rigidbody2D rb;

    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float maxRange = 5.0f;
    [SerializeField] private float minRange = 1.1f;

    void Start()
    {
        myAnim = GetComponent<Animator>(); //Gets Monsters animations
        target = FindObjectOfType<PlayerController>().transform; //Sets Monsters target to the Player
        rb = GetComponent<Rigidbody2D>(); //Gets Monsters rigidbody
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if player is within range, if yes: Follow them. If no: return to starting position
        if(Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange)
        {
            followPlayer();
        }
        else if(Vector3.Distance(target.position, transform.position) >= maxRange)
        {
            returnHome();
        }
    }

    public void followPlayer()
    {
        myAnim.SetBool("isMoving", true); //Toggles animation parameter to enter blend tree
        //Updates X and Y variables
        myAnim.SetFloat("moveX", (target.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (target.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime); // Tells the monster to move towards player
    }

    public void returnHome()
    {
        //Updates X and Y variables
        myAnim.SetFloat("moveX", (homePos.position.x - transform.position.x));
        myAnim.SetFloat("moveY", (homePos.position.y - transform.position.y));
        transform.position = Vector3.MoveTowards(transform.position, homePos.position, speed * Time.deltaTime);

        //Checks if monster has returned to home point. If yes, toggle animation parameter off
        if(Vector3.Distance(transform.position, homePos.position) == 0)
        {
            myAnim.SetBool("isMoving", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Weapon") //If the players weapon collides with their hitbox, apply knockback to Monster
        {
            Vector2 spacing = (transform.position - other.transform.position) / 1.5f;
            transform.position = new Vector2(transform.position.x + spacing.x, transform.position.y + spacing.y);
        }
    }
}
