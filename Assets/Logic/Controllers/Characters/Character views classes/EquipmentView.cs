using UnityEngine;
using System.Collections;

public class EquipmentView : BaseView
{
    [SerializeField] protected GameObject EquipmentButton;
   // [SerializeField] protected 

    protected override void Start()
    {
        GUIController.Instance.RegisterWindow(this, gameObject, GUIWindowType.Equipment);
        gameObject.SetActive(false);
    }

    public override void LoadContent(MonoBehaviour Object)
    {
        PlayerController player = Object as PlayerController;

        foreach (ItemSlot itemSlot in player.CharactereEquipment.equipment)
        {
            
        }
    }
}
