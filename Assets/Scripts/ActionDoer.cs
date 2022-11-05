using System.Collections.Generic;
using UnityEngine;
using Action = Actions.Action;

public class ActionDoer : MonoBehaviour
{
    private readonly List<Action> _actions = new();
    private int _currentAction;

    private void Update()
    {
        if (_actions.Count > 0) _actions[_currentAction].Do();
    }

    public void NextAction()
    {
        _currentAction = (_currentAction + 1) % _actions.Count;
    }

    public void AddAction(Action action)
    {
        _actions.Add(action);
    }

    public void ClearActionQueue()
    {
        _actions.Clear();
        _currentAction = 0;
    }
}
