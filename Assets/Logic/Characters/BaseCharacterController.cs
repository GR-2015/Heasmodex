using UnityEngine;

public class BaseCharacterController : MonoBehaviour
{
    public Animator Animator { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    protected void Awake()
    {
        this.Animator = this.GetComponent<Animator>();
        this.Rigidbody = this.GetComponent<Rigidbody>();
    }

    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}