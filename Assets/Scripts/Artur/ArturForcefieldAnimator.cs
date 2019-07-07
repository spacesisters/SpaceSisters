﻿using System.Collections;
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
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<ArturPlayerOneController>();
    }

    private void Update()
    {
        bool doForcefield = player.GetDoForcefield();
        animator.SetBool("isForcefielding", doForcefield);
    }

}