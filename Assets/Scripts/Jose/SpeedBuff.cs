using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Collectable
{
    public int speedmod;

    public override void Effects(ArturBasePlayerController player)
    {
        StartCoroutine("SetBuff", player);
    }


    IEnumerator SetBuff(ArturBasePlayerController player)
    {
        player.speedBonus = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        player.maxAccelerationGrounded += speedmod;
        yield return new WaitForSeconds(GetComponent<ArturMetaInf>().speedBonusTime);
        player.maxAccelerationGrounded -= speedmod;
        player.speedBonus = false;
        Destroy(gameObject);
    }
}
