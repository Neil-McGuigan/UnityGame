using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageMobs : MonoBehaviour
{
    private int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if players weapon hitbox makes contact with an enemy
        if(other.tag == "Monster" || other.tag == "Boss" || other.tag == "SkeletonKing")
        {
            damage = FindObjectOfType<PlayerController>().weaponDamage; //Gets the players damage variable
            
            //Finds monster that weapon collided with and updates their health to display damage
            MonsterHealthManager monsterHurt;
            monsterHurt = other.gameObject.GetComponent<MonsterHealthManager>();
            monsterHurt.hurtMonster(damage);
        }
    }
}
