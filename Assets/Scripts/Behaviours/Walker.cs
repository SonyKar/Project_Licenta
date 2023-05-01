using System;
using Actions;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Walker : Behaviour
    {
        [SerializeField] private int stoppingDistance = 2;
        
        public void MoveToObject(Target target, bool clearActionQueueOnStop = false)
        {
            if (NavMeshAgent == null)
            {
                Debug.Log("No NavMeshAgent");
                return;
            }

            Collider collider = target.GetComponent<Collider>();
            if (collider is null)
            {
                Debug.Log(target.name + " does not have Collider");
                return;
            }
            MoveTo moveToMe = new MoveTo(ActiveObject, NavMeshAgent, collider.ClosestPointOnBounds(ActiveObject.transform.position), stoppingDistance, clearActionQueueOnStop: clearActionQueueOnStop);
            ActiveObject.AddAction(moveToMe);
        }

        public void MoveToNearest(Func<Vector3, Target> destinationFinder)
        {
            if (NavMeshAgent == null)
            {
                Debug.Log("No NavMeshAgent");
                return;
            }
            MoveTo moveToMe = new MoveTo(ActiveObject, NavMeshAgent, Vector3.zero, stoppingDistance, destinationFinder);
            ActiveObject.AddAction(moveToMe);
        }
        
        public override void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            MoveToObject(tree, true);
        }
        
        public override void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            MoveToObject(sawmill, true);
        }

        public override void DoForConstruction(Target construction, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            MoveToObject(construction, true);
        }
        
        public override void DoForNonInteractableBuilding(NonInteractableBuilding nonInteractableBuilding, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            MoveToObject(nonInteractableBuilding, true);
        }

        public void DoForGround(Vector3 destination)
        {
            if (NavMeshAgent == null)
            {
                Debug.Log("No NavMeshAgent");
                return;
            }
            ActiveObject.ClearActionQueue();
            MoveTo moveTo = new MoveTo(ActiveObject, NavMeshAgent, destination, clearActionQueueOnStop: true);
            ActiveObject.AddAction(moveTo);
        }
    }
}
