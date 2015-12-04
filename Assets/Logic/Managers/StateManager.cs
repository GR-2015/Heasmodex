using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance
    {
        get;
        private set;
    }

    private readonly Stack<BaseState> _statesStack = new Stack<BaseState>();

    public BaseState CurrentState
    {
        get
        {
            return _statesStack.Peek();
        }
    }

    protected void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _statesStack.Push(new LocomotionState());
        CurrentState.EnterState();
    }

    private void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }

    private void Update()
    {
        CurrentState.StateUpdate();
    }

    private void LateUpdate()
    {
        CurrentState.StateLateUpdate();
    }

    public void EnterNewState(BaseState newState)
    {
        if (CurrentState != null)
        {
            CurrentState.Sleep();
        }

        _statesStack.Push(newState);

        if (CurrentState != null)
        {
            CurrentState.EnterState();
        }
    }

    public void ExitCurrentState()
    {
        CurrentState.ExitState();
        _statesStack.Pop();
        CurrentState.Return();
    }
}