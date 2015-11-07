using UnityEngine;

public class TestBehavior : BaseBehavior
{
    public override bool EntryConditions()
    {
        foreach (var item in CharacterManager.Instance.Players)
        {
            float distance = Mathf.Abs(Vector3.Distance(this._controlledEnemy.transform.position, item.transform.position));
            if (distance  < 5)
                return true;
        }

        return false;
    }

    public override void Behavior()
    {
        Debug.Log(this.GetType().ToString());
    }

    public override void OverloadConditions()
    {
        foreach (var item in CharacterManager.Instance.Players)
        {
            float distance = Mathf.Abs(Vector3.Distance(this._controlledEnemy.transform.position, item.transform.position));
            if (distance > 5f)
                overloadPermission = true;
            else
                overloadPermission = false;
        }

    }

}