using UnityEngine;
using System.Collections;

public class GridSreamingHead : MonoBehaviour
{
    public static GridSreamingHead Instance { get; private set; }
    [SerializeField] private Transform _playerTransformToFallow = null;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        this.transform.position = _playerTransformToFallow.position;
    }

    void Update()
    {
        this.transform.position = _playerTransformToFallow.position;
    }
}
