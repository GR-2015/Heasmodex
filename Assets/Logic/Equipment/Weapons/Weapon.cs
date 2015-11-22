using UnityEngine;

public class Weapon : Item
{
    [Header("Wield settings", order = 0)]
    [SerializeField]
    protected Vector3 mainWieldPositionOffest = Vector3.zero;

    public Vector3 MainWieldPositionOffest { get { return mainWieldPositionOffest; } }

    [Header("Weapon settings")]
    [SerializeField]
    protected float damage = 10f;

    public float Damage { get { return damage; } }

    [SerializeField]
    protected float attackSpeed = 1f;

    public float AttackSpeed { get { return attackSpeed; } }

    [SerializeField]
    protected float range = 1f;

    public float Range { get { return range; } }
}