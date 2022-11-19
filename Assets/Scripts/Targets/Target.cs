using Behaviours;
using UnityEngine;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public abstract class Target : MonoBehaviour
    {
        public abstract Behaviour BestBehaviour(BehaviourChooser behaviourChooser);
        public abstract void Behave(Behaviour behaviour);
    }
}
