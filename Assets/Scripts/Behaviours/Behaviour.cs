using UnityEngine;
using Tree = Targets.Target;

namespace Behaviours
{
    public abstract class Behaviour : MonoBehaviour // Visitor
    {
        [SerializeField] protected ActionDoer doerObject;
        public virtual void DoForTree(Tree tree)
        {
            
        }
    }
}
