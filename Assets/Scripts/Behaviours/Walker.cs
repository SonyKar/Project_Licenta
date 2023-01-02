using Actions;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public class Walker : Behaviour
    {
        public void MoveToObject(Target target)
        {
            if (NavMeshAgent is null) return;
            MoveTo moveToMe = new MoveTo(activeObject, NavMeshAgent, target.transform.position, 3);
            activeObject.AddAction(moveToMe);
        }
        
        public override void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) activeObject.ClearActionQueue();
            MoveToObject(tree);
        }
        
        public override void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            if (doCleanActionQueue) activeObject.ClearActionQueue();
            MoveToObject(sawmill);
        }

        public void DoForGround(Vector3 destination)
        {
            if (NavMeshAgent is null) return;
            activeObject.ClearActionQueue();
            MoveTo moveTo = new MoveTo(activeObject, NavMeshAgent, destination);
            activeObject.AddAction(moveTo);
        }
    }
}
