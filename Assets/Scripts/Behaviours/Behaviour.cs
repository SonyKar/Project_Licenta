using ControllableUnit;
using Targets;
using UnityEngine;

namespace Behaviours
{
    public abstract class Behaviour : MonoBehaviour // Visitor
    {
        [SerializeField] protected ActionDoer activeObject;
        public virtual void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            
        }

        public virtual void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            
        }
    }
}
