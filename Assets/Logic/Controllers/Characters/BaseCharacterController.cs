using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    #region Consts

    /// <summary>
    /// For use in 2.5 D games.
    /// </summary>
    public static readonly Vector3 RightRotation = new Vector3(0f, 90f, 0f);
    public static readonly Vector3 LeftRotation = new Vector3(0f, -90f, 0f);
    public static readonly string GetDamageFunctionName = "GetDamage";

    #endregion Consts

    #region Components

    public Animator Animator { get; protected set; }
    public Rigidbody Rigidbody { get; protected set; }
    public CharacterController CharacterController { get; protected set; }

    #endregion Components

    #region Input source

    [SerializeField] protected InputSourceType[] inputSource;
    public InputSourceType[] InputSource { get { return inputSource; } }

    #endregion Input source

    #region Control values

    [HideInInspector] public Vector3 Movement = Vector3.zero;
    [HideInInspector] public Vector3 NewRotation = Vector3.zero;
    [HideInInspector] public Vector3 CoursorPosition = Vector3.zero;

    private bool _oldIsGrounded;
    private bool _currentIsGrounded;

    #endregion Control values

    #region Character statistic

    [SerializeField] protected CharacterStatistics characterStatistics = new CharacterStatistics();
    public CharacterStatistics CharacterStatistics { get { return characterStatistics; } }

    #endregion Character statistic

    #region Character equipment

    [SerializeField] protected CharactereEquipment charactereEquipment = new CharactereEquipment();
    public CharactereEquipment CharactereEquipment { get { return charactereEquipment; } }

    #endregion Character equipment

    #region Character settings

    [Header("Movement parameters")]
    [SerializeField] protected CharacterMovement characterMovement = new CharacterMovement();
    public CharacterMovement CharacterMovement { get { return characterMovement; } }

    [SerializeField] protected LayerMask EnemyLayerMask;

    [Header("Atack parameters")]
    [SerializeField] protected Transform middleHitPoint;
    [SerializeField] public GameObject TestGameObject;

    [Header("Wield settings")]
    [SerializeField] protected Transform leftHendGrip;
    [SerializeField] protected Vector3 leftHandWieldRotation = Vector3.zero;
    [SerializeField] protected Transform rightHendGrip;
    [SerializeField] protected Vector3 rightHandWieldRotation = Vector3.zero;

    #endregion Character settings

    protected void Awake()
    {
        // Pobieranie komponentów. 
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();
        CharacterController = GetComponent<CharacterController>();

        //  Inicjalizacja ekwipunku
        if (charactereEquipment.ActiveProjectile != null)
        {
            BaseProjectile testProjectile = charactereEquipment.ActiveProjectile.GetComponent<BaseProjectile>();
            if (testProjectile == null)
            {
                charactereEquipment.ActiveProjectile = null;
            }
            else
            {
                charactereEquipment.GenerateProjectiles<BaseProjectile>(EnemyLayerMask, gameObject);
            }
        }
    }

    protected virtual void OnAnimatorIK(int layerIndex)
    {
        if (Animator == null)
        {
            return;
        }

        Animator.SetLookAtWeight(1f, 0.5f);
        Animator.SetLookAtPosition(CoursorPosition);
    }

    //TMP
    public virtual void EquipWeapons(Transform hend, Hand type)
    {
        switch (type)
        {
            case Hand.Main:
                charactereEquipment.mainHandWeapon = hend.GetComponentInChildren<Weapon>();
                charactereEquipment.mainHandWeapon.transform.localPosition = charactereEquipment.mainHandWeapon.MainWieldPositionOffest;
                charactereEquipment.mainHandWeapon.transform.localRotation = Quaternion.Euler(rightHandWieldRotation);
                break;

            case Hand.Off:
                charactereEquipment.offHandWeapon = hend.GetComponentInChildren<Weapon>();
                charactereEquipment.offHandWeapon.transform.localPosition = (charactereEquipment.mainHandWeapon as OneHandedMekeeWeapon).OffWieldPositionOffest;
                charactereEquipment.offHandWeapon.transform.localRotation = Quaternion.Euler(leftHandWieldRotation);
                break;
        }
    }

    public virtual void Rotate(Vector3 rotationInput)
    {
        CoursorPosition = rotationInput;

        if (rotationInput.x < 0f)
        {
            NewRotation = LeftRotation;
        }

        if (rotationInput.x > 0f)
        {
            NewRotation = RightRotation;
        }

        transform.rotation = Quaternion.Lerp(
            Quaternion.Euler(NewRotation), 
            transform.rotation, 
            characterMovement.RotationSpeed * Time.deltaTime);
    }

    public virtual void Move(Vector3 movementInput, Vector3 direction)
    {
        _oldIsGrounded = _currentIsGrounded;
        _currentIsGrounded = CharacterController.isGrounded;

        Movement.x = characterMovement.MovementSpeed * movementInput.x;

        if (!_currentIsGrounded)
        {
            Movement.y += Physics.gravity.y * Time.deltaTime;
        }

        if (!_currentIsGrounded && _oldIsGrounded && Movement.y < 0)
        {
            Movement.y = 0f;
        }

        CharacterController.Move(Movement * Time.deltaTime);
    }

    public void AnimationUpdate()
    {
        if (Animator == null)
        {
            return;
        }

        Animator.SetFloat(AnimationHashID.Instance.MovementXaxis, Mathf.Abs(Movement.x));
        Animator.SetBool(AnimationHashID.Instance.IsGrounded, CharacterController.isGrounded); 
    }

    public virtual void Jump()
    {
        if (CharacterController.isGrounded)
        {
            Movement.y = characterMovement.JumpForce;
        }
    }

    public virtual void MeleeAttack()
    {
        Animator.SetTrigger(AnimationHashID.Instance.MeleeAttackTriggerName);

        RaycastHit hit;

        if (Physics.Raycast(middleHitPoint.position, middleHitPoint.forward, out hit, 3f, EnemyLayerMask))
        {
            float damage = characterStatistics.Damage;

            if (charactereEquipment.mainHandWeapon != null)
            {
                damage += charactereEquipment.mainHandWeapon.Damage;
            }

            if (charactereEquipment.offHandWeapon != null)
            {
                damage += charactereEquipment.offHandWeapon.Damage;
            }

            hit.collider.SendMessage(GetDamageFunctionName, damage);
        }

        foreach (BaseProjectile projectile in charactereEquipment.characterProjectiles)
        {
            if (projectile.gameObject.active == false)
            {
                projectile.LaunchProjectile(this.TestGameObject.transform);
                projectile.gameObject.SetActive(true);
                projectile.EnemyLayerMask = EnemyLayerMask;
                break;
            }
        }
    }

    public virtual void GetDamage(float damage)
    {
        characterStatistics.HP -= damage;
    }
}

public enum InputSourceType
{
    KeyboardAndMouse,
    GamePad
}

public enum Hand
{
    Main,
    Off
}