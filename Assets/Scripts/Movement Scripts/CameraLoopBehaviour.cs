using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLoopBehaviour : MonoBehaviour
{
    private Transform tr;
    private Camera mainCam;

    private Vector2 camMax = new Vector2();
    private Vector2 camMin = new Vector2();
    private Vector3 pos;

    private void Awake()
    {
        tr = transform;
        mainCam = Camera.main;
    }

    private void Start()
    {
        CalulcateCameraBounds();
    }

    private void FixedUpdate()
    {
        CheckPosition();   
    }

    private void CheckPosition()
    {
        pos = tr.position;

        if (pos.x < camMin.x) pos.x = camMax.x;
        if (pos.x > camMax.x) pos.x = camMin.x;
        if (pos.y < camMin.y) pos.y = camMax.y;
        if (pos.y > camMax.y) pos.y = camMin.y;

        tr.position = pos;
    }

    private void CalulcateCameraBounds()
    {
        camMax.x = mainCam.transform.position.x 
            + mainCam.orthographicSize * mainCam.aspect;
        camMin.x = mainCam.transform.position.x 
            - mainCam.orthographicSize * mainCam.aspect;

        camMax.y = mainCam.transform.position.y + mainCam.orthographicSize;
        camMin.y = mainCam.transform.position.y - mainCam.orthographicSize;
    }

}
