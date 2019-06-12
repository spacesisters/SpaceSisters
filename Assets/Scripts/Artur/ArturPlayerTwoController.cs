using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerTwoController : ArturBasePlayerController
{
    
    private ArturForcefieldController forcefieldController;
    

    private void Start()
    {
        forcefieldController = transform.GetChild(0).GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(1);

        this.controllerType = ArturControllerSettings.player2ControllerType;
        this.playerNumber = ArturControllerSettings.player2ControllerNumber;

        // TODO: Remove this later. It's just to test the scenes without going through the main menu.
        if (ArturControllerSettings.player2ControllerType != "Xbox" ||
            ArturControllerSettings.player2ControllerType != "DS4")
            this.controllerType = "Xbox";

        if (ArturControllerSettings.player2ControllerNumber != 1 ||
            ArturControllerSettings.player2ControllerNumber != 2)
            this.playerNumber = 2;

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
