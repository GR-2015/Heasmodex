using UnityEngine;

public class LocomotionState : BaseState
{
    public override void StateFixedUpdate() {}

    public override void StateLateUpdate() {}

    public override void StateUpdate()
    {
        foreach (var inputValues in InputCollector.Instance.InputValues)
        {
            float x = inputValues.MovementVector.x;
            PlayerController player = inputValues.Owner;

            Vector3 newMovementVector = Vector3.zero;

            newMovementVector.x = inputValues.MovementVector.x;

            if (inputValues.JumpButton == ButtonState.Down)
            {
                player.Jump();
            }

            player.Move(newMovementVector, player.transform.forward);
            player.Rotate(inputValues.MovementVector);

            if (inputValues.MeleeAttack == ButtonState.Down)
            {
                player.Animator.SetTrigger(AnimationHashID.Instance.MeleeAttackTriggerName);
            }
        }
    }
}