using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class GridStreamingManager : MonoBehaviour
{
    public static GridStreamingManager Instance { get; private set; }

    private const string GridStreamerName = "{0} {1}";

    [SerializeField] private int _height = 100;
    [SerializeField] private int _widtch = 100;
    [SerializeField] private int _layers = 100;

    public int Height { get { return _height; } }
    public int Widtch { get { return _widtch; } }
    public int Layers { get { return _layers; } }

    [SerializeField] private float _streamerRowOffset = 20f;
    [SerializeField] private float _streamerColumnOffset = 15f;

    public float StreamerRowOffset { get { return _streamerRowOffset; } }
    public float StreamerColumnOffset { get { return _streamerColumnOffset; } }

    private float _streamerRowOffsetValue = 15f;
    private float _streamerColumnOffsetValue = 15f;

    [SerializeField] private bool _debug = true;

    public bool Debug { get { return _debug; } }

    [SerializeField] private List<GridStreamer> RowGridStreamers = new List<GridStreamer>();
    [SerializeField] private List<GridStreamer> ColumnGridStreamers = new List<GridStreamer>();

    [SerializeField] private List<int> _activeRowIndex = new List<int>();
    [SerializeField] private List<int> _activeColumnIndex = new List<int>();

    public List<int> ActiveRowIndex { get { return _activeRowIndex; } }
    public List<int> ActiveColumnIndex { get { return _activeColumnIndex; } }

    [SerializeField] private GridMapInfo _mapInfo = null;

    private GameObject[,,] _mapObjects;
    private List<GameObject> asseList = new List<GameObject>();

    [SerializeField] private LayerMask _streaminLayerMask = 0;
    public LayerMask StreaminLayerMask { get { return _streaminLayerMask; } }

    private GameObject _streamers;
    private GameObject _level;

    public void AddActiveIndex(int index, GridStreamesrType type)
    {
        if (type == GridStreamesrType.Column)
        {
            if (!_activeColumnIndex.Contains(index))
            {
                _activeColumnIndex.Add(index);
            }
            Stream(index, type);
        }
        else
        {
            if (!_activeRowIndex.Contains(index))
            {
                _activeRowIndex.Add(index);
            }
            Stream(index, type);
        }
    }

    public void RemoveActiveIndex(int index, GridStreamesrType type)
    {
        if (type == GridStreamesrType.Column)
        {
            _activeColumnIndex.Remove(index);
            Unstream(index, type);
        }
        else
        {
            _activeRowIndex.Remove(index);
            Unstream(index, type);
        }
    }

    private void Stream(int index, GridStreamesrType type)
    {
        GameObject loadedObject = null;
        string objectName;

        if (type == GridStreamesrType.Column)
        {
            foreach (Layer layer in _mapInfo.Layers)
            {
                foreach (int rowIndex in ActiveRowIndex)
                {
                    objectName = layer.RowList[rowIndex].SegmentPregabName[index];

                    if (_mapObjects[rowIndex, index, layer.Index] == null &&
                        layer.RowList[rowIndex].SegmentPregabIndex[index] >= 0)
                    {
                        loadedObject = _mapObjects[rowIndex, index, layer.Index] = LoadMapSegmeentAsset(layer.RowList[rowIndex].SegmentPregabIndex[index]);
                        _mapObjects[rowIndex, index, layer.Index].transform.position = new Vector3(index, rowIndex, layer.Index);
                        loadedObject.transform.SetParent(this._level.transform);
                    }
                }
            }
        }
        else
        {
            foreach (Layer layer in _mapInfo.Layers)
            {
                foreach (int columIndex in ActiveColumnIndex)
                {
                    objectName = layer.RowList[index].SegmentPregabName[columIndex];

                    if (_mapObjects[index, columIndex, layer.Index] == null &&
                        layer.RowList[index].SegmentPregabIndex[columIndex] >= 0)
                    {
                        loadedObject = _mapObjects[index, columIndex, layer.Index] = LoadMapSegmeentAsset(layer.RowList[index].SegmentPregabIndex[columIndex]);
                        _mapObjects[index, columIndex, layer.Index].transform.position = new Vector3(columIndex, index, layer.Index);
                        loadedObject.transform.SetParent(this._level.transform);
                    }
                }
            }
        }
    }

    private GameObject LoadMapSegmeentAsset(string path)
    {
        //GameObject newObject = Resources.Load(index, typeof(GameObject)) as GameObject;

        //if (newObject != null)
        //{
        //    return Instantiate(newObject);
        //}
        foreach (GameObject asset in asseList)
        {
            if (asset.name.Equals(path))
            {
                return GameObject.Instantiate(asset);
            }
        }
        
        return GameObject.CreatePrimitive(PrimitiveType.Cube);
    }

    private GameObject LoadMapSegmeentAsset(int index)
    {
        //GameObject newObject = GameObject.Instantiate(asseList[index]);
        //if (newObject != null)
        //{
        //    return Instantiate(newObject);
        //}
        return GameObject.Instantiate(asseList[index]);
    }


    private void Unstream(int index, GridStreamesrType type)
    {
        if (type == GridStreamesrType.Column)
        {
            //ColumnGridStreamers[index].Test();
            foreach (Layer layer in _mapInfo.Layers)
            {
                foreach (int rowIndex in ActiveRowIndex)
                {
                    if (_mapObjects[rowIndex, index, layer.Index] != null)
                    {
                        GameObject.DestroyImmediate(_mapObjects[rowIndex, index, layer.Index]);
                    }
                }
            }
        }
        else
        {
            //RowGridStreamers[index].Test();
            foreach (Layer layer in _mapInfo.Layers)
            {
                foreach (int columIndex in ActiveColumnIndex)
                {
                    if (_mapObjects[index, columIndex, layer.Index] != null)
                    {
                        GameObject.DestroyImmediate(_mapObjects[index, columIndex, layer.Index]);
                    }
                }
            }
        }      
    }

    private void Awake()
    {
        Instance = this;

        _streamers = new GameObject("Streamers");
        _level = new GameObject("Level");

        _streamers.transform.SetParent(this.transform);
        _level.transform.SetParent(this.transform);

        foreach (string assetName in _mapInfo.assetNamesList)
        {
            asseList.Add(Resources.Load(assetName, typeof(GameObject)) as GameObject);
        }

        GridInitialization();
    }

    private void Start()
    {
        _streamerColumnOffsetValue = (GridSreamingHead.Instance.transform.lossyScale.z / 2f) + _streamerColumnOffset;
        _streamerRowOffsetValue = (GridSreamingHead.Instance.transform.lossyScale.y / 2f) + _streamerRowOffset;

        //foreach (GridStreamer streamer in ColumnGridStreamers)
        //{
        //    streamer.enabled = false;
        //}

        //foreach (GridStreamer streamer in RowGridStreamers)
        //{
        //    streamer.enabled = false;
        //}

        //StreamersLimiting();
    }

    private void StreamersLimiting()
    {
        Transform gridSreamingHeadTransform = GridSreamingHead.Instance.transform;

        int amountX, amountY;

        int minx = (int)gridSreamingHeadTransform.position.x - (int)_streamerColumnOffsetValue;
        minx = minx < 0 ? 0 : minx;

        if ((int)_streamerColumnOffsetValue * 2 + minx > ColumnGridStreamers.Count - 1)
        {
            amountX = (ColumnGridStreamers.Count - 1) - ((int)_streamerColumnOffsetValue * 2 + minx);
            amountX = (int)_streamerColumnOffsetValue * 2 + amountX;
        }
        else
        {
            amountX = (int)_streamerColumnOffsetValue * 2 + 2;
        }

        List<GridStreamer> activeStreames = ColumnGridStreamers.GetRange(minx, amountX);
        foreach (GridStreamer columnStreamer in activeStreames)
        {
            columnStreamer.enabled = true;
        }

        int miny = (int)gridSreamingHeadTransform.position.y - (int)_streamerRowOffsetValue;
        miny = miny < 0 ? 0 : miny;

        if ((int)_streamerRowOffsetValue * 2 + miny > RowGridStreamers.Count - 1)
        {
            amountY = (RowGridStreamers.Count - 1) - ((int)_streamerRowOffsetValue * 2 + miny);
            amountY = (int)_streamerRowOffsetValue * 2 + amountY;
        }
        else
        {
            amountY = (int)_streamerRowOffsetValue * 2;
        }

        activeStreames = RowGridStreamers.GetRange(miny, amountY);

        foreach (GridStreamer rowStreamer in activeStreames)
        {
            rowStreamer.enabled = true;
        }

    }

    private void Update()
    {
        StreamersLimiting();
    }

    private void GridInitialization()
    {
        //  Ustalanie rozmiarow siatki streamerów
        if (_mapInfo == null)
        {
            UnityEngine.Debug.Log("No map info selected!");
            return;
        }

        _height = (int)_mapInfo.Size.x;
        _widtch = (int)_mapInfo.Size.y;
        _layers = (int)_mapInfo.Size.z;

        //  Tworzenie tablicy pomocniczej mapy.
        //  Przetrzymuje informacje o obiektach mapy, które zostaly wczytane           
        _mapObjects = new GameObject[_height, _widtch, _layers];

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
            RowGridStreamers.Add(newGridStreamerComponent);

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
            newGridStreamer.gameObject.name = string.Format(
                GridStreamerName,
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
            ColumnGridStreamers.Add(newGridStreamerComponent);

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
            newGridStreamer.gameObject.name = string.Format(
                GridStreamerName,
                GridStreamesrType.Column, i);
            newGridStreamer.transform.position = newPosition;
            newGridStreamer.transform.SetParent(this._streamers.transform);
        }
    }
}
