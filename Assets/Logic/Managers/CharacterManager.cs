using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    [SerializeField] private List<PlayerController> _players = new List<PlayerController>();
    [SerializeField] private List<EnemyController> _enemies = new List<EnemyController>();

    public List<PlayerController> Players { get { return _players; } }
    public List<EnemyController> Enemies { get { return _enemies; } }

    private void Awake()
    {
        Instance = this;
    }

    public int RegisterPlayer(PlayerController instance)
    {
        _players.Add(instance);

        int index = _players.Count - 1;

        var inputValues = new InputValues(instance);
        InputCollector.Instance.AddInputValues(inputValues);

        foreach (var InputSourc in instance.InputSource)
        {
            switch (InputSourc)
            {
                case InputSourceType.KeyboardAndMouse:
                    InputCollector.Instance.AddInputSorces(new KeyboardAndMouse(inputValues));
                    break;
            }
        }

        return index;
    }

    public void UnRegisterPlayer(PlayerController instance)
    {
        _players.Remove(instance);
    }

    public void RegisterEnemy(EnemyController instance)
    {
        _enemies.Add(instance);
    }

    public void UnRegisterEnemy(EnemyController instance)
    {
        _enemies.Remove(instance);
    }

    public Vector3 ClosestPlayer(Vector3 position)
    {
        Vector3 closestPlayerPosition = Players[0].transform.position;
        float distanceA = 0f;
        float distanceB = 0f;

        distanceA = Vector3.Distance(position, closestPlayerPosition);

        for (int i = 1; i < Players.Count; i++)
        {
            distanceB = Vector3.Distance(position, Players[i].transform.position);

            if (distanceA > distanceB)
            {
                distanceA = distanceB;
                closestPlayerPosition = Players[i].transform.position;
            }
        }

        return closestPlayerPosition;
    }

}