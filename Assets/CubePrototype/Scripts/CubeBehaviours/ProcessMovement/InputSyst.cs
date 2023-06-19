using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSyst : Directions
{

    public Transform grope;
    public Transform upTileTest;
    public Transform downTileTest;

    private bool flipUp = false;
    private bool flipDown = false;



    public float buttonSensitivity = 0.2f;

    private void Update()
    {
        if (GManager.Instance.waitForFlip)
        {
            DirectionInput();
        }
    }

    public void DirectionInput()
    {
        ConvertLocalToWorld();

        if (Input.GetAxis("Horizontal") > buttonSensitivity)
        {
            StartCoroutine(cubeDriver.FloorGroper(worldRight, parentWorld, childLocal, grope, upTileTest, downTileTest, flipUp, flipDown));
            return;
        }

        if (Input.GetAxis("Horizontal") < -buttonSensitivity)
        {
            StartCoroutine(cubeDriver.FloorGroper(worldLeft, parentWorld, childLocal, grope, upTileTest, downTileTest, flipUp, flipDown));
            return;
        }

        if (Input.GetAxis("Vertical") > buttonSensitivity)
        {
            StartCoroutine(cubeDriver.FloorGroper(worldForward, parentWorld, childLocal, grope, upTileTest, downTileTest, flipUp, flipDown));
            return;
        }

        if (Input.GetAxis("Vertical") < -buttonSensitivity)
        {
            StartCoroutine(cubeDriver.FloorGroper(worldBack, parentWorld, childLocal, grope, upTileTest, downTileTest, flipUp, flipDown));
            return;
        }
    }

}
