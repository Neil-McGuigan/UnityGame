using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagement : MonoBehaviour
{
    public GameObject playerObject;
    [SerializeField] private KeyItemHolder itemContainer;
    private Transform container;
    private Transform template;

    private Text messagePrompt;
    private HealthManager health;
    public Slider healthDisplay;
    public GameObject pauseScreen;
    public GameObject deathScreen;
    private bool isPaused;

    private int loadGame;
    //private int newZoneHP;
    // Start is called before the first frame update
    void Start()
    {
        health = FindObjectOfType<HealthManager>();
        isPaused = false;
    }

    void Awake()
    {
        // container = transform.Find("KeyItems");
        // template = container.Find("itemTemplate");
        // template.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.maxValue = health.maxHealth;
        healthDisplay.value = health.currentHealth;

        if(Input.GetKeyDown(KeyCode.Escape) && isPaused == false)
        {
            pauseGame();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused == true)
        {
            unPauseGame();
        }

        else if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Meadow(Tutorial)");
        }

        if(playerObject.GetComponent<HealthManager>().currentHealth <= 0)
        {
            
            deadPlayer();
        }

    }

    public void pauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
    }
    
    public void unPauseGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
    }

    public void deadPlayer()
    {
        Time.timeScale = 0f;
        playerObject.GetComponent<HealthManager>().currentHealth = playerObject.GetComponent<HealthManager>().maxHealth;
        deathScreen.SetActive(true);
    }

    
}
