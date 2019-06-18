using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : Collectable
{
    public int speedmod;
    public float time;

    public override void Effects(ArturBasePlayerController player)
    {
        StartCoroutine("SetBuff", player);
    }


    IEnumerator SetBuff(ArturBasePlayerController player)
    {
        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        player.maxAccelerationGrounded += speedmod;
        yield return new WaitForSeconds(time);
        player.maxAccelerationGrounded -= speedmod;
        Destroy(gameObject);
    }
}
