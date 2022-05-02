using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptHandler : MonoBehaviour
{
    public Text textBox;

    // Start is called before the first frame update
    void Start()
    {
        textBox.text = null;
    }

    // Update is called once per frame
    void Update()
    {
        //textBox.text = message;
    }

    public void showMessage(string message)
    {
        //textElement.SetActive(true);
        textBox.text = message;
        Invoke("clearMessage", 2);
    }

    private void clearMessage()
    {
        textBox.text = null;
        //textElement.SetActive(false);
    }
}
