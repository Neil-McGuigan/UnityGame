using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //[HideInInspector]
    public int levelNum;
    //[HideInInspector]
    public string checkpoint;
    [SerializeField]
    public float speed; //Player's speed
    [HideInInspector] public float originalSpeed;
    public int weaponDamage; //Player damage
    [HideInInspector] public int originalDamage;
    //Toggles Player bonuses
    [HideInInspector]
    public bool speedActive = false;
    [HideInInspector]
    public bool damageActive = false;
    
    public Rigidbody2D rb;
    private Animator animator;
    private float attackDelay = 0.4f;
    private float attackTimer = 0.4f;
    private bool attacking;
    private bool attackLockout = false;

    void Start()
    {
        animator = GetComponent<Animator>(); //Reference to Player's animator
        rb = GetComponent<Rigidbody2D>(); //Reference to Player's rigidbody
        
        //Stores base values on launch
        originalSpeed = speed; //Stores original variables to reset speed after speed potion
        originalDamage = weaponDamage; //Stores original variables to reset damage after damage potion
        levelNum = SceneManager.GetActiveScene().buildIndex; //When player spawns in, sets variable for save and load functions to interact with
    }

    //Movement Script reference from tutorial video
    //(Brackeys - TOP DOWN MOVEMENT in Unity!) - https://www.youtube.com/watch?v=whzomFgjT50
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;

        animator.SetFloat("Horizontal", rb.velocity.x);
        animator.SetFloat("Vertical", rb.velocity.y);

        //Stores X and Y to store players orientation and set the correct sprite orientation when they stop moving
        if(Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Vertical") == -1 || Input.GetAxisRaw("Horizontal") == -1)
        {
            animator.SetFloat("FinalY", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("FinalX", Input.GetAxisRaw("Horizontal"));
        } 


        if(attacking) //When a Player attacks, it freezes them momentarily and puts their attack on a delay
        {
            rb.velocity = Vector2.zero;
            attackTimer -= Time.deltaTime;
            if(attackTimer <= 0) //Resets their attack delay and toggles bools to cancel the animation
            {
                animator.SetBool("Attacking", false);
                attacking = false;
                attackLockout = false;
            }
        }

        //When a player presses the space key and is not on cooldown, they can attack.
        //Sets booleans to true to prevent spam attacks and locking the animtion
        if(Input.GetKeyDown(KeyCode.Space) && (attackLockout == false)) 
        {
            attackTimer = attackDelay;
            animator.SetBool("Attacking", true);
            attacking = true;
            attackLockout = true;
        }  
    }
}


