using UnityEngine;

public class AnimationHashID : MonoBehaviour
{
    public static AnimationHashID Instance { get; private set; }



    #region Float Parameters Hash IDs

    [SerializeField]
    private string _movementXaxisFloatname = "MovementXaxis";

    [SerializeField]
    private string _movementYaxisFloatname = "MovementYaxis";

    #endregion Float Parameters Hash IDs

    #region Bool Parameters Hash IDs

    [SerializeField]
    private string _isGroundedBoolName = "IsGrounded";

    #endregion Bool Parameters Hash IDs

    #region Triggers Hash IDs

    [SerializeField]
    private string _maleAttackTriggerName = "MeleeAttack";

    #endregion Triggers Hash IDs

    //  Flots
    public int MovementXaxis { get; private set; }

    public int MovementYaxis { get; private set; }

    //  Bools
    public int IsGrounded { get; private set; }

    //  Triggers
    public int MeleeAttackTriggerName { get; private set; }

    private void Awake()
    {
        Instance = this;

        GetFloatsHashId();
        GetBoolsHashId();
        GetTriggersHashId();
    }

    private void GetFloatsHashId()
    {
        MovementXaxis = Animator.StringToHash(_movementXaxisFloatname);
        MovementYaxis = Animator.StringToHash(_movementYaxisFloatname);
    }

    private void GetBoolsHashId()
    {
        IsGrounded = Animator.StringToHash(_isGroundedBoolName);
    }

    private void GetTriggersHashId()
    {
        MeleeAttackTriggerName = Animator.StringToHash(_maleAttackTriggerName);
    }
}