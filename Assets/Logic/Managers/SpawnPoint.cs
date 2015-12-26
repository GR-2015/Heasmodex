using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private static EnemiesSpawnManager EnemiesSpawnManager
    {
        get { return EnemiesSpawnManager.Instance; }
    }
    [SerializeField] public float spawnChance = 0f;

    private void Start()
    {
        EnemiesSpawnManager.RegisterSpawnPoint(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals(EnemiesSpawnManager.PlayerTag) == false)
        {
            return;
        }

        float draw = Random.Range(0f, 100f);
        EnemiesSpawnManager.Spawn(draw, collision.gameObject.transform.position);
        //if (draw <= spawnChance)
        //{
        //    Debug.Log("Pow! Monster! "+ gameObject.name);
        //}
    }

    private void OnDestroy()
    {
        EnemiesSpawnManager.UnregisterSpawnPoint(this);
    }
}