using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Julius
{
    public class CameraController : MonoBehaviour
    {
        public GameObject player;
        private Vector3 offset;
        public int playerOneOrTwo;
        // Start is called before the first frame update
        void Start()
        {
            if(playerOneOrTwo == 1)
            {
                player = GameObject.FindGameObjectWithTag("Player1");
            }
            else
            {
                player = player = GameObject.FindGameObjectWithTag("Player2");
            }
            
            //offset = transform.position - player.transform.position;
        }

        // Update is called once per frame
        void LateUpdate()
        {
            //transform.position = player.transform.position + offset;
        }
    }
}
