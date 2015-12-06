using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCharacterController : MonoBehaviour
{
    #region Consts

    /// <summary>
    /// For use in 2.5 D games.
    /// </summary>
    public static readonly Vector3 RightRotation = new Vector3(0f, 90f, 0f);

    public static readonly Vector3 LeftRotation = new Vector3(0f, -90f, 0f);

    public static readonly string getDamageFunctionName = "GetDamage";

    #endregion Consts

    #region Components

    public Animator Animator { get; protected set; }

    public Rigidbody Rigidbody { get; protected set; }

    public CharacterController CharacterController { get; protected set; }

    #endregion Components

    #region Input source

    [SerializeField]
    protected InputSourceType[] inputSource;

    public InputSourceType[] InputSource { get { return inputSource; } }

    #endregion Input source

    #region Control values

    [HideInInspector]
    public Vector3 Movement = Vector3.zero;

    [HideInInspector]
    public Vector3 NewRotation = Vector3.zero;

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
    [SerializeField]
    protected float movementSpeed = 5f;

    public float MovementSpeed { get { return movementSpeed; } }

    [SerializeField]
    protected float jumpForce = 50f;

    public float JumpForce { get { return jumpForce; } }

    [SerializeField]
    protected float rotationSpeed = 60f;

    public float RotationSpeed { get { return rotationSpeed; } }

    [SerializeField]
    protected LayerMask EnemyLayerMask;

    [Header("Atack parameters")]
    [SerializeField]
    protected Transform middleHitPoint;

    [Header("Wield settings")]
    [SerializeField]
    protected Transform leftHendGrip;

    [SerializeField]
    protected Vector3 leftHandWieldRotation = Vector3.zero;

    [SerializeField]
    protected Transform rightHendGrip;

    [SerializeField]
    protected Vector3 rightHandWieldRotation = Vector3.zero;

    #endregion Character settings

    protected void Awake()
    {
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        CharacterController = GetComponent<CharacterController>();
        if (Animator != null)
        {
            leftHendGrip = Animator.GetBoneTransform(HumanBodyBones.LeftHand);
            rightHendGrip = Animator.GetBoneTransform(HumanBodyBones.RightHand);

            if (leftHendGrip != null & rightHendGrip != null)
            {
                EquipWeapons(rightHendGrip, Hand.Main);
                EquipWeapons(leftHendGrip, Hand.Off);
            }
        }

        //TMP 
        charactereEquipment.GenerateProjectiles<BaseProjectile>();
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

        CharacterController.Move(Movement * Time.deltaTime);

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

        if (Physics.Raycast(middleHitPoint.position, middleHitPoint.forward, out hit, 3f, EnemyLayerMask))
        {
            float damage = this.characterStatistics.Damage;

            if (charactereEquipment.mainHandWeapon != null)
            {
                damage += charactereEquipment.mainHandWeapon.Damage;
            }

            if (charactereEquipment.offHandWeapon != null)
            {
                damage += charactereEquipment.offHandWeapon.Damage;
            }

            hit.collider.SendMessage(getDamageFunctionName, damage);
        }

        foreach (BaseProjectile projectile in charactereEquipment.characterProjectiles)
        {
            if (projectile.gameObject.active == false)
            {
                projectile.LaunchProjectile(this.middleHitPoint);
                projectile.gameObject.SetActive(true);
                break;
            }
        }
    }

    public virtual void GetDamage(float damage)
    {
        this.characterStatistics.HP -= damage;
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