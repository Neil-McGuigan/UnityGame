using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadLevel : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadBar;
    public int selectLevel;
    private int loadSaveLevelNum;
    private int RespawnLevel;

    //All functions reset time scale to prevent freezing
    //Starts new game and sets PlayerPref to tell the game not to load in save data
    public void New(int levelNum)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsync(levelNum));
        PlayerPrefs.SetInt("loadGame", 0);
    }

    //Used to load the player between zones
    public void Load(int levelNum)
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadAsync(levelNum));
        Debug.Log(PlayerPrefs.GetInt("loadGame", 1));
        PlayerPrefs.SetInt("loadGame", 1);
    }

    //Loads game and sets PlayerPref to tell the game to load in previous save data
    public void LoadSave()
    {
        Time.timeScale = 1f;
        PlayerData data = SaveFile.loadPlayer();
        loadSaveLevelNum = data.levelNum;
        StartCoroutine(LoadAsync(loadSaveLevelNum));
        PlayerPrefs.SetInt("loadGame", 1);
    }

    //Respawns the player into the same level they died on
    public void RespawnPLayer()
    {
        Time.timeScale = 1f;
        PlayerData data = SaveFile.loadPlayer();
        RespawnLevel = data.levelNum;
        StartCoroutine(LoadAsync(RespawnLevel));
        PlayerPrefs.SetInt("loadGame", 1);
    }

    //Simple loading screen script from a youtube tutorial
    //(Brackeys - How to make a LOADING BAR in Unity) - https://www.youtube.com/watch?v=YMj2qPq9CP8
    IEnumerator LoadAsync(int levelNum)
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(levelNum);

        loadingScreen.SetActive(true);

        while(!loading.isDone)
        {
            float progress = Mathf.Clamp01(loading.progress / 0.9f);
            //Debug.Log(progress);
            loadBar.value = progress;
            yield return null;
        }
    }

    //When player transitions between zones, triggering this will cause their data to be saved before theyr'e loaded into the next zone
    //Collider also updates their level with the scene number of the next one they wish to travel to
    public void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Player")
        {
            GameObject playerUpdater = GameObject.Find(collider.tag);
            playerUpdater.GetComponent<PlayerController>().levelNum = selectLevel;
            playerUpdater.GetComponent<loadHandler>().SavePlayerHP();
            Load(selectLevel);
        }
    }
}
