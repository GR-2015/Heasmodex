using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class EnemyController : BaseCharacterController
{
    [SerializeField] private EnemyType _type;
    public EnemyType Type { get { return _type; } }

    private List<BaseBehavior> _bechaviors;

    public List<BaseBehavior> Bechaviors { get { return _bechaviors; } }
    public BaseBehavior CurrentBechavior { get; set; }

    protected void OnEnable()
    {
        characterStatistics.HP = 100f;
    }

    protected void Awake()
    {
        base.Awake();

        _bechaviors = GetComponents<BaseBehavior>().ToList();

        if (_bechaviors.Count > 0)
        {
            CurrentBechavior = _bechaviors[0];
        }
    }

    protected void Start()
    {
        //CharacterManager.Instance.RegisterEnemy(this);
    }

    protected void Update()
    {
        foreach (var bechavior in _bechaviors)
        {
            if (bechavior.EntryConditions() == true)
            {
                if (CurrentBechavior.Priority < bechavior.Priority)
                {
                    CurrentBechavior = bechavior;
                }
                else if (CurrentBechavior.OverloadPermission == true)
                {
                    CurrentBechavior = bechavior;
                }
            }
        }

        if (CurrentBechavior != null)
        {
            CurrentBechavior.Behavior();
            CurrentBechavior.OverloadConditions();
        }

        if (characterStatistics.HP <= 0 )
        {
            this.gameObject.SetActive(false);
        }
    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnDisable()
    {
        EnemiesSpawnManager.Instance.Hide(this.gameObject,_type);
    }
}

public enum EnemyType
{
    Normal,
    Strong,
    Elite,
    Boss
}