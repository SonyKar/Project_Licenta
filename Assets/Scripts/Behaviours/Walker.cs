using Actions;
using Targets;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public class Walker : Behaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        public override void DoForTree(ITarget tree)
        {
            doerObject.ClearActionQueue();
            MoveTo moveToMe = new MoveTo(doerObject, navMeshAgent, ((MonoBehaviour)tree).transform.position, 3);
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
