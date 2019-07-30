using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturPlayerOneController : ArturBasePlayerController
{

    public float pushCooldownTimer;

    private ArturForcefieldController forcefieldController;
    private float pushCooldownTime;
    private bool playForcefieldAnimation;

    private void Start()
    {
        forcefieldController = transform.GetChild(0).GetComponent<ArturForcefieldController>();
        forcefieldController.Initialize(-1);

        this.controllerType = ArturControllerSettings.player1ControllerType;
        this.playerNumber = ArturControllerSettings.player1ControllerNumber;


        // TODO: Remove this later. It's just to test the scenes without going through the main menu.
        if (ArturControllerSettings.player1ControllerType == string.Empty) 
        {
            this.controllerType = "Xbox";
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
        

    }

    private new void FixedUpdate()
    {
        base.FixedUpdate();
        playForcefieldAnimation = false;
        if (pushCooldownTime > 0)
        {
            pushCooldownTime -= Time.deltaTime;
        }
        else if (pushCooldownTime < 0)
        {
            pushCooldownTime = 0;
        }

        if (doForcefield && pushCooldownTime == 0 && energy > energyDrainPerSecond)
        {
            playForcefieldAnimation = true;

            if (playForcefieldSound)
            {
                audioSource.PlayOneShot(sfxForcefield);
                playForcefieldSound = false;
            }

            forcefieldController.ActivateForcefield();
            energy -= energyDrainPerSecond;
            if (energy < 0)
                energy = 0;
            pushCooldownTime = pushCooldownTimer;
            doForcefield = false;
        }
        else
            playForcefieldSound = true;
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
