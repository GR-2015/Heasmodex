using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BaseProjectile : MonoBehaviour
{
    protected Vector3 oldPosition = Vector3.zero;
    protected float distanceTraveled = 0f;
    protected Vector3 direction = Vector3.zero;

    protected Rigidbody _Rigidbody = null;

    [SerializeField]
    protected float _range = 10f;

    [SerializeField]
    protected float _damage = 10f;

    [SerializeField]
    protected float _velocity = 100f;

    [SerializeField]
    protected LayerMask _enemyLayerMask;

    public LayerMask EnemyLayerMask
    {
        get { return _enemyLayerMask; }
        set { _enemyLayerMask = value; }
    }

    protected void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    protected void OnEnable()
    {
        oldPosition = this.transform.position;
    }

    protected void Update()
    {
        if (distanceTraveled >= _range)
        {
            distanceTraveled = 0f;
            this.gameObject.SetActive(false);
        }
    }

    protected void FixedUpdate()
    {
        float distance = Vector3.Distance(this.transform.position,
           oldPosition);

        distanceTraveled += distance;

        _Rigidbody.AddForce(direction * _velocity, ForceMode.VelocityChange);

        oldPosition = this.transform.position;
    }

    protected void OnCollisionEnter(Collision collision)
    {
        //this.enabled = false;

        //collision.gameObject.SendMessage(BaseCharacterController.getDamageFunctionName, _damage);
    }

    public void LaunchProjectile(Transform firingCharacter)
    {
        this.direction = firingCharacter.forward;

        this.transform.position = firingCharacter.position;
    }
}