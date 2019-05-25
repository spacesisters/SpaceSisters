using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturEnemyController : MonoBehaviour
{
    public int hp;

    private CharacterController controller;
    private Vector3 moveDirection;
    private bool gotHit;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
        moveDirection = new Vector3();
    }

    void Update()
    {
        controller.Move(moveDirection * Time.deltaTime);
        if (hp == 0)
            Destroy(gameObject);
    }

    public void SetHit(bool hit, Vector3 hurtBoxPosition, float knockback)
    {
        gotHit = hit;
        if (hurtBoxPosition.x < transform.position.x)
        {
            moveDirection.x = knockback;
        }
        else
        {
            moveDirection.x = -knockback;
        }

        StartCoroutine(Knockback());
        hp--;
    
    }

    public IEnumerator Knockback()
    {
        yield return new WaitForSeconds(0.5f);
        moveDirection.x = 0;
    }
}
