using Actions;
using Targets;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class Walker : Behaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;

        public void MoveToObject(Target target)
        {
            MoveTo moveToMe = new MoveTo(activeObject, navMeshAgent, target.transform.position, 3);
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
            activeObject.ClearActionQueue();
            MoveTo moveTo = new MoveTo(activeObject, navMeshAgent, destination);
            activeObject.AddAction(moveTo);
        }
    }
}
