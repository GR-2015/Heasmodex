using UnityEngine;

public class AnimationHashID : MonoBehaviour
{
    public static AnimationHashID Instance { get; private set; }

    #region Animation States Hash IDs
    #endregion

    #region Transitions Hash IDs
    #endregion

    #region Float Parameters Hash IDs

    [SerializeField]
    private string _movementXaxisFloatname = "MovementXaxis";
    [SerializeField]
    private string _movementYaxisFloatname = "MovementYaxis";

    #endregion

    #region Bool Parameters Hash IDs

    [SerializeField]
    private string _isGroundedBoolName = "IsGrounded";

    #endregion

    #region Triggers Hash IDs
    #endregion

    public int MovementXaxis { get; private set; }
    public int MovementYaxis { get; private set; }
    public int IsGrounded { get; private set; }

    private void Awake()
    {
        Instance = this;

        GetParametersHashId();
    }

    private void GetParametersHashId()
    {
        MovementXaxis = Animator.StringToHash(_movementXaxisFloatname);
        MovementYaxis = Animator.StringToHash(_movementYaxisFloatname);

        IsGrounded = Animator.StringToHash(_isGroundedBoolName);
    }
}