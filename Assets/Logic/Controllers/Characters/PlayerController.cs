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

    protected override void HandleDeath()
    {
        Animator.SetTrigger("Test");
        StateManager.Instance.EnterNewState(new DeathState());
    }
}