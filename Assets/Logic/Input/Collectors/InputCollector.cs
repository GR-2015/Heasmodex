using System.Collections.Generic;
using UnityEngine;

internal class InputCollector : MonoBehaviour
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

    public static ButtonState SimulateButtonPress(bool condition, ButtonState currentButtonState)
    {
        if (condition)
        {
            if (currentButtonState == ButtonState.Released)
            {
                return ButtonState.Down;
            }

            if (currentButtonState == ButtonState.Down)
            {
                return ButtonState.Pressed;
            }

            if (currentButtonState == ButtonState.Pressed)
            {
                return ButtonState.Released;
            }

            return currentButtonState;
        }
        else
        {
            if (currentButtonState == ButtonState.Pressed)
            {
                return ButtonState.Up;
            }

            if (currentButtonState == ButtonState.Up)
            {
                return ButtonState.Released;
            }

            return currentButtonState;
        }
    }

    public static ButtonState SimulateButtonHold(bool condition, ButtonState currentButtonState)
    {
        if (condition)
        {
            if (currentButtonState == ButtonState.Released)
            {
                return ButtonState.Down;
            }

            if (currentButtonState == ButtonState.Down)
            {
                return ButtonState.Pressed;
            }

            return currentButtonState;
        }
        else
        {
            if (currentButtonState == ButtonState.Pressed)
            {
                return ButtonState.Up;
            }

            if (currentButtonState == ButtonState.Up)
            {
                return ButtonState.Released;
            }

            return currentButtonState;
        }
    }
}