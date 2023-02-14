using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Action = Actions.Action;

namespace ControllableUnit
{
    public class ActionDoer : MonoBehaviour
    {
        private readonly List<Action> _actions = new List<Action>();
        private Action _currentAction;
        private int _currentActionIndex;

        private void Update()
        {
            if (_actions.Count > 0 && _currentAction != null) _currentAction.Do();
        }

        public void NextAction()
        {
            _currentActionIndex = (_currentActionIndex + 1) % _actions.Count;
            if (_actions.Count > 0) _currentAction = _actions[_currentActionIndex];
            StopAllCoroutines();
        }

        public void AddAction(Action action)
        {
            _actions.Add(action);
            _currentAction ??= _actions[_currentActionIndex];
        }

        public void ClearActionQueue()
        {
            _actions.Clear();
            _currentActionIndex = 0;
            _currentAction = null;
            StopAllCoroutines();
        }

        public void StartNewCoroutine(IEnumerator enumerator)
        {
            StartCoroutine(enumerator);
        }
    }
}
