using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{

    public static EnemiesSpawnManager Instance { get; private set; }

    [SerializeField] private LayerMask _playerMask;
    public LayerMask PlayerMask { get { return _playerMask; } }
    [SerializeField] private LayerMask _enemyMask;

    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    [SerializeField] private List<GameObject> _normalEnemyGameObjects = new List<GameObject>();
    [SerializeField] private int _maxNormalEnemyCount = 20;
    [SerializeField] private Stack<GameObject> _normalEnemy = new Stack<GameObject>();

    [SerializeField] private AnimationCurve _chanceCurve;
    public float Chance { get { return _chanceCurve.Evaluate(currentDistance); } }
    [SerializeField] private float maxDistance = 100f;
    [SerializeField] private float currentDistance = 0f;
    [SerializeField] private Vector3 playerOldPosition;
    [SerializeField] private Vector3 playerCurrentPosition;
    [SerializeField] private float formPlayerSpawnDistance = 5f;

    [SerializeField] private bool spawnBloack = false;

    private void Awake()
    {
        Instance = this;
        PrepareEnemies();
    }

    private void Update()
    {
        playerOldPosition = playerCurrentPosition;
        playerCurrentPosition = CharacterManager.Instance.Players[0].transform.position;
        playerCurrentPosition.y = (float)System.Math.Round((double)playerCurrentPosition.y, 2);

        float distance = Vector3.Distance(playerCurrentPosition, playerOldPosition);
        currentDistance += distance;

        if (currentDistance >= maxDistance)
        {
            currentDistance = 0f;
        }
    }

    private void PrepareEnemies()
    {
        int index = 0;
        for (int i = 0; i < _maxNormalEnemyCount; i++)
        {
            GameObject gameObject = Instantiate(_normalEnemyGameObjects[index]);
            gameObject.SetActive(false);

            ++index;
            if (index > _normalEnemyGameObjects.Count - 1)
            {
                index = 0;
            }
        }
    }


    public void Spawn(Vector3 position)
    {
        if (spawnBloack)
        {
            return;
        }

        float draw = Random.Range(0f, 100f);

        if (draw <= Chance)
        {
            if (_normalEnemy.Count == 0) return;

            GameObject enemGameObject = _normalEnemy.Pop();

            position.y += 1f;
            position.x += Random.Range(5f, 10f) * (Random.Range(-1, 1) >= 0 ? 1 : -1); ;

            enemGameObject.transform.position = position;
            enemGameObject.SetActive(true);

            if (!Physics.CheckSphere(position, 1f, _enemyMask) &&
                !Physics.CheckSphere(position, 1f, _playerMask))
            {
                enemGameObject.SetActive(false);
            }
        }
    }

    public void Hide(GameObject enemy, EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Normal:
                _normalEnemy.Push(enemy);
                break;
        }
    }

}