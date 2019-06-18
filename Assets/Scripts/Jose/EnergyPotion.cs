using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPotion : Collectable
{
    //Modifier quantity
    public int energymod;

    public override void Effects(ArturBasePlayerController player)
    {
        player.energy += energymod;
        Destroy(gameObject);
    }
}