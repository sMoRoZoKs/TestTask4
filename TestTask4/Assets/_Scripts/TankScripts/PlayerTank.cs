using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTank : BaseTank
{
    protected override void TankUpdate()
    {
        base.TankUpdate();
        CheckClick();
    }
    private void CheckClick()
    {
        if(Input.GetMouseButton(0))
        {
            MouseDown();
        }
        else 
        {
            MouseUp();
        }
    }
    private void MouseUp()
    {
        weapoon.FireStop();
    }
    private void MouseDown()
    {
        weapoon.FireStart();
    }
    private void OnTriggerEnter(Collider other)
    {
        BaseTank tank = other.GetComponent<BaseTank>();
        if(tank && tank.TeamId != TeamId)
        {
            Dead();
        }
    }
}
