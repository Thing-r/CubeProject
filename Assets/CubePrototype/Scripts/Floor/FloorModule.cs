
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorModule : Floor_Data
{
    public GameObject[] floorTiles = new GameObject[6];


    public IEnumerator FloorBulder(Transform origin, bool changeXDirection)
    {
        Vector3 originPosition = origin.position;

        origin.transform.localEulerAngles = new Vector3(0, 0, 0);

        Quaternion originRotation = origin.rotation;

        for (int fi = 0; fi < floorLenght; fi++)
        {
            float xAngle = 90;
            float yPos = 0;
            float zPos = 0 + fi;

            if (fi == step)
            {
                xAngle = 0;
                yPos = 0.5f;
                zPos -= .5f;
            }

            if (fi > step)
            {
                xAngle = 90;
                yPos = 1f;
                zPos--;
            }


            for (int wi = 0; wi < floorWidth; wi++)
            {
                int xSelection = Random.Range(0, 6);
                GameObject xFloor = Instantiate(floorTiles[xSelection], originPosition, originRotation, origin);


                float xPos = wi + (0 * wi);  // spacer stuff (currently not in use)

                //Laying out on X axis from 0 to minus / Right to Left
                if (changeXDirection == true)
                    xPos *= -1;

                // defining positiona and angles
                xFloor.transform.localEulerAngles = new Vector3(xAngle, 0, 0);
                xFloor.transform.localPosition = new Vector3(xPos, yPos, zPos);
                TileStausNames(xFloor, fi, wi, changeXDirection);

                yield return new WaitForSeconds(.1f);
            }
        }
    }

    void TileStausNames(GameObject floor, int fi, int wi, bool changed)
    {
        // defining "LeftEnd" and "RightEnd" deriving points for STEP
        if ((wi == 0) && (fi == 0))
        {
            floor.name = "Start_LH" + wi + "-" + fi;
        }
        else if (changed == false)
        {
            if ((wi == 0) && (fi == floorLenght - 1))
            {
                leftEnd = floor.transform;  // LeftEnd tile transform
                floor.name = "LEFT" + wi + "-" + fi;
            }
            else if ((wi == floorWidth - 1) && (fi == floorLenght - 1))
            {
                rightEnd = floor.transform;  // RightEnd tile transform
                floor.name = "RIGHT" + wi + "-" + fi;
            }
        }
        else if (changed == true)
        {
            if ((wi == 0) && (fi == (floorLenght - 1)))
            {
                rightEnd = floor.transform;  // RightEnd tile transform
                floor.name = "RIGHT" + wi + "-" + fi;
            }
            else if ((wi == (floorWidth - 1)* -1) && (fi == floorLenght - 1))
            {
                rightEnd = floor.transform;  // RightEnd tile transform
                floor.name = "RIGHT" + wi + "-" + fi;
            }
        }
        else
            floor.name = "F(z)" + wi + " W(x)-" + fi;
    }


    public Transform StraightFWD(Transform nextPosition)
    {
        GManager.Instance.changeXDirection = false;

        Transform derivingPoint = Instantiate(empty, nextPosition);

        derivingPoint.transform.localPosition = new Vector3(0, 1f, 0);
        derivingPoint.transform.localEulerAngles = new Vector3(-90f, 0, 0);
        return derivingPoint;
    }


    public Transform LeftTurn(Transform nextPosition)
    {
        GManager.Instance.changeXDirection = false;

        Transform derivingPoint = Instantiate(empty, nextPosition);

        derivingPoint.transform.localPosition = new Vector3(0, 1f, 0);
        derivingPoint.transform.localEulerAngles = new Vector3(0f, -90f, 90f);
        return derivingPoint;
    }

    public Transform RightTurn(Transform nextPosition)
    {
        GManager.Instance.changeXDirection = true;

        Transform derivingPoint = Instantiate(empty, nextPosition);

        derivingPoint.transform.localPosition = new Vector3(0, 1, 0);
        derivingPoint.transform.localEulerAngles = new Vector3(0f, 90f, -90f);
        return derivingPoint;
    }
}




/*
    public Transform Straight(Transform nextPosition)
    {
                Vector3 pos = nextPosition.position; // new Vector3(0, 0, 0);
                Quaternion angle = Quaternion.Euler(-90, 0, 0);

        Transform emptyPoint = Instantiate(empty, nextPosition);

                float x = emptyPoint.transform.localPosition.x;
                float y = emptyPoint.transform.localPosition.y;
                float z = emptyPoint.transform.localPosition.z;

        emptyPoint.transform.localPosition = new Vector3(0, 1f, 0);
        emptyPoint.transform.localEulerAngles = new Vector3(-90f, 0, 0);
        return emptyPoint;

        // Vector3 pos = new Vector3(0, 0, 1);
        // Quaternion angle = Quaternion.Euler(0, 0, 0);
        // Transform clonePoint = Instantiate(fmOrigin, pos, angle, nextPosition);

    }
*/
