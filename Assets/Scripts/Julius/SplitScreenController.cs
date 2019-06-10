using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitScreenController : MonoBehaviour
{
    public Camera cameraLeft;
    public Camera cameraRight;
    public bool isSplitted;

    private GameObject player1;
    private GameObject player2;
    private Vector3 cameraOffsetPlayer1;
    private Vector3 cameraOffsetPlayer2;
    private Camera mainCamera;
    private GameObject leadingPlayer;
    private float cameraSmoothing;
    private bool needsSplitScreenTransition = true;
    private bool splitScreenTransitionDone = true;
    
    // Start is called before the first frame update
    void Start()
    {
        //player1 = cameraLeft.GetComponent<Julius.CameraController>().player;
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
        cameraOffsetPlayer1 = cameraLeft.transform.position - player1.transform.position;
        cameraOffsetPlayer2 = cameraRight.transform.position - player2.transform.position;

        isSplitted = false;
    }

    void Update()
    {
        float distance = player1.transform.position.x - player2.transform.position.x;
        int leading = 1;
        if(distance < 0)
        {
            leading = 2;
        }

        if(Mathf.Abs(distance) < 20)
        {
            isSplitted = false;
            Vector3 offset;
            if (leading == 1)
            {
                mainCamera = cameraLeft;
                leadingPlayer = player1;
                offset = cameraOffsetPlayer1;
                cameraRight.gameObject.SetActive(false);
            }
            else
            {
                mainCamera = cameraRight;
                leadingPlayer = player2;
                offset = cameraOffsetPlayer2;
                cameraLeft.gameObject.SetActive(false);
            }
            mainCamera.gameObject.SetActive(true);
            mainCamera.rect = new Rect(0, 0, 1, 1);
            mainCamera.transform.position = new Vector3(leadingPlayer.transform.position.x, leadingPlayer.transform.position.y, distance) + offset;
            splitScreenTransitionDone = false;
            needsSplitScreenTransition = true;
        }
        else
        {
            // Split screen
            isSplitted = true;
            if (needsSplitScreenTransition)
            {
                cameraLeft.gameObject.SetActive(true);
                cameraRight.gameObject.SetActive(true);
                cameraLeft.rect = new Rect(0, 0, 0.5f, 1);
                cameraRight.rect = new Rect(0.5f, 0, 0.5f, 1);
                needsSplitScreenTransition = false;
            }
            
            if (splitScreenTransitionDone)
            {
                cameraLeft.transform.position = player1.transform.position + cameraOffsetPlayer1;
                cameraRight.transform.position = player2.transform.position + cameraOffsetPlayer2;
            }
            else
            {
                cameraSmoothing += Time.deltaTime * 0.2f;
                cameraLeft.transform.LookAt(player1.transform);
                cameraRight.transform.LookAt(player2.transform);
                cameraLeft.transform.position = Vector3.Lerp(cameraLeft.transform.position, player1.transform.position + cameraOffsetPlayer1, cameraSmoothing);
                cameraRight.transform.position = Vector3.Lerp(cameraRight.transform.position, player2.transform.position + cameraOffsetPlayer2, cameraSmoothing);
                if(cameraSmoothing > 1.0f)
                {
                    splitScreenTransitionDone = true;
                    cameraSmoothing = .0f;
                }
            }
        }
        
    }
}
