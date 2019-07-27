using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlePosition : MonoBehaviour
{
    public Vector3 position;

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }

    // Update is called once per frame
    void Update()
    {
        float xDist;

        if(player1.transform.position.x > player2.transform.position.x)
        {
            xDist = player1.transform.position.x;
        }
        else
        {
            xDist = player2.transform.position.x;
        }

        transform.position = position + new Vector3(xDist, 0.0f, 0.0f);
    }
}
