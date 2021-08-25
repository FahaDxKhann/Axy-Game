using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int healtH;
    public int coin;
    public int potion;
    public float[] position;


    public PlayerData(Player player)
    {
        coin = player.coins;
        healtH = player.currentHealth;
        potion = player.Potions;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
