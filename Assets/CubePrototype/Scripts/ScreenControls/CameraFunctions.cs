using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFunctions : BaseScreenControls
{
    [Header("CAMERA & TARGET")]
    public Transform target;
    public Transform cam;
    [Header("ZOOM")]
    public float zoomStep = 5f;            // Zooming nextMovement(speed) while scolling mouse wheel
    public float scrollEffect;            // Zooming nextMovement(speed) while scolling mouse wheel
    public float zoomRadius = 4f;           // Set radius at the start of the game
    protected float minZoomRadius = 1f;      // takes camera closer
    protected float maxZoomRadius = 19f;        // takes camera further away
    [Header("MOUSE")]
    protected float mouseSensetivity = 10f;     // mouse swipe sensitivity on the screen
    public float swipeX = 0f;                   // 
    public float swipeY = 0f;                   //  
    protected float minSwipeY = 5f;
    protected float maxSwipeY = -50f;
    [Header("CAMERA TO CUBE STARTUP ADJUSTMENT")]
    public float screenOffSetValue;             // "0" puts the TARGET letrally at the CENTRE of the screen.   
    public float startAngleX = 0f;              // By assigning a value to "swioe" it will preset the camera angle ...
    public float startAngleY = 0f;              // ... at the start of the game  



    public override void UpdateZoomValues()
    {
        zoomRadius -= Input.mouseScrollDelta.y * zoomStep;
        zoomRadius = Mathf.Clamp(zoomRadius, minZoomRadius, maxZoomRadius);
    }


    public override void UpdateInputValues(bool horizontalDrag, bool verticalDrag)
    {
        // Processing vales of HORIZONTAL drag for rotation around Y axis 
        if (horizontalDrag && !verticalDrag)
        {
            swipeX += Input.GetAxis("Mouse X") * mouseSensetivity;
            if (swipeX > 360)
            {
                swipeX -= 360;
            }
            else if (swipeX < 0)
            {
                swipeX += 360;
            }
        }

        // Processing vales of VERTICAL drag for rotation around X axis 
        if (!horizontalDrag && verticalDrag)
        {
            swipeY = Mathf.Clamp(swipeY -= Input.GetAxis("Mouse Y") * mouseSensetivity, -50, 5);
        }
    }


    public override void CameraLateUpdate()
    {

        // Dinamically register the RADIUS from CAMERA to PIVOT-POINT  
        Vector3 orbitRadius = Vector3.forward * zoomRadius;

        // Calculating ROTATION values around the target on X and Y axis 
        orbitRadius = Quaternion.Euler(swipeY, swipeX, 0) * orbitRadius;

        // APPLYING values to CAMERA
        cam.transform.position = target.transform.position + orbitRadius;

        // Manage the TARGETS position On the screen. Positive value is looking ABOVE the TARGTE  
        Vector3 screenOffSet = new Vector3(0, screenOffSetValue, 0);
        cam.transform.LookAt(target.position + screenOffSet);

    }

}
