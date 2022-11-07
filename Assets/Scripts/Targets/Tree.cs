using Behaviours;
using Behaviour = Behaviours.Behaviour;

namespace Targets
{
    public class Tree : Mineable
    {
        public override Behaviour BestBehaviour(BehaviourChooser behaviourChooser)
        {
            return behaviourChooser.ChooseBehaviour(this);
        }

        public override void Behave(Behaviour behaviour)
        {
            behaviour.DoForTree(this);
        }
    }
}
