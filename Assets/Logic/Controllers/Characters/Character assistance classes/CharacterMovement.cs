using System;
using UnityEngine;

[Serializable]
public class CharacterMovement
{
    [SerializeField] protected float movementSpeed = 5f;
    public float MovementSpeed { get { return movementSpeed; } }

    [SerializeField] protected float jumpForce = 50f;
    public float JumpForce { get { return jumpForce; } }

    [SerializeField] protected float rotationSpeed = 60f;
    public float RotationSpeed { get { return rotationSpeed; } }
}
