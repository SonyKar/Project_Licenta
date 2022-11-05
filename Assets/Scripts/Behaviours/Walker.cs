using Actions;
using UnityEngine;
using UnityEngine.AI;
using Tree = Targets.Target;

namespace Behaviours
{
    public class Walker : Behaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        public override void DoForTree(Tree tree)
        {
            doerObject.ClearActionQueue();
            MoveTo moveToMe = new MoveTo(doerObject, navMeshAgent, tree.transform.position, 5);
            doerObject.AddAction(moveToMe);
        }

        public void DoForGround(Vector3 destination)
        {
            doerObject.ClearActionQueue();
            MoveTo moveTo = new MoveTo(doerObject, navMeshAgent, destination);
            doerObject.AddAction(moveTo);
        }
    }
}
