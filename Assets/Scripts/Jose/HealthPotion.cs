using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Collectable
{
    //Modifier quantity
    public int healthmod;

    public override void Effects(ArturBasePlayerController player)
    {
        // player.health += healthmod;
        ArturMetaInf arturMetaInf = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<ArturMetaInf>();
        arturMetaInf.playerHealth += healthmod;
        Destroy(gameObject);
    }
}
