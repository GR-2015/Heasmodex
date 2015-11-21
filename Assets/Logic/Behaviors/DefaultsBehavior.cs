using UnityEngine;

public class DefaultsBehavior : BaseBehavior
{
    public override bool EntryConditions()
    {
        foreach (var item in CharacterManager.Instance.Players)
        {
            float distance = Mathf.Abs(Vector3.Distance(this._controlledEnemy.transform.position, item.transform.position));
            if (distance < 10)
                return true;
        }
        return false;
    }

    public override void Behavior()
    {
        //Debug.Log(this.GetType().ToString());
    }

    public override void OverloadConditions()
    {
    }
}