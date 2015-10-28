using UnityEngine;

public class LocomotionState : BaseState
{
    public override void StateFixedUpdate()
    {
    }

    public override void StateLateUpdate()
    {
    }

    public override void StateUpdate()
    {
        foreach (var inputValues in InputCollector.Instance.InputValues)
        {
            float x = inputValues.MovementVector.x;
            var player = inputValues.Owner;
            Vector3 newMovementVector = Vector3.zero;

            newMovementVector.x = inputValues.MovementVector.x;

            newMovementVector.y = inputValues.JumpButton == ButtonState.Down ? 1f : 0f;

            newMovementVector.z = inputValues.MovementVector.y;

            player.Move(newMovementVector, player.transform.forward);
            player.Rotate(inputValues.MovementVector);

            x = Mathf.Abs(x);

            player.Animator.SetFloat(AnimationHashID.Instance.MovementXaxis, x);
        }
    }
}