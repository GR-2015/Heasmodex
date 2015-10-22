using UnityEngine;

public abstract class BaseInputSource
{
    protected InputValues InupValues;
    protected InputNames InputNames;

    public abstract void GetInputValues();

    protected void SetMovementVectr(float x, float y)
    {
        InupValues.MovementVector = new Vector2(x, y);
    }
}