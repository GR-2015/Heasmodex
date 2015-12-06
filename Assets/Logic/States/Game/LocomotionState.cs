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

            Vector3 newMovementVector = Vector3.zero;

            newMovementVector.x = inputValues.MovementVector.x;

            if (inputValues.JumpButton == ButtonState.Down)
            {
                inputValues.Owner.Jump();
            }

            inputValues.Owner.Move(newMovementVector, inputValues.Owner.transform.forward);
            inputValues.Owner.Rotate(inputValues.MovementVector);

            if (inputValues.MeleeAttack == ButtonState.Down)
            {
                inputValues.Owner.MeleeAttack();
            }

            if (Input.GetKeyDown(KeyCode.Escape) == true)
            {
                StateManager.Instance.EnterNewState(new EquipmentState(inputValues));
            }
        }
    }
}