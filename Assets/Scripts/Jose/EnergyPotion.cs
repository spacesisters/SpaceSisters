using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : Collectable
{
    //Modifier quantity
    public float energymod;

    public override void Effects(ArturBasePlayerController player)
    {
        if (player.energy + energymod > 1)
        {
            player.energy = 1;
        }
        else
            player.energy += energymod;
        Destroy(gameObject);
    }
}