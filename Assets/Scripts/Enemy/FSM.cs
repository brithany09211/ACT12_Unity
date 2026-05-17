using System;
using System.Collections.Generic;

public class FSM<T> where T : Enum
{
    public class State
    {
        public Action OnEnter, OnStay, OnExit;
    }

    Dictionary<T, State> _states = new Dictionary<T, State>();
    T _current;

    public FSM(T initial)
    {
        _current = initial;
    }

    State GetOrCreate(T key)
    {
        if (!_states.ContainsKey(key))
            _states[key] = new State();
        return _states[key];
    }

    public void SetOnEnter(T state, Action a) => GetOrCreate(state).OnEnter = a;
    public void SetOnStay(T state, Action a)  => GetOrCreate(state).OnStay  = a;
    public void SetOnExit(T state, Action a)  => GetOrCreate(state).OnExit  = a;

    public void Update()
    {
        if (_states.ContainsKey(_current))
            _states[_current]?.OnStay?.Invoke();
    }

    public void ChangeState(T next)
    {
        if (_states.ContainsKey(_current))
            _states[_current]?.OnExit?.Invoke();
        _current = next;
        if (_states.ContainsKey(_current))
            _states[_current]?.OnEnter?.Invoke();
    }
}
