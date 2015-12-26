using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{

    public static EnemiesSpawnManager Instance { get; private set; }

    [SerializeField]
    private string _playerTag;

    public string PlayerTag { get { return _playerTag; } }

    [SerializeField]
    private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    [SerializeField]
    private List<GameObject> _normalEnemyGameObjects = new List<GameObject>();
    [SerializeField]
    private int _maxNormalEnemyCount = 20;
    [SerializeField]
    private Stack<GameObject> _normalEnemy = new Stack<GameObject>();

    // TMP
    [SerializeField]
    private float maxChance = 20f;
    [SerializeField]
    private float minChance = 0f;

    private void Awake()
    {
        Instance = this;
        PrepareEnemies();
    }

    private void PrepareEnemies()
    {
        int index = 0;
        for (int i = 0; i < _maxNormalEnemyCount; i++)
        {
            GameObject gameObject = GameObject.Instantiate(_normalEnemyGameObjects[index]) as GameObject;
            gameObject.SetActive(false);

            _normalEnemy.Push(gameObject);

            ++index;
            if (index > _normalEnemyGameObjects.Count - 1)
            {
                index = 0;
            }
        }
    }

    public void RegisterSpawnPoint(SpawnPoint point)
    {
        _spawnPoints.Add(point);
        UpdateSprawnPoints();
    }

    public void UnregisterSpawnPoint(SpawnPoint point)
    {
        _spawnPoints.Remove(point);
        UpdateSprawnPoints();
    }

    public void Spawn(float draw, Vector3 position)
    {
        if (draw >= 10f && draw <= 20f)
        {
            GameObject enemGameObject = _normalEnemy.Pop();
            if (enemGameObject == null) return;

            enemGameObject.SetActive(true);

            position.x -= 1f;

            enemGameObject.transform.position = position;
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

    private void UpdateSprawnPoints()
    {
        foreach (SpawnPoint point in _spawnPoints)
        {
            point.spawnChance = Random.Range(minChance, maxChance);
        }
    }
}