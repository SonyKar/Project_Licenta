using ControllableUnit;
using Targets;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviours
{
    public abstract class Behaviour : MonoBehaviour // Visitor
    {
        protected ActionDoer ActiveObject;
        protected Inventory Inventory;
        protected Carrier Carrier;
        protected Walker Walker;
        protected NavMeshAgent NavMeshAgent;
        
        private void Awake()
        {
            ActiveObject = GetComponent<ActionDoer>();
            Inventory = GetComponent<Inventory>();
            Carrier = GetComponent<Carrier>();
            Walker = GetComponent<Walker>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        public virtual void DoForTree(Target tree, bool doCleanActionQueue = true)
        {
            
        }

        public virtual void DoForSawmill(Target sawmill, bool doCleanActionQueue = true)
        {
            
        }
        
        public virtual void DoForConstruction(Target construction, bool doCleanActionQueue = true)
        {
            
        }
    }
}
