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
    public InputSourceType[] InputSource { get { return inputSource; } }

    #endregion

    #region Control values

    [HideInInspector]
    public Vector3 Movement = Vector3.zero;
    [HideInInspector]
    public Vector3 NewRotation = Vector3.zero;

    #endregion

    #region Character settings

    [Header("Movement parameters")]
    [SerializeField]
    protected float movementSpeed = 5f;
    public float MovementSpeed { get { return movementSpeed; } }

    [SerializeField]
    protected float jumpForce = 50f;
    public float JumpForce { get { return jumpForce; } }

    [SerializeField]
    protected float rotationSpeed = 60f;
    public float RotationSpeed { get { return rotationSpeed; } }

    #endregion

    protected void Awake()
    {
        this.Animator = this.GetComponent<Animator>();
        this.Rigidbody = this.GetComponent<Rigidbody>();

        this.CharacterController = this.GetComponent<CharacterController>();
    }

}

public enum InputSourceType
{
    KeyboardAndMouse,
    GamePad
}