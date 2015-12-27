using UnityEngine;
using System.Collections;

public class GridStreamer : MonoBehaviour
{
    private GridStreamingManager GridStreamingManager { get { return GridStreamingManager.Instance; } }

    private const string GizmoName = "Hdd_Icon.png";

    public Vector3 DebugLineEnd = Vector3.zero;
    public Vector3 Direction = Vector3.zero;
    public int MaxDistance = 100;
    public int Index = 0;

    public bool isLoaded = false;

    public GridStreamesrType Type = GridStreamesrType.Column;
    public GridStreamesrState State = GridStreamesrState.Disabled;

    [SerializeField]
    private bool _isCrossedInPast = false;
    [SerializeField]
    private bool _isCrossedNow = false;

    private void Start()
    {
    }

    private void Update()
    {
        State = UpdateStreamerState();

        switch (State)
        {
            case GridStreamesrState.Activated:
                DrawDebugLine(Color.yellow);
                GridStreamingManager.AddActiveIndex(Index, Type); 
                break;

            case GridStreamesrState.Active:
                DrawDebugLine(Type == GridStreamesrType.Column ? Color.green : Color.cyan);
                break;

            case GridStreamesrState.Disabled:
                DrawDebugLine(Color.yellow);
                GridStreamingManager.RemoveActiveIndex(Index, Type);
                isLoaded = false;
                break;

            case GridStreamesrState.Inactive:
                DrawDebugLine(Color.red);
                break;
        }

    }

    private GridStreamesrState UpdateStreamerState()
    {
        _isCrossedInPast = _isCrossedNow;
        _isCrossedNow = Physics.Raycast(this.transform.position, Direction, MaxDistance);

        if (_isCrossedNow && !_isCrossedInPast)
        {
            return GridStreamesrState.Activated;
        }

        if (_isCrossedNow && _isCrossedInPast)
        {
            return GridStreamesrState.Active;
        }

        if (!_isCrossedNow && _isCrossedInPast)
        {
            return GridStreamesrState.Disabled;
        }

        if (!_isCrossedNow && !_isCrossedInPast)
        {
            return GridStreamesrState.Inactive;
        }

        return GridStreamesrState.Inactive;
    }

    private void DrawDebugLine(Color color)
    {
        if (GridStreamingManager.Debug)
        {
            Debug.DrawLine(this.transform.position, DebugLineEnd, color);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(this.transform.position, GizmoName, true);
    }
}

public enum GridStreamesrType
{
    Column,
    Row
}

public enum GridStreamesrState
{
    Activated,
    Active,
    Disabled,
    Inactive
}
