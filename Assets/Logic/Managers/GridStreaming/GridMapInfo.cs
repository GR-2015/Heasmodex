using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

public class GridMapInfo : ScriptableObject
{
    [SerializeField] public Vector3 Size = Vector3.zero;
    [SerializeField] public Layer[] Layers;
}

[System.Serializable]
public struct RowInfo
{
    [SerializeField] public string[] SegmentPregabName;

    public RowInfo(int tabSize)
    {
        SegmentPregabName = new string[tabSize];

        for (int i = 0; i < tabSize; i++)
        {
            SegmentPregabName[i] = string.Empty;
        }
    }
}

[System.Serializable]
public struct Layer
{
    [SerializeField] public int Index;
    [SerializeField] public RowInfo[] RowList;

    public Layer(int _width, int _height, int index)
    {
        this.Index = index;

        RowList = new RowInfo[_height];
        for (int i = 0; i < RowList.Length; i++)
        {
            RowList[i] = new RowInfo(_width);
        }
    }
}