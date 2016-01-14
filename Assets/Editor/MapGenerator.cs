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
    private int _layers = 0;

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

        GUILayout.BeginHorizontal();
        GUILayout.Label("Layers");
        _layers = EditorGUILayout.IntField(_layers);
        GUILayout.EndHorizontal();


        if (GUILayout.Button("Set width form selected."))
        {
            _width = (int) (Selection.activeObject as GameObject).transform.position.x + 1;
        }
        if (GUILayout.Button("Set height form selected."))
        {
            _height = (int)(Selection.activeObject as GameObject).transform.position.y + 1;
        }
        if (GUILayout.Button("Set layers form selected."))
        {
            _layers = (int)(Selection.activeObject as GameObject).transform.position.z + 1;
        }
        if (GUILayout.Button("Create map."))
        {
            CreateMap();
        }



    }

    private void CreateMap()
    {
        GridMapInfo mapInfo = ScriptableObject.CreateInstance<GridMapInfo>();

        duplicateList.Clear();
        mapInfo.Size = new Vector3(_height, _width, _layers);
        mapInfo.Layers = new Layer[_layers];

        for (int i = 0; i < mapInfo.Layers.Length; i++)
        {
            mapInfo.Layers[i] = new Layer(_width, _height, i);
        }

        GameObject[] mapSegmentsList = GameObject.FindGameObjectsWithTag(LevelElementString);

        int counter = 0;
        float x = 0f;

        try
        {
            foreach (GameObject o in mapSegmentsList)
            {
                x = ((10 * counter) / mapSegmentsList.Length);
                EditorUtility.DisplayProgressBar("Map grneration", counter + "/" + mapSegmentsList.Length, x);

                //o.transform.parent = null;

                string[] newName = o.name.Split(' ');
                o.name = newName[0];

                if (!mapInfo.assetNamesList.Contains(o.name))
                {
                    mapInfo.assetNamesList.Add(o.name);
                }

                o.transform.position = new Vector3((int)o.transform.position.x, (int)o.transform.position.y, (int)o.transform.position.z);

                if (mapInfo.Layers[(int)o.transform.position.z].RowList[(int)o.transform.position.y].SegmentPregabName[(int)o.transform.position.x] == string.Empty)
                {
                    mapInfo.Layers[(int)o.transform.position.z].RowList[(int)o.transform.position.y].SegmentPregabName[(int)o.transform.position.x] = o.name;
                }
                else
                {
                    duplicateList.Add(o);
                }

                ++counter;
            }
        }
        catch (Exception)
        {
            EditorUtility.ClearProgressBar();
            Debug.Log("Index");
        }

        EditorUtility.ClearProgressBar();

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
