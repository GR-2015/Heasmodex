using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : Item
{
    protected Vector3 OldPosition = Vector3.zero;
    protected float DistanceTraveled = 0f;
    protected Vector3 Direction = Vector3.zero;

    protected Rigidbody Rigidbody;

    [SerializeField] public float Range = 5f;
    [SerializeField] public float Damage = 10f;
    [SerializeField] public float Velocity = 100f;
    [SerializeField] public LayerMask EnemyLayerMask;
    [SerializeField] public GameObject Owner;

    protected void OnEnable()
    {
        OldPosition = this.transform.position;
    }

    protected void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        if (DistanceTraveled >= Range)
        {
            DistanceTraveled = 0f;
            this.gameObject.SetActive(false);
        }

        DistanceTraveled += Vector3.Distance(
            transform.position,
            OldPosition);

        OldPosition = transform.position;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Owner)
        {
            return;
        }

        gameObject.SetActive(false);

        if (collision.gameObject.layer == EnemyLayerMask)
        {
            collision.gameObject.SendMessage(BaseCharacterController.GetDamageFunctionName, Damage);
        }
    }

    public void LaunchProjectile(Transform firingCharacter)
    {
        this.transform.position = firingCharacter.position;
        this.gameObject.transform.rotation = firingCharacter.rotation;

        Rigidbody.velocity = transform.forward *Velocity;

    }
}