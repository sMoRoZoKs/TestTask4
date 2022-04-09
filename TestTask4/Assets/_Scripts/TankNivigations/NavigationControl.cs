using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationControl : BaseNavigation
{
    private Vector3 _direction;
    private void OnGUI()
    {
        if(Input.GetKey("w"))
        {
            _direction = Vector3.forward;
        }
        else if(Input.GetKey("s"))
        {
            _direction = Vector3.back;
        }
        else if(Input.GetKey("a"))
        {
            _direction = Vector3.left;
        }
        else if(Input.GetKey("d"))
        {
            _direction = Vector3.right;
        }
        else 
        {
            _direction = Vector3.zero;
        }
    }
    public override Vector3 GetDirection()
    {
        return _direction;
    }

    public override void Init()
    {
        
    }

    public override void NavigationOff()
    {
        
    }
}
