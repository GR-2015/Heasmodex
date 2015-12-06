using UnityEngine;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;

    [SerializeField]
    private GameObject equipmentWindow;

    private EquipmentView equipmentView;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenInventory(BaseCharacterController owner)
    {
        if (equipmentWindow.active == false)
        {
            equipmentWindow.SetActive(true);
            equipmentView.LoadContent(owner);
        }
        else
        {
            equipmentWindow.SetActive(false);
        }
    }

    public void RegisterWindow(BaseView view, GameObject windowGameObject, GUIWindowType windowType)
    {
        switch (windowType)
        {
            case GUIWindowType.Equipment:
                equipmentView = view as EquipmentView;
                equipmentWindow = windowGameObject;
                break;
        }
    }
}

public enum GUIWindowType
{
    Equipment
}