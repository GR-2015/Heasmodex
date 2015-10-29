using UnityEngine;

public class PlayerController : BaseCharacterController
{


    protected void Awake()
    {
        base.Awake();
        NewRotation = transform.rotation.eulerAngles;
    }

    private void Start()
    {
        CharacterManager.Instance.RegisterPlayer(this);
    }
}
