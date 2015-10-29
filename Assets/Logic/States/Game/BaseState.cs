using UnityEngine;

public abstract class BaseState
{
    // Use this for initialization
    public virtual void EnterState() {}

    public virtual void ExitState() {}

    public virtual void Sleep() {}

    public virtual void Return() {}

    public virtual void StateUpdate() {}

    public abstract void StateFixedUpdate();

    public virtual void StateLateUpdate() {}

    public virtual void Move(BaseCharacterController character, Vector3 movementInput, Vector3 direction)
    {
        character.Movement.x = character.MovementSpeed * movementInput.x;

        character.Movement.y += Physics.gravity.y * Time.deltaTime;

        character.CharacterController.Move(character.Movement * Time.deltaTime);
    }

    public virtual void Jump(BaseCharacterController character)
    {
        if (character.CharacterController.isGrounded)
        {
            character.Movement.y = character.JumpForce;
        }
    }

    public virtual void Rotate(BaseCharacterController character, Vector3 rotationInput)
    {
        if (rotationInput.x < 0)
        {
            character.NewRotation = BaseCharacterController.LeftRotation;
        }

        if (rotationInput.x > 0)
        {
            character.NewRotation = BaseCharacterController.RightRotation;
        }

        character.transform.rotation = Quaternion.Lerp(Quaternion.Euler(character.NewRotation), character.transform.rotation, character.RotationSpeed * Time.deltaTime);
    }

}