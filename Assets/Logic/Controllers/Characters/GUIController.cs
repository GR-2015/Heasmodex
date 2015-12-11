using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GUIController : MonoBehaviour
{
    public static GUIController Instance;

    private List<BaseView> cachedViews = new List<BaseView>();

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
        BaseView cahedView = null;

        switch (windowType)
        {
            case GUIWindowType.Equipment:
                cahedView = view;
                equipmentView = cahedView as EquipmentView;
                equipmentWindow = windowGameObject;
                break;
        }

        if (cahedView != null)
        {
            cachedViews.Add(cahedView);
        }
    }

    public BaseView GetView<T>() where T : BaseView
    {
        return cachedViews.OfType<T>().FirstOrDefault();
    }
}

public enum GUIWindowType
{
    Equipment
}