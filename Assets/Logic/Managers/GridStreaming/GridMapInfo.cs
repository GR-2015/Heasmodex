using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

public class GridMapInfo : ScriptableObject
{
    [SerializeField] public Vector3 Size = Vector3.zero;
    [SerializeField] public Layer[] Layers;
    [SerializeField] public List<string> assetNamesList = new List<string>();
}

[System.Serializable]
public struct RowInfo
{
    [SerializeField] public string[] SegmentPregabName;
    [SerializeField] public int[] SegmentPregabIndex;

    public RowInfo(int tabSize)
    {
        SegmentPregabName = new string[tabSize];
        SegmentPregabIndex = new int[tabSize];

        for (int i = 0; i < tabSize; i++)
        {
            SegmentPregabName[i] = string.Empty;
            SegmentPregabIndex[i] = -1;
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