using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerOneController : ArturBasePlayerController
{
    
    private ArturForcefieldController forcefieldController;
    

    private void Start()
    {
        forcefieldController = transform.GetChild(0).GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(-1);

        this.controllerType = ArturControllerSettings.player1ControllerType;
        this.playerNumber = ArturControllerSettings.player1ControllerNumber;


        // TODO: Remove this later. It's just to test the scenes without going through the main menu.
        if (ArturControllerSettings.player1ControllerType == string.Empty) 
        {
            this.controllerType = "DS4";
        }

        if (ArturControllerSettings.player1ControllerNumber == -1)
        {
            this.playerNumber = 1;
        }
        
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
