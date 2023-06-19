using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScreenControlOutput : CameraFunctions
{

    [Header("CursorRelated")]
    public Texture2D horizontalTexture;
    public Texture2D verticalTexture;
    private Rect topH_Rec;
    private Rect leftV_Rec;
    private Rect central_Rec;
    Movement inputDirection = new Movement();

    private void Update()
    {
        ScreenAreas();

        UpdateZoomValues();

        bool horizontalDrag = topH_Rec.Contains(Input.mousePosition);
        bool verticalDrag = leftV_Rec.Contains(Input.mousePosition);
        bool centralDrag = central_Rec.Contains(Input.mousePosition);

        if (ScreenActive())
        {
            Cursor.visible = true;
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

            if (horizontalDrag && !verticalDrag)
            {
                Cursor.SetCursor(horizontalTexture, Vector2.zero, CursorMode.Auto);
            }

            if (!horizontalDrag && verticalDrag)
            {
                Cursor.SetCursor(verticalTexture, Vector2.zero, CursorMode.Auto);
            }

        }
        else
        {
            Cursor.visible = false;
            UpdateInputValues(horizontalDrag, verticalDrag);

            if (centralDrag)
            {
                Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);

                if (GManager.Instance.waitForFlip && Input.GetMouseButton(0))   // Input.GetMouseButtonDown(0)
                {
                    if (Input.GetAxis("Mouse X") > .5)
                        StartCoroutine(inputDirection.ForwardDirector(2f, -1, transform));

                    if (Input.GetAxis("Mouse X") < -.5)
                        StartCoroutine(inputDirection.ForwardDirector(2f, 1, transform));
                }
            }

        }
    }


    private void LateUpdate()
    {
        CameraLateUpdate();
    }


    void ScreenAreas()
    {
        float unitProportion = 6;
        float width = Screen.width;
        float height = Screen.height;

        float vertWidth = width / unitProportion;
        float horizHeight = height / unitProportion;

        topH_Rec = new Rect(0, height - horizHeight, width, horizHeight);
        leftV_Rec = new Rect(0, 0, vertWidth, height);
        central_Rec = new Rect(width / 3, height / 3, width / 3, height / 3);
    }


    protected bool ScreenActive()
    {
        bool screenActive = true;

        if (Input.GetButton("Fire1"))
            screenActive = false;
        if (Input.GetButtonUp("Fire1"))
            screenActive = true;
        return screenActive;
    }

}
