using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerTwoController : ArturBasePlayerController
{
    
    private ArturForcefieldController forcefieldController;
    private bool playForcefieldAnimation;




    private void Start()
    {
        forcefieldController = transform.GetChild(0).GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(1);

        this.controllerType = ArturControllerSettings.player2ControllerType;
        this.playerNumber = ArturControllerSettings.player2ControllerNumber;



        // TODO: Remove this later. It's just to test the scenes without going through the main menu.
        if (ArturControllerSettings.player2ControllerType == string.Empty)
        {
            this.controllerType = "Keyboard";
        }

        if (ArturControllerSettings.player2ControllerNumber == -1)
        {
            this.playerNumber = 2;
        }

        base.Initialize();
    }

    private new void Update()
    {
        base.Update();
        TranslateInput();

        

    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        if (doForcefield && energy > energyDrainPerSecond * Time.deltaTime)
        {
            if (playForcefieldSound)
            {
                audioSource.PlayOneShot(sfxForcefield);
                playForcefieldSound = false;
            }
            forcefieldController.ActivateForcefield();
            energy -= energyDrainPerSecond * Time.deltaTime;
            playForcefieldAnimation = true;
        }
        else
        {
            playForcefieldAnimation = false;
            playForcefieldSound = true;
        }
    }

    private new void TranslateInput()
    {
        base.TranslateInput();
        
    }

    public override bool GetDoForcefield()
    {
        return playForcefieldAnimation;
    }

}
