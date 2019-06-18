using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Collectable
{
    public int speedmod;
    private ArturMetaInf metaInf;

    public override void Effects(ArturBasePlayerController player)
    {
        StartCoroutine("SetBuff", player);
    }

    public void Start()
    {
         metaInf = GetComponent<ArturMetaInf>();
        Debug.Log(metaInf);
    }

    IEnumerator SetBuff(ArturBasePlayerController player)
    {
        player.speedBonus = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        player.maxAccelerationGrounded += speedmod;
        yield return new WaitForSeconds(metaInf.speedBonusTime);
        player.maxAccelerationGrounded -= speedmod;
        player.speedBonus = false;
        Destroy(gameObject);
    }
}
