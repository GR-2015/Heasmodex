using UnityEngine;

public abstract class InputSource
{
    protected InputValues InupValues;
    protected InputNames InputNames;

    public abstract void GetInputValues();

    protected void SetMovementVectr(float x, float y)
    {
        InupValues.MovementVector = new Vector2(x, y);
    }
}