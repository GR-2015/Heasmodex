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
    private string _movementXaxis = "MovementXaxis";
    [SerializeField]
    private string _movementYaxis = "MovementYaxis";

    #endregion
    #region Bool Parameters Hash IDs
    #endregion
    #region Triggers Hash IDs
    #endregion

    public int MovementXaxis { get; private set; }
    public int MovementYaxis { get; private set; }

    private void Awake()
    {
        Instance = this;

        GetParametersHashID();
    }

    private void GetParametersHashID()
    {
        MovementXaxis = Animator.StringToHash(_movementXaxis);
        MovementYaxis = Animator.StringToHash(_movementYaxis);

    }
}