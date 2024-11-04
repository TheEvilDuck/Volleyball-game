using System;
using System.Collections.Generic;
using Common.Tickables;
using DI;
using UnityEngine;

namespace Common.States
{
    public class StateMachine : IStateMachine, ITickable, IDisposable
    {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        private IState _currentState;

        public DIContainer StateMachineContext {get; private set;} = new DIContainer();

        public void ChangeState<TState>()
        {
            Type stateType = typeof(TState);

            if (!_states.ContainsKey(stateType))
            {
                Debug.LogError($"State machine doesn't contain {stateType}");
                return;
            }

            _currentState?.Exit();
            _currentState = _states[stateType];
            _currentState?.Enter();
        }

        public void AddState(IState state)
        {
            Type stateType = state.GetType();

            if (_states.ContainsKey(stateType))
            {
                Debug.LogError($"State of type {stateType} is already registered!");
                return;
            }

            _states.Add(stateType, state);
        }
        public void Tick(float deltaTime) => _currentState?.Update(deltaTime);

        public void Dispose()
        {
            _currentState?.Exit();
            _currentState = null;
        }
    }
}
