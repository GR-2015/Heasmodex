using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CollectibleItem : Item
{
    [SerializeField] protected static List<CollectibleItem> colledtedItems;
 
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (LayerHelper.IsLayerMaskLayer(other.gameObject.layer, PLayerMask))
        {
            CharacterManager.Instance.Players[int.Parse(other.name)].CharactereEquipment.AddMoney(sellingPrice);
            Destroy(this.gameObject);
        }
    }
}
