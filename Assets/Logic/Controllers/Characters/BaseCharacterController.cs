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

    #region Character statistic

    [SerializeField] protected float HP = 100f;

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

    [SerializeField] protected LayerMask EnemyLayerMask;

    [Header("Atack parameters")] 
    [SerializeField] protected Transform middleHitPoint;

    #endregion

    protected void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        CharacterController = GetComponent<CharacterController>();
    }

    public virtual void Rotate(Vector3 rotationInput)
    {
        if (rotationInput.x < 0)
        {
            NewRotation = LeftRotation;
        }

        if (rotationInput.x > 0)
        {
            NewRotation = RightRotation;
        }

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(NewRotation), transform.rotation, RotationSpeed * Time.deltaTime);
    }

    public virtual void Move(Vector3 movementInput, Vector3 direction)
    {
        Movement.x = MovementSpeed * movementInput.x;

        Movement.y += Physics.gravity.y * Time.deltaTime;

        CharacterController.Move(Movement*Time.deltaTime);

        if (Animator != null)
        {
            Animator.SetFloat(AnimationHashID.Instance.MovementXaxis, Mathf.Abs(Movement.x));
            Animator.SetBool(AnimationHashID.Instance.IsGrounded, CharacterController.isGrounded);
        }
    }

    public virtual void Jump()
    {
        if (CharacterController.isGrounded)
        {
            Movement.y = JumpForce;
        }
    }

    public virtual void MeleeAttack()
    {
        Animator.SetTrigger(AnimationHashID.Instance.MeleeAttackTriggerName);
        RaycastHit hit;
        Debug.Log(transform.forward);
        Debug.DrawRay(transform.position, transform.forward, Color.blue, 1f);

        if (Physics.Raycast(middleHitPoint.position, middleHitPoint.forward, out hit, 3f))
        {
            hit.collider.SendMessage("GetDamage", 10f);
        }
    }

    public virtual void GetDamage(float damaga)
    {
        Debug.Log(damaga);
    }
}

public enum InputSourceType
{
    KeyboardAndMouse,
    GamePad
}