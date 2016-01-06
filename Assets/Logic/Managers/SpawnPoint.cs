using System.Collections;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private static EnemiesSpawnManager EnemiesSpawnManager
    {
        get { return EnemiesSpawnManager.Instance; }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (LayerHelper.IsLayerMaskLayer(
            collision.gameObject.layer,
            EnemiesSpawnManager.PlayerMask) == false)
        {
            return;
        }

        EnemiesSpawnManager.Spawn(collision.gameObject.transform.position);
    }

}