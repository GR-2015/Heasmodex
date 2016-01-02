using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

public class MapGenerator : EditorWindow
{
    private const string LevelElementString = "Level element";
    private const string NewMapPath = "Assets/{0}.asset";
    private const string NewMapNameDefault = "NewMap";

    private string _newMapName = string.Empty;

    private int _height = 0;
    private int _width = 0;

    private List<GameObject> duplicateList = new List<GameObject>();

    [MenuItem("Window/Map/Map Generator")]
    public static void Init()
    {
        // Get existing open window or if none, make SegmentPregabName new one:
        MapGenerator window = (MapGenerator)EditorWindow.GetWindow(typeof(MapGenerator));
        window.Show();
    }

    private void OnGUI()
    {
        if (_newMapName == string.Empty)
        {
            _newMapName = NewMapNameDefault;
        }

        _newMapName = EditorGUILayout.TextField("New map name: ", _newMapName); 

        GUILayout.BeginHorizontal();
        GUILayout.Label("Height");
        _height = EditorGUILayout.IntField(_height);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Width");
        _width = EditorGUILayout.IntField(_width);
        GUILayout.EndHorizontal();

        if (GUILayout.Button("Create map."))
        {
            CreateMap();
        }
    }

    private void CreateMap()
    {
        GridMapInfo mapInfo = ScriptableObject.CreateInstance<GridMapInfo>();

        duplicateList.Clear();
        mapInfo.Size = new Vector3(_height, _width, 1);
        mapInfo.RowList = new RowInfo[_height];

        for (int i = 0; i < mapInfo.RowList.Length; i++)
        {
            mapInfo.RowList[i] = new RowInfo(_width);
        }

        GameObject[] mapSegmentsList = GameObject.FindGameObjectsWithTag(LevelElementString);

        foreach (GameObject o in mapSegmentsList)
        {
            string[] newName = o.name.Split(' ');
            o.name = newName[0];

            Debug.Log(o.name + " " + o.transform.position);
            Debug.Log((int)o.transform.position.y + " " + (int)o.transform.position.x);

            if (mapInfo.RowList[(int) o.transform.position.y].SegmentPregabName[(int) o.transform.position.x] == string.Empty)
            {
                mapInfo.RowList[(int)o.transform.position.y].SegmentPregabName[(int)o.transform.position.x] = o.name;
            }
            else
            {
                duplicateList.Add(o);
            }
        }

        foreach (GameObject o in duplicateList)
        {
            Debug.Log(o.name + o.transform.position+  " Duplicate!");
            GameObject.DestroyImmediate(o);
        }

        AssetDatabase.CreateAsset(mapInfo, string.Format(NewMapPath, _newMapName));
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = mapInfo;
    }
}
