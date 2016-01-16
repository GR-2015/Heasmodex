using UnityEngine;

public class DefaultsBehavior : BaseBehavior
{
    public override bool EntryConditions()
    {
        foreach (var item in CharacterManager.Instance.Players)
        {
            float distance = Mathf.Abs(Vector3.Distance(this._controlledEnemy.transform.position, item.transform.position));
            if (distance < 10)
            {
                return true;
            }
        }

        return false;
    }

    public override void Behavior()
    {
        Vector3 playerPosition = this.ClosestPlayer();
        Vector3 movementAxes = Vector3.zero;

        if (playerPosition.x < this.transform.position.x)
        {
            movementAxes.x = -1;
        }
        else
        {
            movementAxes.x = 1;
        }
        _controlledEnemy.InputValues.MovementAxes = movementAxes;
        //_controlledEnemy.Move(inputValue, Vector3.zero);
    }

    public override void OverloadConditions()
    {
    }
}