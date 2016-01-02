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

    private GameObject _streamers = null;
    private GameObject _level = null;

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
        GameObject loadedObject = null;
        string name = String.Empty;
       
        switch (type)
        {
            case GridStreamesrType.Column:

                foreach (int rowIndex in ActiveRowIndex)
                {
                    name = _mapInfo.RowList[rowIndex].SegmentPregabName[index];

                    if (MapObjects[rowIndex, index] == null && name != string.Empty)
                    {
                        loadedObject = MapObjects[rowIndex, index] = LoadMapSegmeentAsset(name);
                        MapObjects[rowIndex, index].transform.position = new Vector3(index, rowIndex, 0);
                    }
                }
                break;

            case GridStreamesrType.Row:
                foreach (int columIndex in ActiveColumnIndex)
                {
                    name = _mapInfo.RowList[index].SegmentPregabName[columIndex];

                    if (MapObjects[index, columIndex] == null && name != string.Empty)
                    {
                        loadedObject = MapObjects[index, columIndex] = LoadMapSegmeentAsset(name);
                        MapObjects[index, columIndex].transform.position = new Vector3(columIndex, index, 0);
                    }
                }
                break;
        }

        if (loadedObject != null)
        {
            loadedObject.transform.SetParent(this._level.transform);
        }
    }

    private GameObject LoadMapSegmeentAsset(string path)
    {
        ResourceRequest newResourceRequest = Resources.LoadAsync(path, typeof(GameObject));
        GameObject newObject = newResourceRequest.asset as GameObject;
        if (newObject != null)
        {
            return GameObject.Instantiate(newObject);
        }
        else
        {
            return GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
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

        _streamers = new GameObject("Streamers");
        _level = new GameObject("Level");

        _streamers.transform.SetParent(this.transform);
        _level.transform.SetParent(this.transform);

        GridInitialization();
    }

    private void GridInitialization()
    {
        //  Ustalanie rozmiarow siatki streamerów
        _height = (int)_mapInfo.Size.x;
        _widtch = (int)_mapInfo.Size.y;

        //  Tworzenie tablicy pomocniczej mapy.
        //  Przetrzymuje informacje o obiektach mapy, które zostaly wczytane           
        MapObjects = new GameObject[_height, _widtch];
 
        //  Zmienne pomocnicze.
        GameObject newGridStreamer;
        Vector3 newPosition;
        GridStreamer newGridStreamerComponent;

        //  Tworzenie streamerów wierszy.
        for (int i = 0; i < _height; i++)
        {
            //  Tworzenie nowego obiektu streamera.
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            //  Nadawanie pozycji streamerowi
            newPosition = Vector3.zero;
            newPosition.x = -1;
            newPosition.y = i;

            //  Ustawianie paremetrów streamera.
            newGridStreamerComponent.DebugLineEnd = newPosition;
            newGridStreamerComponent.DebugLineEnd.x = _widtch;
            newGridStreamerComponent.MaxDistance = _widtch + 1;
            newGridStreamerComponent.Direction = Vector3.right;

            //  Ustawianie typu i indeksu streamera.
            newGridStreamerComponent.Type = GridStreamesrType.Row;
            newGridStreamerComponent.Index = i;

            //  Instancjonowanie streamera.
            newGridStreamer.gameObject.name = string.Format(GridStreamerName,
                GridStreamesrType.Row, i);
            newGridStreamer.transform.position = newPosition;
            newGridStreamer.transform.SetParent(this._streamers.transform);
        }

        //  Tworzenie streamerów kolum.
        for (int i = 0; i < _widtch; i++)
        {
            //  Tworzenie nowego obiektu streamera.
            newGridStreamer = new GameObject();
            newGridStreamerComponent = newGridStreamer.AddComponent<GridStreamer>();

            //  Nadawanie pozycji streamerowi.
            newPosition = Vector3.zero;
            newPosition.x = i;
            newPosition.y = _height;

            //  Ustawianie paremetrów streamera.
            newGridStreamerComponent.DebugLineEnd = newPosition;
            newGridStreamerComponent.DebugLineEnd.y = 0f;
            newGridStreamerComponent.MaxDistance = _height;
            newGridStreamerComponent.Direction = Vector3.down;

            //  Ustawianie typu i indeksu streamera.
            newGridStreamerComponent.Type = GridStreamesrType.Column;
            newGridStreamerComponent.Index = i;

            //  Instancjonowanie streamera.
            newGridStreamer.gameObject.name = string.Format(GridStreamerName,
                GridStreamesrType.Column, i);
            newGridStreamer.transform.position = newPosition;
            newGridStreamer.transform.SetParent(this._streamers.transform);
        }
    }
}
