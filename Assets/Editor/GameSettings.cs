using UnityEngine;
using UnityEditor;
using System.Collections;

public class GameSettings : EditorWindow
{

    [MenuItem("Window/Game settings")]
    static void Init()
    {
        // Get existing open window or if none, make segmentPregabName new one:
        GameSettings window = (GameSettings)EditorWindow.GetWindow(typeof(GameSettings));
        window.Show();
    }

    void OnGUI()
    {
    }

}
