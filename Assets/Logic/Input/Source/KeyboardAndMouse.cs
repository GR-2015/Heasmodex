using UnityEngine;

public class KeyboardAndMouse : InputSource
{
    private const string PrefabName = "KeyboardAndMouseInputNames";

    public KeyboardAndMouse(InputValues inputValues)
    {
        InputNames = (Resources.Load(PrefabName, typeof(GameObject)) as GameObject).GetComponent<InputNames>();
        this.InupValues = inputValues;
    }

    public override void GetInputValues()
    {
        float x, y;

        x = Input.GetAxis(InputNames.MovementXaxis);
        y = 0;

        SetMovementVectr(x, y);
    }
}