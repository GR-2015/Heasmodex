using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]

public class PlayerController : BaseCharacterController
{
    protected void Awake()
    {
        base.Awake();
        NewRotation = transform.rotation.eulerAngles;
    }

    private void Start()
    {
        int index = CharacterManager.Instance.RegisterPlayer(this);

        this.name = index.ToString();
    }
}