using Behaviours;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public interface ITarget
    {
        public Behaviour BestBehaviour(BehaviourChooser behaviourChooser);
        public void Behave(Behaviour behaviour);
    }
}
