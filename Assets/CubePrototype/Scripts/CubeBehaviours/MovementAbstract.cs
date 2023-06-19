using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovementAbstract
{

    public abstract bool RayCaster(Vector3 rayCastDerivingPoint, Vector3 rayCastDirection);

    public abstract IEnumerator FloorGroper(Vector3 gropingDirection, Transform cube, Transform directional, Transform grope, Transform upTileTest, Transform downTileTest, bool flipUp, bool flipDown);

    public abstract IEnumerator MovementBehaviour(Vector3 direction, Transform cube, Transform directional, bool flipUp, bool flipDown);

    public abstract IEnumerator ForwardDirector(float turnDuration, float turnDirection, Transform turnable);


}
