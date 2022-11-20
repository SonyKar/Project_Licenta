using ControllableUnit;
using UnityEngine;
using UnityEngine.AI;

namespace Actions
{
    public class MoveTo : Action
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        private readonly Vector3 _destination;
        private bool _wasStarted;
        private readonly int _stoppingDistance;

        public MoveTo(ActionDoer activeObject, NavMeshAgent navMeshAgent, Vector3 destination, int stoppingDistance = 0)
        : base(activeObject)
        {
            _navMeshAgent = navMeshAgent;
            _destination = destination;
            _stoppingDistance = stoppingDistance;
        }
        
        public override void Do()
        {
            if (_wasStarted && _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance + 0.1f)
            {
                _wasStarted = false;
                ActiveObject.NextAction();
                return;
            }
            
            if (!_wasStarted)
            {
                _navMeshAgent.stoppingDistance = _stoppingDistance;
                _navMeshAgent.destination = _destination;
                _wasStarted = true;
            }
        }
    }
}
