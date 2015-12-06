using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class EquipmentView : BaseView
{
    [SerializeField] private GameObject EquipmentButton;
    [SerializeField] private GameObject ContentBox;

    private List<GameObject> butList  = new List<GameObject>(); 

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
            butList[butList.Count-1].transform.SetParent(ContentBox.transform);

            Text text = butList[butList.Count - 1].GetComponentInChildren<Text>();

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
