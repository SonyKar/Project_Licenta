using ControllableUnit;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public abstract class Behaviour : MonoBehaviour // Visitor
    {
        [SerializeField] protected ActionDoer doerObject;
        public virtual void DoForTree(ITarget tree)
        {
            
        }
    }
}
