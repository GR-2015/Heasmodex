using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SceneSelectorConfig : ScriptableObject
{
    [SerializeField] public List<string> leveNamesList = new List<string>(); 
}
