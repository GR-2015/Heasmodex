using UnityEngine;
using System.Collections;

public abstract class BaseBehavior : MonoBehaviour 
{
    protected EnemyController _controlledEnemy;

    [SerializeField]
    private int priority = 1;

    public int Priority { get { return priority; } } 

    [SerializeField]
    protected bool overloadPermission = false;

    public bool OverloadPermission { get { return overloadPermission; } }

    private void Awake()
    {
        _controlledEnemy = GetComponent<EnemyController>();
    }

    public abstract bool EntryConditions();

    public abstract void Behavior();

    public abstract void OverloadConditions();

    protected Vector3 ClosestPlayer()
    {
        Vector3 closestPlayerPosition = CharacterManager.Instance.Players[0].transform.position;
        float distanceA = 0f;
        float distanceB = 0f;

        distanceA = Vector3.Distance(this.transform.position, closestPlayerPosition);
        
        for (int i = 1; i < CharacterManager.Instance.Players.Count; i++)
        {
            distanceB = Vector3.Distance(this.transform.position, CharacterManager.Instance.Players[i].transform.position);
            
            if (distanceA > distanceB)
            {
                distanceA = distanceB;
                closestPlayerPosition = CharacterManager.Instance.Players[i].transform.position;
            }
        }

        return closestPlayerPosition;
    }
}
