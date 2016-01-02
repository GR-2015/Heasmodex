using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    private Camera _controlledCamera;

    [SerializeField] private Vector3 _cameraOffset = Vector3.zero;
    [SerializeField] private float _cameraSnapingSpeed = 600f;
    [SerializeField] private float _toEdgeDistance = 10f;

    [SerializeField] private Transform _playerTransformToFallow;

    [SerializeField] private bool _debug;
    private GameObject testGameObject = null;
    [SerializeField] private PrimitiveType primitiveType = PrimitiveType.Sphere;
    
    public Vector3 CursorWordPosition { get; private set; }

    private void Awake()
    {
        _controlledCamera = GetComponent<Camera>();

        if (_debug)
        {
            testGameObject = GameObject.CreatePrimitive(primitiveType);
            testGameObject.name = "Cursor Test Object!";
        }
    }

    private void Update()
    {
        CursorWordPosition = CalculateCursorWorldPosition();

        if (testGameObject != null)
        {
            testGameObject.transform.position = CursorWordPosition;
        }
    }

    private void LateUpdate()
    {
        float edge = 0f;
        _playerTransformToFallow = CharacterManager.Instance.Players[0].transform;

        if (_playerTransformToFallow.position.x < ((float)GridStreamingManager.Instance.Widtch / 2))
        {
            Vector3 edgePosition = new Vector3(
                0, 
                _playerTransformToFallow.position.y, 
                0);
            edge = Vector3.Distance(edgePosition, _playerTransformToFallow.position);
        }
        else
        {
            Vector3 edgePosition = new Vector3(
                GridStreamingManager.Instance.Widtch, 
                _playerTransformToFallow.position.y, 
                0);
            edge = Vector3.Distance(edgePosition, _playerTransformToFallow.position);
        }

        if (_toEdgeDistance < edge)
        {
            Vector3 newPosition = _playerTransformToFallow.position + _cameraOffset;
            transform.position = Vector3.Lerp(
                transform.position,
                newPosition,
                _cameraSnapingSpeed*Time.deltaTime);
        }
    }

    private Vector3 CalculateCursorWorldPosition()
    {
        Vector3 cursorWorldPosition = Input.mousePosition;
        cursorWorldPosition.z = _cameraOffset.z * -1;
        cursorWorldPosition = _controlledCamera.ScreenToWorldPoint(cursorWorldPosition);

        return cursorWorldPosition;
    }
}