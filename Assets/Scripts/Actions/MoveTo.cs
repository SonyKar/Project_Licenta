using System;
using ControllableUnit;
using Targets;
using UnityEngine;
using UnityEngine.AI;

namespace Actions
{
    public class MoveTo : Action
    {
        private readonly NavMeshAgent _navMeshAgent;
        
        private Vector3 _destination;
        private readonly Func<Vector3, Target> _destinationFinder;
        private bool _wasStarted;
        private readonly int _stoppingDistance;
        private Target _nearestTarget;
        private readonly bool _clearActionQueueOnStop;

        public MoveTo(ActionDoer activeObject, NavMeshAgent navMeshAgent, Vector3 destination, int stoppingDistance = 0, Func<Vector3, Target> destinationFinder = null, bool clearActionQueueOnStop = false)
        : base(activeObject)
        {
            _navMeshAgent = navMeshAgent;
            _stoppingDistance = stoppingDistance;
            _destination = destination;
            _destinationFinder = destinationFinder;
            _clearActionQueueOnStop = clearActionQueueOnStop;
        }
        
        public override void Do()
        {
            if (_wasStarted && _navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance + 0.1f)
            {
                _wasStarted = false;
                if (_clearActionQueueOnStop) ActiveObject.ClearActionQueue();
                else ActiveObject.NextAction();
                return;
            }

            if (!_wasStarted)
            {
                ActiveObject.GetAnimator().SetWalkingAnimation();
            }
            
            if (!_wasStarted || _destinationFinder != null && _nearestTarget == null)
            {
                if (_destinationFinder != null)
                {
                    _nearestTarget = _destinationFinder(_navMeshAgent.gameObject.transform.position);
                    if (_nearestTarget is null)
                    {
                        _navMeshAgent.destination = _navMeshAgent.transform.position;
                        ActiveObject.ClearActionQueue();
                        return;
                    }
                    Collider collider = _nearestTarget.GetComponent<Collider>();
                    if (collider is null)
                    {
                        Debug.Log(_nearestTarget.name + " does not have Collider");
                        return;
                    }
                    _destination = collider.ClosestPointOnBounds(ActiveObject.transform.position);
                }

                _navMeshAgent.stoppingDistance = _stoppingDistance;
                _navMeshAgent.destination = _destination;
                _wasStarted = true;
            }
        }
    }
}
