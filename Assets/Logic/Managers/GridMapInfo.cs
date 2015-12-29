using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

public class GridMapInfo : ScriptableObject
{
    [SerializeField] public Vector3 Size = Vector3.zero;

    [SerializeField] public RowInfo[] RowList;
}

[System.Serializable]
public class RowInfo
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