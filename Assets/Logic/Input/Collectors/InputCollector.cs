using System.Collections.Generic;
using UnityEngine;

public class InputCollector : MonoBehaviour
{
    public static InputCollector Instance { get; private set; }

    private List<InputSource> _inputSources = new List<InputSource>();
    private List<InputValues> _inputValues = new List<InputValues>();

    public List<InputValues> InputValues
    {
        get { return _inputValues; }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //List<PlayerController> playersList = CharacterManager.Instance.Players;

        //foreach (var player in playersList)
        //{
        //    var newInputValues = new InputValues(player);
        //    _inputValues.Add(newInputValues);

        //    CreateInputSorces(player.InputSource, newInputValues);
        //}
    }

    public void CreateInputSorces(PlayerController player)
    {
        var inputValues = new InputValues(player);
        _inputValues.Add(inputValues);

        foreach (var InputSourc in player.InputSource)
        {
            switch (InputSourc)
            {
                case InputSourceType.KeyboardAndMouse:
                    _inputSources.Add(new KeyboardAndMouse(inputValues));
                    break;
            }
        }
    }

    private void Update()
    {
        foreach (var inputSource in _inputSources)
        {
            inputSource.GetInputValues();
        }
    }
}