using UnityEngine;

public class InputValues
{
    public PlayerController Owner { get; private set; }

    #region Input axes

    public Vector2 MovementVector { get; set; }

    #endregion

    #region Buttons

    public ButtonState JumpButton;
    public ButtonState MeleeAttack;


    #endregion

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