using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : BaseTank
{
    public override void TankInit()
    {
        base.TankInit();
        Shot();
    }
    public override void Respawn()
    {
        base.Respawn();
        Shot();
    }
}
