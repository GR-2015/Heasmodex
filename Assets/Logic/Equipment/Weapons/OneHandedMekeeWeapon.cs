using UnityEngine;

public class OneHandedMekeeWeapon : MeleeWeapon
{
    [Header("Wield settings", order = 0)]
    [SerializeField]
    protected Vector3 offWieldPositionOffest = Vector3.zero;

    public Vector3 OffWieldPositionOffest { get { return offWieldPositionOffest; } }
}