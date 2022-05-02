using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItems : MonoBehaviour
{
    //public int accessLevel;

    [SerializeField] private ItemAccessLevel item;
    public enum ItemAccessLevel
    {
        MeadowKey,
        ForestKey,
        ArenaKey,
        PondKey,
        CaveKey,
        
        SnowKey,
        CastleKey,
        BossKey,
        Tier6,
    }

    public ItemAccessLevel getItemInfo()
    {
        return item;
    }

}
