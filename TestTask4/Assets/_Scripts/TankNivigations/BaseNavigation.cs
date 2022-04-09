using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseNavigation : MonoBehaviour
{
    public abstract void Init();
    public abstract void NavigationOff();
    public virtual Vector3 GetDirection()
    {
        return Vector3.zero;
    }
}
