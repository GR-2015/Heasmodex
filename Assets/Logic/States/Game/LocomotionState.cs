using UnityEngine;

public class LocomotionState : BaseState
{
    public override void StateFixedUpdate() {}

    public override void StateLateUpdate() {}

    public override void StateUpdate()
    {
        foreach (var inputValues in InputCollector.Instance.InputValues)
        {
            if(!inputValues.Owner.gameObject.active)
                continue;
            
            float x = inputValues.MovementAxes.x;

            Vector3 newMovementVector = Vector3.zero;

            newMovementVector.x = inputValues.MovementAxes.x;

            if (inputValues.JumpButton == ButtonState.Down)
            {
                inputValues.Owner.Jump();
            }

            inputValues.Owner.Move(newMovementVector, inputValues.Owner.transform.forward);
            inputValues.Owner.Rotate(newMovementVector);

            inputValues.Owner.CoursorPosition =
                inputValues.Owner.TestGameObject.transform.GetChild(0).transform.position;

            inputValues.Owner.HindsightXAngle -= inputValues.MouseAxes.y*5;
            inputValues.Owner.HindsightXAngle = Mathf.Clamp(inputValues.Owner.HindsightXAngle, -90, 90);

            inputValues.Owner.TestGameObject.transform.rotation = Quaternion.Euler(
                inputValues.Owner.HindsightXAngle,
                inputValues.Owner.TestGameObject.transform.rotation.eulerAngles.y,
                inputValues.Owner.TestGameObject.transform.rotation.eulerAngles.z);

            if (inputValues.Attack == ButtonState.Down)
            {
                inputValues.Owner.Attack();
            }

            inputValues.Owner.AnimationUpdate();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StateManager.Instance.EnterNewState(new EquipmentState(inputValues));
            }
        }
    }
}