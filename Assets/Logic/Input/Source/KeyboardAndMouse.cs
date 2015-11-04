using UnityEngine;

public class KeyboardAndMouse : BaseInputSource
{
    private const string PrefabName = "KeyboardAndMouseInputNames";

    public KeyboardAndMouse(InputValues inputValues)
    {
        InputNames = (Resources.Load(PrefabName, typeof(GameObject)) as GameObject).GetComponent<InputNames>();
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