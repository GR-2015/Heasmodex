using UnityEngine;

public class DefaultsBehavior : BaseBehavior
{
    private float _distance;

    public override bool EntryConditions()
    {
        _distance = Mathf.Abs(Vector3.Distance(this._controlledEnemy.transform.position, ClosestPlayer()));
        if (_distance < 10)
        {
            return true;
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

        bool fireCondition = _controlledEnemy.CharactereEquipment.ActiveProjectile.Range >= _distance;
        
        _controlledEnemy.InputValues.Attack = InputCollector.SimulateButtonPress(fireCondition, _controlledEnemy.InputValues.Attack);
    }

    public override void OverloadConditions()
    {
    }
}