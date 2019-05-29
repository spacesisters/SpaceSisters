using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class menuEnergyBar : MonoBehaviour
{
    public Image energy;
    public VictoriaPlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        energy.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.dead)
        {
            energy.fillAmount = player.energy;
        }
        
    }
}
