using UnityEngine;
using UnityEditor;
using System.Collections;

public class Selector : EditorWindow
{
    private static GameObject[] _players;
    private static int _currentSelectedPlayer = 0;

    [MenuItem("Select/Player _F5")]
    public static void SelectPlayer()
    {
        if(_players == null)
        {
            _players = GameObject.FindGameObjectsWithTag("Player");
        }

        Selection.activeGameObject = _players[_currentSelectedPlayer];

        ++_currentSelectedPlayer;
        if(_currentSelectedPlayer > _players.Length - 1)
        {
            _currentSelectedPlayer = 0;
        }
    }
}
