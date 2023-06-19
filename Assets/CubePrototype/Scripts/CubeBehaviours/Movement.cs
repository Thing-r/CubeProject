using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MovementAbstract
{

    [Space]
    [Header("MOVEMENT")]
    public float rollDuration = 1f; //

    [Space]
    [Header("GROPING")]
    public bool waitForGroping = true;
    public int frames = 0;

    [Space]
    [Header("CUBE FLOOR MOVEMENT PARAMs")]
    Vector3 rollAxis;
    Vector3 spinPoint;
    Quaternion endRotation;
    Vector3 endPosition;


    /// <summary>
    /// Attach this script to any gameObject for which you want to put a note.
    /// </summary>
    /// 
    public override IEnumerator MovementBehaviour(Vector3 direction, Transform cube, Transform directional, bool flipUp, bool flipDown)
    {
        GManager.Instance.waitForFlip = false;

        float timeLeft = 0;
        float halfWidth = cube.transform.localScale.x / 2;


        rollAxis = Vector3.Cross(Vector3.up, direction); // Rotation Axsi

        Quaternion rotation = cube.transform.rotation;   // registering ANGLE BEFORE spin

        Quaternion resetDirection = directional.transform.rotation;

        float rollAngle = 90;
        float upDownAngle = 180;

        if (!flipDown && !flipUp)
        {
            spinPoint = cube.transform.position + (Vector3.down * halfWidth) + (direction * halfWidth);// spin point // those are world 
            endRotation = Quaternion.AngleAxis(rollAngle, rollAxis) * rotation;
            endPosition = cube.transform.position + direction;
        }


        if (flipUp && !flipDown)
        {
            rollAngle = upDownAngle;

            rollAxis = Vector3.Cross(Vector3.up, direction);
            spinPoint = cube.transform.position + (Vector3.up * halfWidth) + (direction * halfWidth);//
            endRotation = Quaternion.AngleAxis(rollAngle, rollAxis) * rotation;
            endPosition = cube.transform.position + direction + new Vector3(0, 1, 0);
        }

        if (flipDown && !flipUp)
        {
            rollAngle = upDownAngle;

            rollAxis = Vector3.Cross(Vector3.up, direction);
            spinPoint = cube.transform.position + (Vector3.down * halfWidth) + (direction * halfWidth);//
            endRotation = Quaternion.AngleAxis(rollAngle, rollAxis) * rotation;
            endPosition = cube.transform.position + direction + new Vector3(0, -1, 0);
        }

        float oldAngle = 0;
        float I_oldAngle = 0;

        while (timeLeft < rollDuration)
        {

            yield return new WaitForEndOfFrame();
            timeLeft += Time.deltaTime;                                 // Counts (+ / ads) time until condition stops it

            float newAngle = (timeLeft / rollDuration) * rollAngle;     // calculating angle through the period of time
            float stepByStepRoll = newAngle - oldAngle;                  // Calculating the spin angle throught the period of time
            oldAngle = newAngle;
            cube.transform.RotateAround(spinPoint, rollAxis, stepByStepRoll);

            float I_newAngle = (timeLeft / rollDuration) * -rollAngle;     // calculating angle through the period of time
            float I_stepByStepRoll = I_newAngle - I_oldAngle;              // Calculating the spin angle throught the period of time
            I_oldAngle = I_newAngle;
            directional.transform.RotateAround(directional.transform.position, rollAxis, I_stepByStepRoll);
        }

        cube.transform.position = endPosition;
        cube.transform.rotation = endRotation;

        directional.transform.rotation = resetDirection; // addad
        GManager.Instance.waitForFlip = true;

    }



    /// <summary>
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// Attach this script to any gameObject for which you want to put a note.
    /// </summary>
    /// 


    /// <summary>
    /// Attach this script to any gameObject for which you want to put a note.
    /// </summary>
    /// 
    public override IEnumerator FloorGroper(Vector3 gropingDirection, Transform cube, Transform directional, Transform gropeRig, Transform upTileTest, Transform downTileTest, bool flipUp, bool flipDown)
    {
       
        if (gropingDirection == Vector3.forward)
            gropeRig.transform.eulerAngles = new Vector3(0, 0, 0);

        if (gropingDirection == Vector3.right)
            gropeRig.transform.eulerAngles = new Vector3(0, 90, 0);

        if (gropingDirection == Vector3.back)
            gropeRig.transform.eulerAngles = new Vector3(0, 180, 0);

        if (gropingDirection == Vector3.left)
            gropeRig.transform.eulerAngles = new Vector3(0, -90, 0);

        while (frames < 3)
        {
            yield return new WaitForEndOfFrame();

            for (frames = 0; frames < 3; frames++)
            {

                if (frames == 0)
                {
                    Vector3 derivingPoint = upTileTest.transform.position;
                    Vector3 rotationAngles = upTileTest.transform.rotation * Vector3.down;

                    if (RayCaster(derivingPoint, rotationAngles))
                    {
                        flipUp = false;
                        flipDown = false;
                    }
                }
                if (frames == 1)
                {
                    Vector3 derivingPoint = upTileTest.transform.position;
                    Vector3 rotationAngles = upTileTest.transform.rotation * Vector3.back;

                    if (RayCaster(derivingPoint, rotationAngles))
                    {
                        flipUp = true;
                        flipDown = false;
                    }
                }
                if (frames == 2)
                {
                    Vector3 derivingPoint = downTileTest.transform.position;
                    Vector3 rotationAngles = upTileTest.transform.rotation * Vector3.back;

                    if (RayCaster(derivingPoint, rotationAngles))
                    {
                        flipUp = false;
                        flipDown = true;
                    }
                }
                if (frames == 3)
                {

                }
            }
        }

        yield return MovementBehaviour(gropingDirection, cube, directional, flipUp, flipDown);

        gropeRig.transform.localEulerAngles = new Vector3(0, 0, 0);

        frames = 0;
    }


    /// <summary>
    /// 
    /// 
    /// 
    /// Attach this script to any gameObject for which you want to put a note.
    /// </summary>
    /// 

    public override bool RayCaster(Vector3 rayCastDerivingPoint, Vector3 rayCastDirection)
    {
        LayerMask mask = LayerMask.GetMask("FloorTile");
        RaycastHit hit;
        float distance = 0.8f;
        
        
        
        if (Physics.Raycast(rayCastDerivingPoint, rayCastDirection, out hit, distance, mask))
        {
            Debug.Log("object name is " + hit.collider.name);
            //Debug.DrawRay(rayCastDerivingPoint, rayCastDirection * distance, Color.green);
            return true;
        }
        else
        {
            Debug.DrawRay(rayCastDerivingPoint, rayCastDirection * distance, Color.magenta);
            //Debug.Log("Did not Hit");
            return false;

        }
    }

    public Vector3 eulr;

    /// <summary>
    /// Attach this script to any gameObject for which you want to put a note.
    /// </summary>
    /// 
    public override IEnumerator ForwardDirector(float turnDuration, float turnDirection, Transform turnable)
    {
        GManager.Instance.waitForFlip = false;

        float timeLapse = 0;
        float rollAngle = 90 * turnDirection;

        //Quaternion startRotation = turnable.transform.rotation;
        Quaternion targetRotation = turnable.transform.rotation * Quaternion.Euler(0, rollAngle, 0);

        float oldAngleTiming = 0;

        while (timeLapse < turnDuration)
        {
            yield return new WaitForEndOfFrame();
            timeLapse += Time.deltaTime;                                    // Counts (+ / ads) time until condition stops it
            float newAngleTiming = (timeLapse / turnDuration) * rollAngle;  // calculating angle through the period of time
            float stepBtStepRoll = newAngleTiming - oldAngleTiming;         // Calculating the spin angle throught the period of time
            oldAngleTiming = newAngleTiming;

            turnable.transform.RotateAround(turnable.transform.position, Vector3.up, stepBtStepRoll);

        }

        turnable.transform.rotation = targetRotation;
        GManager.Instance.waitForFlip = true;
    }

}




