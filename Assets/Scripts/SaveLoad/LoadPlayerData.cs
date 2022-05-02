// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class LoadPlayerData : MonoBehaviour
// {
//     // Start is called before the first frame update
//     void Awake()
//     {
//         HealthManager health = gameObject.GetComponent<HealthManager>();
//         KeyItemHolder keyItems = gameObject.GetComponent<KeyItemHolder>();
//         PlayerController cp = gameObject.GetComponent<PlayerController>();
        

//         PlayerData data = SaveFile.loadPlayer();

//         if(data == null)
//         {
//             Debug.Log("Hello1");
//             health.currentHealth = health.maxHealth;
//         }
        
//         else
//         {
//             Debug.Log("Hello2");
//             health.currentHealth = data.currentHealth;
//             keyItems.itemsHeld = data.keyItems;
//             cp.checkpoint = data.checkpoint;
//             cp.levelNum = data.levelNum;
//         }

//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }
// }
