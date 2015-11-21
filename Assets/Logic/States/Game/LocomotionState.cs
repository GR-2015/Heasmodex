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
                Jump(player);
            }

            Move(player, newMovementVector, player.transform.forward);
            Rotate(player, inputValues.MovementVector);

            x = Mathf.Abs(x);
            if (inputValues.MeleeAttack == ButtonState.Down)
            {
                player.Animator.SetTrigger(AnimationHashID.Instance.MeleeAttackTriggerName);
            }

            player.Animator.SetFloat(AnimationHashID.Instance.MovementXaxis, x);
            player.Animator.SetBool(AnimationHashID.Instance.IsGrounded, player.CharacterController.isGrounded);
        }
    }
}