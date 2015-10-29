using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _controlledCamera;
    [SerializeField]
    private Vector3 _cameraOffset= Vector3.zero;
    [SerializeField]
    private float _cameraSnapingSpeed = 600f;

    public static CameraController Instance
    {
        get;
        private set;
    }

    private void Awake()
    {
        _controlledCamera = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 newPosition = CharacterManager.Instance.Players[0].transform.position + _cameraOffset;
        transform.position = Vector3.Lerp(transform.position, newPosition, _cameraSnapingSpeed * Time.deltaTime);
    }
}