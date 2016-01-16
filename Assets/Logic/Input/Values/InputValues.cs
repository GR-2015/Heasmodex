using UnityEngine;

public class InputValues
{
    public BaseCharacterController Owner { get; private set; }

    #region Input axes

    public Vector2 MovementAxes { get; set; }
    public Vector2 MouseAxes { get; set; }
    public Vector2 MousePosition { get; set; }

    #endregion Input axes

    #region Buttons

    public ButtonState JumpButton = ButtonState.Released;
    public ButtonState MeleeAttack = ButtonState.Released;

    #endregion Buttons

    public InputValues(BaseCharacterController owner)
    {
        Owner = owner;
        Owner.InputValues = this;
    }
}

public enum ButtonState
{
    Down,
    Pressed,
    Up,
    Released
}

public enum AxisType
{
    Normal,
    Raw
}