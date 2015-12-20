using UnityEngine;

public class KeyboardAndMouse : BaseInputSource
{
    protected override string PrefabName { get { return "KeyboardAndMouseInputNames"; } }

    public KeyboardAndMouse(InputValues inputValues)
    {
        InputNames = Resources.Load(PrefabName, typeof(InputNames)) as InputNames;
        this.InupValues = inputValues;
    }

    public override void GetInputValues()
    {
        var x = GetAxis(InputNames.MovementXaxis, AxisType.Raw);
        var y = GetAxis(InputNames.MovementYaxis, AxisType.Raw);

        InupValues.MovementVector = new Vector2(x, y);

        InupValues.JumpButton = GetButtonState(InputNames.JumpButton);
        InupValues.MeleeAttack = GetButtonState(InputNames.MeleeAttackButton);
    }
}