using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;

public class GridMapInfo : ScriptableObject
{
    [SerializeField] public Vector3 size = Vector3.zero;

    [SerializeField] public RowInfo[] RowList;
}

[System.Serializable]
public class RowInfo
{
    public RowInfo(int tabSize)
    {
        segmentPregabName = new string[tabSize];
    }

    [SerializeField] public string[] segmentPregabName;

}