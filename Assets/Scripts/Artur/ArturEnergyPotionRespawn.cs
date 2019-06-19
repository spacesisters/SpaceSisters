using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturEnergyPotionRespawn : MonoBehaviour
{

    public bool shouldRespawn;
    public float respawnTime;



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
        {
            StartCoroutine(RespawnEnergyPotion());
        }
    }


    IEnumerator RespawnEnergyPotion()
    {
        yield return new WaitForSeconds(respawnTime);
        GameObject energyPotion = Resources.Load<GameObject>("Prefabs/Main/Collectables/EnergyPotion");
        GameObject instantiated = Instantiate(energyPotion, transform.position, Quaternion.identity);
        //instantiated.transform.parent = this.transform;
    }
}
