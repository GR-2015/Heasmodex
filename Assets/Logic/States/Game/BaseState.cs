public abstract class BaseState
{
    // Use this for initialization
    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void Sleep() {}

    public virtual void Return() {}

    public abstract void StateUpdate();

    public virtual void StateFixedUpdate() {}

    public virtual void StateLateUpdate() {}
}