using UnityEngine;

public class Armor : Item
{
    [SerializeField]
    protected float protection = 10f;

    public float Protection { get { return protection; } }
}