using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerController : BaseCharacterController
{


    protected void Awake()
    {
        base.Awake();
        NewRotation = transform.rotation.eulerAngles;
        //Application.LoadLevelAdditiveAsync("Test");
    }

    private void Start()
    {
        CharacterManager.Instance.RegisterPlayer(this);
    }
}
