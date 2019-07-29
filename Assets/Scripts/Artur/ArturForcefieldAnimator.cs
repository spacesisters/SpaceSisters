using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturForcefieldAnimator : MonoBehaviour
{
    public string playerTag;

    private ArturBasePlayerController player;
    private Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<ArturBasePlayerController>();
    }

    private void Update()
    {
        bool doForcefield = player.GetDoForcefield();

        if (doForcefield)
        {
            print(playerTag + " " + doForcefield);

        }

        animator.SetBool("isForcefielding", doForcefield);
    }

    public void GetPlayerTarget()
    {
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<ArturBasePlayerController>();
    }
}
