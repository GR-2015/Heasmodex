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

            if (x < 0)
            {
                player.transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                player.transform.rotation = Quaternion.Euler(0, 0, 0);
            }

            x = Mathf.Abs(x);

            player.Animator.SetFloat(AnimationHashID.Instance.MovementXaxis, x);
        }
    }
}