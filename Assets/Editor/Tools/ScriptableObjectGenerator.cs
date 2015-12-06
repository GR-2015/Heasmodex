using UnityEngine;
using UnityEditor;

using System.Collections;

public class ScriptableObjectGenerator
{
    [MenuItem("Assets/Create/Generate ScriptableObject/InputNames")]
    public static void CreateInputNamesScriptableObject()
    {
        InputNames newInputNames = ScriptableObject.CreateInstance<InputNames>();

        AssetDatabase.CreateAsset(newInputNames,"Assets/NewInputNames.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = newInputNames;
    }
}
