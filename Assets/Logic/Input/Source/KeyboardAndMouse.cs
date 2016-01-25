using UnityEngine;

public class KeyboardAndMouse : BaseInputSource
{
    protected override string PrefabName { get { return "KeyboardAndMouseInputNames"; } }

    public KeyboardAndMouse(InputValues inputValues)
    {
        InputNames = Resources.Load(PrefabName, typeof(InputNames)) as InputNames;
        this.InputValues = inputValues;
    }

    public override void GetInputValues()
    {
        float x = GetAxis(InputNames.MovementXaxis, AxisType.Raw);
        float y = GetAxis(InputNames.MovementYaxis, AxisType.Raw);

        InputValues.MovementAxes = new Vector2(x, y);

        x = GetAxis(InputNames.MouseX);
        y = GetAxis(InputNames.MouseY);

        InputValues.MouseAxes = new Vector2(x, y);

        InputValues.MousePosition = Input.mousePosition;

        InputValues.JumpButton = GetButtonState(InputNames.JumpButton);
        InputValues.Attack = GetButtonState(InputNames.MeleeAttackButton);
        InputValues.Pause = GetButtonState(InputNames.PauseButton);
    }
}