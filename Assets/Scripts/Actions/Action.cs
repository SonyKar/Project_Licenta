using ControllableUnit;

namespace Actions
{
    public abstract class Action
    {
        protected readonly ActionDoer ActiveObject;

        protected Action(ActionDoer actionDoer)
        {
            ActiveObject = actionDoer;
        }
        
        public abstract void Do();
    }
}
