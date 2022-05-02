using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterHealthBars : MonoBehaviour
{
    public Slider HealthBar;
    public Vector3 Offset;

    //Updates the health on the slider to display current health of the enemy. Only becomes visible whenever enemy is not at max health
    public void SetHealth(float currentHealth, float maxHealth)
    {
        HealthBar.gameObject.SetActive(currentHealth < maxHealth);
        HealthBar.value = currentHealth;
        HealthBar.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        HealthBar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset); 
    }
}
