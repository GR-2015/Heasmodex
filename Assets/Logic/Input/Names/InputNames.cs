using UnityEngine;

public class InputNames : ScriptableObject
{
    [SerializeField] private string _movementXaxis = "MovementXaxis";
    [SerializeField] private string _movementYaxis = "MovementYaxis";

    public string MovementXaxis { get { return _movementXaxis; } }
    public string MovementYaxis { get { return _movementYaxis; } }

    [SerializeField] private string _mouseX = "Mouse X";
    [SerializeField] private string _mouseY = "Mouse Y";

    public string MouseX { get { return _mouseX; } }
    public string MouseY { get { return _mouseY; } }

    [SerializeField] private string _jumpButton = "Jump";
    
    public string JumpButton { get { return _jumpButton; } }

    [SerializeField] private string _meleeAttackButton = "Attack";

    public string MeleeAttackButton { get { return _meleeAttackButton; } }
}