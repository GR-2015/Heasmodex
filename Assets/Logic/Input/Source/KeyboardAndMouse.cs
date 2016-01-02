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
        var x = GetAxis(InputNames.MovementXaxis, AxisType.Raw);
        var y = GetAxis(InputNames.MovementYaxis, AxisType.Raw);

        InputValues.MovementVector = new Vector2(x, y);

        InputValues.mousePosition = Input.mousePosition;

        InputValues.JumpButton = GetButtonState(InputNames.JumpButton);
        InputValues.MeleeAttack = GetButtonState(InputNames.MeleeAttackButton);
    }
}