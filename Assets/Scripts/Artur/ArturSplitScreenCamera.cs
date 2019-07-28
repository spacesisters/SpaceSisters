using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArturSplitScreenCamera : MonoBehaviour
{
    public Vector3 baseOffset;
    public float maxPlayerDistance;
    public float smoothTime;
    public float minZoom = 60f;
    public float maxZoom = 30f;
    public float zoomLimiter = 50f;

    public bool isSplitted;

    private GameObject player1;
    private GameObject player2;

    private Vector3 velocity;
    private Camera cam;

    private Camera cameraLeft, cameraRight;


    private void Start()
    {
        cameraLeft = GameObject.FindGameObjectWithTag("SimpleCameraLeft").GetComponent<Camera>();
        cameraRight = GameObject.FindGameObjectWithTag("SimpleCameraRight").GetComponent<Camera>();
        cam = GetComponent<Camera>();
        isSplitted = false;
        SetPlayers();
    }

    private void Update()
    {
        if (player1 == null || player2 == null)
        {
            SetPlayers();
        }
    }

    private void LateUpdate()
    {
        

        Move();
        Zoom();

        if ((GetPlayerDistanceX() > maxPlayerDistance) || (GetPlayerDistanceY() > maxPlayerDistance))
        {
            cameraLeft.enabled = true;
            cameraRight.enabled = true;
            cam.enabled = false;
            isSplitted = true;
        }
        else
        {
            cameraLeft.enabled = false;
            cameraRight.enabled = false;
            cam.enabled = true;
            isSplitted = false;
        }
    }


    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetPlayerDistanceX() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    private void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + baseOffset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    private void SetPlayers()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");
    }


    private Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(player1.transform.position, Vector3.zero);
        bounds.Encapsulate(player2.transform.position);
        return bounds.center;
    }

    private float GetPlayerDistanceX()
    {
        var bounds = new Bounds(player1.transform.position, Vector3.zero);
        bounds.Encapsulate(player2.transform.position);
        return bounds.size.x;
    }

    private float GetPlayerDistanceY()
    {
        var bounds = new Bounds(player1.transform.position, Vector3.zero);
        bounds.Encapsulate(player2.transform.position);
        return bounds.size.y;
    }
}
