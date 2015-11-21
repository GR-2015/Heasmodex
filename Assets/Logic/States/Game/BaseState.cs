using UnityEngine;

public abstract class BaseState
{
    // Use this for initialization
    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void Sleep() {}

    public virtual void Return() {}

    public virtual void StateUpdate() {}

    public abstract void StateFixedUpdate();

    public virtual void StateLateUpdate() {}
}