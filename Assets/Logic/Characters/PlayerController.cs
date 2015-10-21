using UnityEngine;

public class PlayerController : BaseCharacterController
{
    [SerializeField] private InputSourceType[] _inputSource;

    public InputSourceType[] InputSource
    {
        get { return _inputSource; }
    }

    protected void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        CharacterManager.Instance.RegisterPlayer(this);
    }

    private void Update()
    {
    }
}

public enum InputSourceType
{
    KeyboardAndMouse
}