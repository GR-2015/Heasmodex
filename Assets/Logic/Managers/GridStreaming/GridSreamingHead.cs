using UnityEngine;
using System.Collections;

public class GridSreamingHead : MonoBehaviour
{
    public static GridSreamingHead Instance { get; private set; }

    [SerializeField] private Transform _playerTransformToFallow;
    [SerializeField] private Vector3 _headOffset = Vector3.zero;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        _playerTransformToFallow = CharacterManager.Instance.Players[0].transform;

        if (_playerTransformToFallow.position.x < (transform.lossyScale.z/2))
        {
            transform.position = new Vector3(
                transform.lossyScale.z / 2, 
                _playerTransformToFallow.position.y,
                _playerTransformToFallow.position.z) + _headOffset;
            return;
        }

        if (_playerTransformToFallow.position.x > GridStreamingManager.Instance.Widtch - (transform.lossyScale.z / 2))
        {
            transform.position = new Vector3(
                GridStreamingManager.Instance.Widtch - (transform.lossyScale.z / 2),
                _playerTransformToFallow.position.y,
                _playerTransformToFallow.position.z) + _headOffset;
            return;
        }

        transform.position = _playerTransformToFallow.position + _headOffset;
    }
}
