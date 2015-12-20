using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    protected string inGameName = "Item";

    [SerializeField]
    protected float purchasePrice = 0f;

    [SerializeField]
    protected float sellingPrice = 0f;

    public float PurchasePrice { get { return purchasePrice; } }
    public float SellingPrice { get { return sellingPrice; } }
}