using System;
using System.Collections.Generic;
using UnityEngine;

public class InputCollector : MonoBehaviour
{
    public static InputCollector Instance { get; private set; }

    private List<BaseInputSource> _inputSources = new List<BaseInputSource>();
    private List<InputValues> _inputValues = new List<InputValues>();

    public List<InputValues> InputValues
    {
        get { return _inputValues; }
    }

    private void Awake()
    {
        Instance = this;
    }

    public void AddInputValues(InputValues newInputValues)
    {
        _inputValues.Add(newInputValues);
    }

    public void AddInputSorces(BaseInputSource newInputSorce)
    {
        _inputSources.Add(newInputSorce);
    }

    private void Update()
    {
        foreach (var inputSource in _inputSources)
        {
            inputSource.GetInputValues();
        }
    }
}