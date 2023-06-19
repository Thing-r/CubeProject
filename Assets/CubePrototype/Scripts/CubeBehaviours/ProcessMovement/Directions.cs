using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions : MonoBehaviour
{

    protected Movement cubeDriver = new Movement();


    [Header("OBJECTS")]
    public Transform parentWorld;
    public Transform childLocal;
    [Space]
    [Header("LOCAL & WORLD")]
    protected Vector3 localDirection;
    public Vector3 worldForward;
    public Vector3 worldBack;
    public Vector3 worldRight;
    public Vector3 worldLeft;

    private void Start()
    {
        localDirection = childLocal.transform.eulerAngles;
    }



    public void ConvertLocalToWorld()
    {
        worldForward = childLocal.TransformDirection(localDirection.x, 0, localDirection.z + 1);
        worldBack = childLocal.TransformDirection(localDirection.x, 0, localDirection.z - 1);
        worldRight = childLocal.TransformDirection(localDirection.x + 1, 0, localDirection.z);
        worldLeft = childLocal.TransformDirection(localDirection.x - 1, 0, localDirection.z);
    }

}
