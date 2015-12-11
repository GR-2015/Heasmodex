using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EquipmentView : BaseView
{
    public UnityEvent<string> TestUnityEvent;

    [SerializeField]
    private GameObject EquipmentButton;

    [SerializeField]
    private GameObject ContentBox;

    private List<GameObject> butList = new List<GameObject>();
    private List<Button> buttonsss = new List<Button>();

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
            butList.Add(GameObject.Instantiate(EquipmentButton));
            butList[butList.Count - 1].transform.SetParent(ContentBox.transform);
            buttonsss.Add(butList[butList.Count - 1].GetComponentInChildren<Button>());
            Text text = butList[butList.Count - 1].GetComponentInChildren<Text>();

            InventoryItemButtonHandler handler =
               butList[butList.Count - 1].GetComponentInChildren<InventoryItemButtonHandler>();

            handler.ItemInfo = itemSlot;

            EquipmentView t = GUIController.Instance.GetView<EquipmentView>() as EquipmentView;

            text.text = itemSlot.itemName;
        }
    }

    private void OnDisable()
    {
        foreach (GameObject buttonObject in butList)
        {
            GameObject.Destroy(buttonObject);
        }

        butList.Clear();
    }
}