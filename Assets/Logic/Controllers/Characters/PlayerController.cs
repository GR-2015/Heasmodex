using UnityEngine;

public class PlayerController : BaseCharacterController
{
    private Vector3 _rotationToLerp;

    [SerializeField] private float _rotationSpeed = 50f;

    protected void Awake()
    {
        base.Awake();
        _rotationToLerp = transform.rotation.eulerAngles;
    }

    private void Start()
    {
        CharacterManager.Instance.RegisterPlayer(this);
    }

    public override void Move(Vector3 movementInput, Vector3 direction)
    {
        var movement = transform.TransformDirection(0f, 0f, movementInput.x);
        movement.x *= movementInput.x;
        movement *= (MovementSpeed);

        movement *= Time.deltaTime;

        if (movementInput.y > 0)
        {
            movement.y = JumpForce;
        }
        movement.y += Physics.gravity.y * 2 * Time.deltaTime;

        CharacterController.Move(movement);
    }

    public override void Rotate(Vector3 rotationInput)
    {
        if (rotationInput.x < 0)
        {
            _rotationToLerp = LeftRotation;
        }

        if (rotationInput.x > 0)
        {
            _rotationToLerp = RightRotation;
        }

        transform.rotation = Quaternion.Lerp(Quaternion.Euler(_rotationToLerp), transform.rotation, _rotationSpeed * Time.deltaTime);
    }
}
