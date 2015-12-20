using UnityEngine;
using UnityEngine.UI;

public abstract class BaseButtonHandler : MonoBehaviour
{
    protected Button handledButton;

    protected virtual void Awake()
    {
        handledButton = GetComponent<Button>();

        handledButton.onClick.AddListener(OnClick);
    }

    protected virtual void OnDestroy()
    {
        handledButton.onClick.RemoveListener(OnClick);
    }

    public abstract void OnClick();
}