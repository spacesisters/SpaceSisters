using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MichaelGravityReverseField : MonoBehaviour
{
    Vector3 collision_enter_p1, collision_exit_p1, collision_enter_velocity_p1;
    Vector3 collision_enter_p2, collision_exit_p2, collision_enter_velocity_p2;

    void Start()
    {
        collision_enter_p1 = new Vector3(0,0,0);
        collision_exit_p1 = new Vector3(0,0,0);
        collision_enter_p2 =new Vector3(0,0,0);
        collision_exit_p2 = new Vector3(0,0,0);
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject collided_object = other.gameObject;

        if(collided_object.tag == "Player1")
        {
            ArturBasePlayerController player = collided_object.GetComponent<ArturBasePlayerController>();
            collision_enter_p1 = collided_object.transform.position - this.transform.position;
            collision_enter_velocity_p1 = collided_object.GetComponent<Rigidbody>().velocity;
        }
        else if(collided_object.tag == "Player2")
        {
            ArturBasePlayerController player = collided_object.GetComponent<ArturBasePlayerController>();
            collision_enter_p2 = collided_object.transform.position - this.transform.position;
            collision_enter_velocity_p2 = collided_object.GetComponent<Rigidbody>().velocity;
        }
    }

    void OnTriggerExit(Collider other)
    {
        GameObject collided_object = other.gameObject;

        if(collided_object.tag == "Player1")
        {
            ArturBasePlayerController player = collided_object.GetComponent<ArturBasePlayerController>();
            collision_exit_p1 = collided_object.transform.position - this.transform.position;

            if (this.transform.rotation.eulerAngles.z == 0 || this.transform.rotation.eulerAngles.z == 180)
            {
                if(collision_enter_velocity_p1.x > 0 && collision_enter_p1.x <= collision_exit_p1.x)
                {
                    player.ReverseGravity();
                }
                else if(collision_enter_velocity_p1.x < 0 && collision_enter_p1.x >= collision_exit_p1.x)
                {
                    player.ReverseGravity();
                }
            }
            else if (this.transform.rotation.eulerAngles.z == 90 || this.transform.rotation.eulerAngles.z == -90)
            {
                if(collision_enter_velocity_p1.y > 0 && collision_enter_p1.y <= collision_exit_p1.y)
                {
                    player.ReverseGravity();
                }
                else if(collision_enter_velocity_p1.y < 0 && collision_enter_p1.y >= collision_exit_p1.y)
                {
                    player.ReverseGravity();
                }
            }

            collision_enter_p1 = new Vector3(0,0,0);
            collision_exit_p1 = new Vector3(0,0,0);
            collision_enter_velocity_p1 = new Vector3(0,0,0);
        }
        else if(collided_object.tag == "Player2")
        {
            ArturBasePlayerController player = collided_object.GetComponent<ArturBasePlayerController>();
            collision_exit_p2 = collided_object.transform.position - this.transform.position;

            if (this.transform.rotation.eulerAngles.z == 0 || this.transform.rotation.eulerAngles.z == 180)
            {
                if(collision_enter_velocity_p2.x > 0 && collision_enter_p2.x <= collision_exit_p2.x)
                {
                    player.ReverseGravity();
                }
                else if(collision_enter_velocity_p2.x < 0 && collision_enter_p2.x >= collision_exit_p2.x)
                {
                    player.ReverseGravity();
                }
            }
            else if (this.transform.rotation.eulerAngles.z == 90 || this.transform.rotation.eulerAngles.z == -90)
            {
                if(collision_enter_velocity_p2.y > 0 && collision_enter_p2.y <= collision_exit_p2.y)
                {
                    player.ReverseGravity();
                }
                else if(collision_enter_velocity_p2.y < 0 && collision_enter_p2.y >= collision_exit_p2.y)
                {
                    player.ReverseGravity();
                }
            }
            collision_enter_p2 = new Vector3(0,0,0);
            collision_exit_p2 = new Vector3(0,0,0);
            collision_enter_velocity_p2 = new Vector3(0,0,0);
        }
    }

}
