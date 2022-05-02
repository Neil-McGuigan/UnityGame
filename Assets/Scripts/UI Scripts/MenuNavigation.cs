
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject helpMenu;

    public void playGame() 
    {
        SceneManager.LoadScene("Meadow(Tutorial)");
    }
 
    public void helpScreen() 
    {
        mainMenu.SetActive(false);
        helpMenu.SetActive(true);    
    }

    public void back() 
    {
        mainMenu.SetActive(true);
        helpMenu.SetActive(false);
    }
 
    public void exitGame() 
    {
        Application.Quit();
    }
}