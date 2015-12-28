using System;
using UnityEngine;
using System.Collections.Generic;

public class GridStreamingManager : MonoBehaviour
{
    public static GridStreamingManager Instance { get; private set; }

    private const string GridStreamerName = "{0} {1}";

    [SerializeField] private int _height = 100;
    [SerializeField] private int _widtch = 100;
    [SerializeField] private bool _debug = true;

    public bool Debug { get { return _debug; } }

    [SerializeField] private List<int> _activeRowIndex = new List<int>();
    [SerializeField] private List<int> _activeColumnIndex = new List<int>();

    public List<int> ActiveRowIndex { get { return _activeRowIndex; } }
    public List<int> ActiveColumnIndex { get { return _activeColumnIndex; } }

    [SerializeField] private GridMapInfo _mapInfo = null;

    private GameObject [,] MapObjects;

    [SerializeField] private LayerMask streaminLayerMask;
    public LayerMask StreaminLayerMask { get { return streaminLayerMask; } }

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
                Stream(index, type);
                break;
        }
    }

    public void RemoveActiveIndex(int index, GridStreamesrType type)
    {
        switch (type)
        {
            case GridStreamesrType.Column:
                _activeColumnIndex.Remove(index);
                Unstream(index, type);
                break;

            case GridStreamesrType.Row:
                _activeRowIndex.Remove(index);
                Unstream(index, type);
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
                    string name = _mapInfo.RowList[rowIndex].segmentPregabName[index];
                    //UnityEngine.Debug.Log(name);

                    if (MapObjects[rowIndex, index] == null && name != string.Empty)
                    {
                        MapObjects[rowIndex, index] = LoadMapSegmeentAsset(name);
                        MapObjects[rowIndex, index].transform.position = new Vector3(index, rowIndex, 0);
                    }
                }
                break;

            case GridStreamesrType.Row:
                foreach (int columIndex in ActiveColumnIndex)
                {
                    string name = _mapInfo.RowList[index].segmentPregabName[columIndex];
                    //UnityEngine.Debug.Log(name);

                    if (MapObjects[index, columIndex] == null && name != string.Empty)
                    {
                        MapObjects[index, columIndex] = LoadMapSegmeentAsset(name);
                        MapObjects[index, columIndex].transform.position = new Vector3(columIndex, index, 0);
                    }
                }
                break;
        }
    }

    private GameObject LoadMapSegmeentAsset(string path)
    {
        ResourceRequest newResourceRequest = Resources.LoadAsync(path, typeof(GameObject));
        GameObject newObject = newResourceRequest.asset as GameObject;
        return GameObject.Instantiate(newObject);
    }

    private void Unstream(int index, GridStreamesrType type)
    {
        switch (type)
        {
            case GridStreamesrType.Column:

                foreach (int rowIndex in ActiveRowIndex)
                {
                    if (MapObjects[rowIndex, index] != null)
                    {
                        GameObject.Destroy(MapObjects[rowIndex, index]);
                        MapObjects[rowIndex, index] = null;
                    }
                }
                break;

            case GridStreamesrType.Row:
                foreach (int columIndex in ActiveColumnIndex)
                {
                    if (MapObjects[index, columIndex] != null)
                    {
                        GameObject.Destroy(MapObjects[index, columIndex]);
                        MapObjects[index, columIndex] = null;
                    }
                }
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
        _height = (int)_mapInfo.size.x;
        _widtch = (int)_mapInfo.size.y;

        MapObjects = new GameObject[_height, _widtch];
 
        GameObject newGridStreamer;
        Vector3 newPosition;
        GridStreamer newGridStreamerComponent;

        for (int i = 0; i < _height; i++)
        {
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            newPosition = Vector3.zero;
            newPosition.x = -1;
            newPosition.y = i;

            newGridStreamerComponent.DebugLineEnd = newPosition;
            newGridStreamerComponent.DebugLineEnd.x = _widtch;
            newGridStreamerComponent.MaxDistance = _widtch + 1;
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
