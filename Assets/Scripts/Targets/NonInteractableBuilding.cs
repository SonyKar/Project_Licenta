using Behaviours;

namespace Targets
{
    public class NonInteractableBuilding : Target
    {
        public override Behaviour BestBehaviour(BehaviourChooser behaviourChooser)
        {
            return behaviourChooser.ChooseBehaviour(this);
        }

        public override void Behave(Behaviour behaviour)
        {
            behaviour.DoForNonInteractableBuilding(this);
        }
    }
}