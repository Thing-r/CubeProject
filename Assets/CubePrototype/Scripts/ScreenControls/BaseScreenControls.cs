using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScreenControls : MonoBehaviour
{

    public abstract void UpdateInputValues(bool horizintalDrag, bool verticalDrag);

    public abstract void UpdateZoomValues();

    public abstract void CameraLateUpdate();


}
