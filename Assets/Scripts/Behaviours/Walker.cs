using System;
using Actions;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Walker : Behaviour
    {
        public void MoveToObject(Target target)
        {
            if (NavMeshAgent == null)
            {
                Debug.Log("No NavMeshAgent");
                return;
            }
            MoveTo moveToMe = new MoveTo(ActiveObject, NavMeshAgent, target.transform.position, 3);
            ActiveObject.AddAction(moveToMe);
        }

        public void MoveToNearest(Func<Vector3, Target> destinationFinder)
        {
            if (NavMeshAgent == null)
            {
                Debug.Log("No NavMeshAgent");
                return;
            }
            MoveTo moveToMe = new MoveTo(ActiveObject, NavMeshAgent, Vector3.zero, 3, destinationFinder);
            ActiveObject.AddAction(moveToMe);
        }
        
        public override void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            MoveToObject(tree);
        }
        
        public override void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) ActiveObject.ClearActionQueue();
            MoveToObject(sawmill);
        }

        public void DoForGround(Vector3 destination)
        {
            if (NavMeshAgent == null)
            {
                Debug.Log("No NavMeshAgent");
                return;
            }
            ActiveObject.ClearActionQueue();
            MoveTo moveTo = new MoveTo(ActiveObject, NavMeshAgent, destination);
            ActiveObject.AddAction(moveTo);
        }
    }
}
