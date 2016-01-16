using UnityEngine;

public class LocomotionState : BaseState
{
    public override void StateFixedUpdate() {}
    public override void StateLateUpdate() {}
    private float xAngle = 0f;
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

            xAngle -= inputValues.MouseAxes.y*5;
            xAngle = Mathf.Clamp(xAngle, -90, 90);

            inputValues.Owner.TestGameObject.transform.rotation = Quaternion.Euler(
                xAngle,
                inputValues.Owner.TestGameObject.transform.rotation.eulerAngles.y,
                inputValues.Owner.TestGameObject.transform.rotation.eulerAngles.z);

            Debug.DrawRay(
                inputValues.Owner.TestGameObject.transform.position,
                inputValues.Owner.TestGameObject.transform.forward,
                Color.yellow);

            if (inputValues.MeleeAttack == ButtonState.Down)
            {
                inputValues.Owner.MeleeAttack();
            }

            inputValues.Owner.AnimationUpdate();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StateManager.Instance.EnterNewState(new EquipmentState(inputValues));
            }
        }
    }
}