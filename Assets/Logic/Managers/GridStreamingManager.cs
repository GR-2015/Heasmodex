using UnityEngine;
using System.Collections.Generic;

public class GridStreamingManager : MonoBehaviour
{
    public static GridStreamingManager Instance { get; private set; }
    private const string GridStreamerName = "{0} {1}";

    [SerializeField] private int _height = 100;
    [SerializeField] private int _widtch = 100;
    [SerializeField] private bool _debug = true;

    [SerializeField] private List<int> _activeRowIndex = new List<int>();
    [SerializeField] private List<int> _activeColumnIndex = new List<int>();

    [SerializeField] private GridMapInfo mapInfo;

    public List<int> ActiveRowIndex { get { return _activeRowIndex; } }

    public List<int> ActiveColumnIndex { get { return _activeColumnIndex; } }

    public bool Debug { get { return _debug; } }

    public void AddActiveIndex(int index, GridStreamesrType type)
    {
        switch (type)
        {
            case GridStreamesrType.Column:
                _activeColumnIndex.Add(index);
                Stream(index, type);
                break;

            case GridStreamesrType.Row:
                _activeRowIndex.Add(index);
                break;
        }
    }

    public void RemoveActiveIndex(int index, GridStreamesrType type)
    {
        switch (type)
        {
            case GridStreamesrType.Column:
                _activeColumnIndex.Remove(index);
                break;

            case GridStreamesrType.Row:
                _activeRowIndex.Remove(index);
                break;
        }
    }

    private void Stream(int index, GridStreamesrType type)
    {
        switch (type)
        {
            case GridStreamesrType.Column:

                foreach (int rowIndex in ActiveRowIndex)
                {
                    UnityEngine.Debug.Log(mapInfo.RowList[rowIndex].segmentPregabName[index]);
                }
                break;

            case GridStreamesrType.Row:
                _activeRowIndex.Remove(index);
                break;
        }
    }


    private void Awake()
    {
        Instance = this;
        GridInitialization();
    }

    private void GridInitialization()
    {
        _height = (int)mapInfo.size.x;
        _widtch = (int)mapInfo.size.y;
 
        GameObject newGridStreamer;
        Vector3 newPosition;
        GridStreamer newGridStreamerComponent;

        for (int i = 0; i < _height; i++)
        {
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            newPosition = Vector3.zero;
            newPosition.y = i;

            newGridStreamerComponent.DebugLineEnd = newPosition;
            newGridStreamerComponent.DebugLineEnd.x = _widtch;
            newGridStreamerComponent.MaxDistance = _widtch;
            newGridStreamerComponent.Direction = Vector3.right;
            newGridStreamerComponent.Type = GridStreamesrType.Row;
            newGridStreamerComponent.Index = i;

            newGridStreamer.gameObject.name = string.Format(GridStreamerName,
                GridStreamesrType.Row, i);
            newGridStreamer.transform.position = newPosition;
            newGridStreamer.transform.SetParent(this.transform);
        }

        for (int i = 0; i < _widtch; i++)
        {
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            newPosition = Vector3.zero;
            newPosition.x = i;
            newPosition.y = _height;

            newGridStreamerComponent.DebugLineEnd = newPosition;
            newGridStreamerComponent.DebugLineEnd.y = 0f;
            newGridStreamerComponent.MaxDistance = _height;
            newGridStreamerComponent.Direction = Vector3.down;
            newGridStreamerComponent.Type = GridStreamesrType.Column;
            newGridStreamerComponent.Index = i;

            newGridStreamer.gameObject.name = string.Format(GridStreamerName,
                GridStreamesrType.Column, i);
            newGridStreamer.transform.position = newPosition;
            newGridStreamer.transform.SetParent(this.transform);
        }
    }
}
