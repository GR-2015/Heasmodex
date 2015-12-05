using UnityEngine;
using System.Collections;

public class Armor : Item
{
    [SerializeField] protected float protection = 10f;

    public float Protection { get { return protection; } }
}
