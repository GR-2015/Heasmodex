using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Globalization;

public class MapGenerator : EditorWindow
{
    private const string LevelElementString = "Level element";
    private const string NewMapPath = "Assets/{0}.asset";
    private const string NewMapName = "NewMap";
    private int _height = 0;
    private int _width = 0;

    [MenuItem("Window/Map/Map Generator")]
    public static void Init()
    {
        // Get existing open window or if none, make segmentPregabName new one:
        MapGenerator window = (MapGenerator)EditorWindow.GetWindow(typeof(MapGenerator));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.BeginHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("_height");
        _height = EditorGUILayout.IntField(_height);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("_width");
        _width = EditorGUILayout.IntField(_width);
        GUILayout.EndHorizontal();

        GUILayout.EndHorizontal();

        if (GUILayout.Button("Test"))
        {
            GridMapInfo mapInfo = ScriptableObject.CreateInstance<GridMapInfo>();

            //mapInfo.mapPtefabNames = new string[_height, _width, 1];
            mapInfo.size = new Vector3(_height, _width, 1);
            
            mapInfo.RowList = new RowInfo[_height];

            for (int i = 0; i < mapInfo.RowList.Length; i++)
            {
                mapInfo.RowList[i] = new RowInfo(_height);
            }

            GameObject[] mapSegmentsList = GameObject.FindGameObjectsWithTag(LevelElementString);

            foreach (GameObject mapSegments in mapSegmentsList)
            {
                string[] newName = mapSegments.name.Split(' ');
                mapSegments.name = newName[0];
            }

            foreach (GameObject o in mapSegmentsList)
            {
                Debug.Log(o.name + " " + o.transform.position);
                //mapInfo.mapPtefabNames[(int)o.transform.position.y, (int)o.transform.position.x, 0] = o.name;
                mapInfo.RowList[(int)o.transform.position.y].segmentPregabName[(int)o.transform.position.x] = o.name;
            }

            //string debugString = string.Empty;
            //for (int i = 0; i < _height; i++)
            //{
            //    debugString = string.Empty;
            //    for (int j = 0; j < _width; j++)
            //    {
            //        debugString += mapInfo.mapPtefabNames[i, j, 0] != null ? "x" : "o";
            //    }
            //    Debug.Log(debugString);
            //}

            AssetDatabase.CreateAsset(mapInfo, string.Format(NewMapPath, NewMapName));
            AssetDatabase.SaveAssets();

            EditorUtility.FocusProjectWindow();
            Selection.activeObject = mapInfo;

        }
    }


    private string Cheack(Vector3 position)
    {
        position.z -= 2;
        RaycastHit hit;
        Physics.Raycast(position, Vector3.forward, out hit, 1.5f);

        return hit.collider != null ? hit.collider.gameObject.name : String.Empty;
    }

}
