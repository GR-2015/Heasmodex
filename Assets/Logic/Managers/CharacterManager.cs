using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager Instance { get; private set; }

    private readonly List<PlayerController> _players = new List<PlayerController>();

    public List<PlayerController> Players
    {
        get { return _players; }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void RegisterPlayer(PlayerController instance)
    {
        _players.Add(instance);

        var inputValues = new InputValues(instance);
        InputCollector.Instance.AddInputValues(inputValues);

        foreach (var InputSourc in instance.InputSource)
        {
            switch (InputSourc)
            {
                case InputSourceType.KeyboardAndMouse:
                    InputCollector.Instance.AddInputSorces(new KeyboardAndMouse(inputValues));
                    break;
            }
        }

    }

    public void UnRegisterPlayer(PlayerController instance)
    {
        _players.Remove(instance);
    }



}