using System.IO;
using UnityEditor;
using UnityEngine;
 
public class SceneSelector : EditorWindow
{
    [MenuItem("Window/SceneSelector")]
    public static void Init()
    {
        // Get existing open window or if none, make SegmentPregabName new one:
        SceneSelector window = (SceneSelector)EditorWindow.GetWindow(typeof(SceneSelector));
        window.Show();
    }

    public static void OpenScene(int index)
    {
        var scene = EditorBuildSettings.scenes[index];
        if (scene.enabled)
        {
            if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
            {
                EditorApplication.OpenScene(scene.path);
            }
        }
    }

    [MenuItem("Window/SceneSelector/SelectFirst  %1")]
    public static void SelectFirst()
    {
        OpenScene(0);
    }

    [MenuItem("Window/SceneSelector/SelectSecond %2")]
    public static void SelectSecond()
    {
        OpenScene(1);
    }

    [MenuItem("Window/SceneSelector/SelectThird %3")]
    public static void SelectThird()
    {
        OpenScene(2);
    }

    [MenuItem("Window/SceneSelector/SelectFourth %4")]
    public static void SelectFourth()
    {
        OpenScene(3);
    }

    [MenuItem("Window/SceneSelector/SelectFifth %5")]
    public static void SelectFifth()
    {
        OpenScene(4);
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        GUILayout.Label("Scenes In Build", EditorStyles.boldLabel);
        for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            var scene = EditorBuildSettings.scenes[i];
            if (scene.enabled)
            {
                var sceneName = Path.GetFileNameWithoutExtension(scene.path);
                var pressed = GUILayout.Button(i + ": " + sceneName, new GUIStyle(GUI.skin.GetStyle("Button")) { alignment = TextAnchor.MiddleLeft });
                if (pressed)
                {
                    if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
                    {
                        EditorApplication.OpenScene(scene.path);
                    }
                }
            }
        }

        EditorGUILayout.EndVertical();
    }

}
