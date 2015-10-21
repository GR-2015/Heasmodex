using UnityEngine;

public class InputValues
{
    public PlayerController Owner { get; private set; }
    public Vector2 MovementVector { get; set; }

    public InputValues(PlayerController owner)
    {
        Owner = owner;
    }
}