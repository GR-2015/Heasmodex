using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseCharacterController : MonoBehaviour
{
    #region Consts

    /// <summary>
    /// For use in 2.5 D games.
    /// </summary>
    public static readonly Vector3 RightRotation = new Vector3(0f, 90f, 0f);
    public static readonly Vector3 LeftRotation = new Vector3(0f, -90f, 0f);

    #endregion

    #region Components

    public Animator Animator { get; protected set; }

    public Rigidbody Rigidbody { get; protected set; }

    public CharacterController CharacterController { get; protected set; }

    #endregion

    #region Input source

    [SerializeField]
    protected InputSourceType[] inputSource;

    public InputSourceType[] InputSource
    {
        get { return inputSource; }
    }

    #endregion

    #region Character settings

    [SerializeField] protected float MovementSpeed = 5f;
    [SerializeField] protected float JumpForce = 50f;


    #endregion

    protected void Awake()
    {
        this.Animator = this.GetComponent<Animator>();
        this.Rigidbody = this.GetComponent<Rigidbody>();

        this.CharacterController = this.GetComponent<CharacterController>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    #region Control functions

    public virtual void Move(Vector3 movementInput, Vector3 direction) {}
    public virtual void Rotate(Vector3 rotationInput) {}

    #endregion
}

public enum InputSourceType
{
    KeyboardAndMouse
}