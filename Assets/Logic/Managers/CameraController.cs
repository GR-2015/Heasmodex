using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera _controlledCamera;
    [SerializeField]
    private Vector3 _cameraOffset;

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
        newPosition.y = Mathf.RoundToInt(newPosition.y);

        transform.position = Vector3.Lerp(transform.position,
                                            newPosition,
                                            10f * Time.deltaTime);
        //newPosition.z = _cameraOffset;
        transform.position = newPosition;

    }
}