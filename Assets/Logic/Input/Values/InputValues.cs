using UnityEngine;

public class InputValues
{
    public PlayerController Owner { get; private set; }

    #region Input axes

    public Vector2 MovementAxes { get; set; }
    public Vector2 MouseAxes { get; set; }
    public Vector2 MousePosition { get; set; }

    #endregion Input axes

    #region Buttons

    public ButtonState JumpButton;
    public ButtonState MeleeAttack;

    #endregion Buttons

    public InputValues(PlayerController owner)
    {
        Owner = owner;
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