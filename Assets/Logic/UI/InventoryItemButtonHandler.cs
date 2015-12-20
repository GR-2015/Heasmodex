using UnityEngine;

public class InventoryItemButtonHandler : BaseButtonHandler
{
    //  TMP for tests
    private ItemSlot _itemInfo;

    public ItemSlot ItemInfo
    {
        get { return _itemInfo; }
        set { _itemInfo = value; }
    }

    public override void OnClick()
    {
        //  TMP for tests
        Debug.Log(_itemInfo.itemName + " " + _itemInfo.itemCount.ToString());
    }
}