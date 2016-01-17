using UnityEngine;
using System.Collections;

public class DeathState : BaseState
{
    public override void StateUpdate()
    {
        Debug.Log("Game over!");
    }
}
