using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerOneController : ArturBasePlayerController
{
    
    private ArturForcefieldController forcefieldController;
    

    private void Start()
    {
        forcefieldController = transform.GetChild(0).GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(1);

        Player[] players = JsonUtility.FromJson<Player[]>(PlayerPrefs.GetString("Controllers"));
        this.controllerType = players[0].controllertype;
        this.playerNumber = players[0].numForJulius;
        base.Initialize();
    }

    private new void Update()
    {
        base.Update();
        TranslateInput();
        if (doForcefield)
        {
            forcefieldController.ActivateForcefield();
            energy -= energyDrainPerSecond * Time.deltaTime;
        }

        
    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        
    }

    private new void TranslateInput()
    {
        base.TranslateInput();
        
    }

}
