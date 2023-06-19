using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INDICATORS : MonoBehaviour
{

    Movement movement = new Movement();
    public Vector3 eulr;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        eulr = movement.eulr ;

    }
}
