using UnityEngine;

public abstract class BaseInputSource
{
    protected InputValues InputValues;
    protected InputNames InputNames;

    protected virtual string PrefabName { get { return null; } }

    public abstract void GetInputValues();

    protected float GetAxis(string axisName, AxisType type = AxisType.Normal)
    {
        float value = 0f;

        value = type == AxisType.Normal ? Input.GetAxis(axisName) : Input.GetAxisRaw(axisName);

        return value;
    }

    protected ButtonState GetButtonState(string buttonName)
    {
        if (Input.GetButtonDown(buttonName))
        {
            return ButtonState.Down;
        }

        if (Input.GetButton(buttonName))
        {
            return ButtonState.Pressed;
        }

        if (Input.GetButtonUp(buttonName))
        {
            return ButtonState.Up;
        }

        return ButtonState.Released;
    }
}