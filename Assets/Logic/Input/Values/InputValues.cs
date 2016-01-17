using UnityEngine;

public class InputValues
{
    public BaseCharacterController Owner { get; private set; }

    #region Input axes

    public Vector2 MovementAxes = Vector2.zero;
    public Vector2 MouseAxes = Vector2.zero;
    public Vector2 MousePosition = Vector2.zero;

    #endregion Input axes

    #region Buttons

    public ButtonState JumpButton = ButtonState.Released;
    public ButtonState Attack = ButtonState.Released;

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