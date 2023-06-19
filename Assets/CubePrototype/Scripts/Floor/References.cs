using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class References
{
    public FloorModule instance;
    public FloorModule floorModule;


    public Transform ref_PointOfOrigin;
    public int ref_NextCount;
    public int ref_Width = 5;
    public int ref_Lenght = 15;
    public int ref_Step = 15;

    public enum Destinations { STOP, LEFT, STRAIGHT, RIGHT };
    public Destinations nextDirection = Destinations.STOP;

    public void Setup()
    {
        // Get references to the components.
        floorModule = instance.GetComponent<FloorModule>();

        // Set the references.
        floorModule.floorWidth = ref_Width;
        floorModule.floorLenght = ref_Lenght;
        floorModule.step = ref_Step;

    }

    // Switch statement assigning the deriving point for next Module
    public void NewRefrences(int width)
    {
        switch (nextDirection)
        {
            case    Destinations.STRAIGHT:
                {
                    GManager.Instance.nextSpawnPoint = floorModule.StraightFWD(floorModule.leftEnd);
                    Debug.Log("STRAIGHT TRANSFORM S IS: ");
                }
                break;

            case Destinations.LEFT:
                {
                    GManager.Instance.nextSpawnPoint = floorModule.LeftTurn(floorModule.rightEnd);
                    Debug.Log("LEFT TRANSFORM IS: ");
                }
                break;

            case Destinations.RIGHT:
                {
                    GManager.Instance.nextSpawnPoint = floorModule.RightTurn(floorModule.leftEnd);
                    Debug.Log("RIGHT TRANSFORM: ");
                }
                break;

            default:
                {
                    Debug.Log("DEFAULT TRANSFORM:");
                }
                break;
        }
    }



}



